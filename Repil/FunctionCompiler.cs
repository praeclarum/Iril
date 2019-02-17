using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Repil.Types;
using CecilInstruction = Mono.Cecil.Cil.Instruction;
using System.Runtime.InteropServices;

namespace Repil
{
    public class DefinedFunction
    {
        public Symbol Symbol;
        public Repil.Module IRModule;
        //public IR.FunctionDeclaration IRDeclaration;
        public IR.FunctionDefinition IRDefinition;
        public MethodDefinition ILDefinition;
        public SymbolTable<ParameterDefinition> ParamSyms;
    }

    public class SimdVector
    {
        //public int Length;
        //public LType ElementType;
        public TypeReference ElementClrType;

        public TypeReference ClrType;

        public MethodReference Ctor;

        public FieldReference[] ElementFields;

        //public MethodReference Add;
        public MethodReference Subtract;
    }

    class FunctionCompiler
    {
        // Input
        readonly Compilation compilation;
        readonly DefinedFunction function;

        // Created
        readonly MethodBody body;
        readonly ILProcessor il;

        // Working Variables
        bool triedToCompile;
        CecilInstruction prev;
        readonly LivelinessTable liveliness;

        public FunctionCompiler (Compilation compilation, DefinedFunction function)
        {
            this.compilation = compilation;
            this.function = function;
            body = new MethodBody (function.ILDefinition);
            il = body.GetILProcessor ();
            prev = default (CecilInstruction);
            liveliness = new LivelinessTable (function);
        }

        public void CompileFunction ()
        {
            if (triedToCompile)
                return;
            triedToCompile = true;

            var f = function.IRDefinition;
            var paramSyms = function.ParamSyms;
            var md = function.ILDefinition;
            var vectorTemps = new Dictionary<(string, int), VariableDefinition> ();

            //
            // Get local usage count
            //
            var localCounts = new SymbolTable<int> ();
            foreach (var p in paramSyms) {
                localCounts.Add (p.Key, 0);
            }
            foreach (var b in f.Blocks) {
                foreach (var i in b.Assignments) {
                    if (i.Result != LocalSymbol.None)
                        localCounts.Add (i.Result, 0);
                }
            }
            foreach (var b in f.Blocks) {
                var insts = b.Assignments.Select (x => x.Instruction).Concat (new IR.Instruction[] { b.Terminator });
                foreach (var i in insts) {
                    foreach (var l in i.ReferencedLocals) {
                        localCounts[l]++;
                    }
                }
            }

            //
            // Determine whether assignments can be inlined
            //
            var shouldInline = new SymbolTable<bool> ();
            foreach (var p in paramSyms.Keys) {
                shouldInline[p] = true;
            }
            foreach (var b in f.Blocks) {
                for (var i = 0; i < b.Assignments.Length; i++) {
                    var a = b.Assignments[i];
                    var symbol = a.Result;
                    if (symbol == LocalSymbol.None)
                        continue;

                    // Make sure it's used only once
                    var referencedOnce = symbol != LocalSymbol.None && localCounts.ContainsKey (symbol) && localCounts[symbol] == 1;

                    var should = false;
                    if (referencedOnce) {
                        should = true;
                        // Make sure its use is before a state-changing instruction
                        for (var j = i + 1; j < b.Assignments.Length; j++) {
                            if (b.Assignments[j].Instruction.ReferencedLocals.Contains (symbol)) {
                                break;
                            }
                            if (!b.Assignments[j].Instruction.IsIdempotent) {
                                should = false;
                                break;
                            }
                        }
                    }
                    shouldInline.Add (symbol, should);
                }
            }

            var vdbgs = new List<VariableDebugInformation> ();
            var phiLocals = new SymbolTable<VariableDefinition> ();
            var locals = new SymbolTable<VariableDefinition> ();

            //
            // Create phi locals
            //
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    var symbol = a.Result;
                    if (a.HasResult && a.Instruction is IR.PhiInstruction phi) {
                        var irtype = a.Instruction.ResultType (function.IRModule);
                        var ctype = GetClrType (irtype);
                        var local = GetFreeVariable (symbol, ctype, true);
                        phiLocals[a.Result] = local;
                        //var name = "phi" + a.Result.Text.Substring (1);
                        //var dbg = new VariableDebugInformation (local, name);
                        //vdbgs.Add (dbg);
                    }
                }
            }

            //
            // Create variables for non-inlined assignments
            //
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    var symbol = a.Result;
                    if (a.HasResult && !ShouldInline (symbol) && !(a.Instruction is IR.PhiInstruction)) {
                        var irtype = a.Instruction.ResultType (function.IRModule);
                        var ctype = GetClrType (irtype);
                        var local = GetFreeVariable (symbol, ctype, false);
                        locals[a.Result] = local;
                        //var name = "local" + symbol.Text.Substring (1);
                        //var dbg = new VariableDebugInformation (local, name);
                        //vdbgs.Add (dbg);
                    
                    }
                }
            }

            //
            // Create target instructions for each block
            //
            var blockFirstInstr = new SymbolTable<CecilInstruction> ();
            foreach (var b in f.Blocks) {
                var i = il.Create (OpCodes.Nop);
                il.Append (i);
                blockFirstInstr[b.Symbol] = i;
            }

            //
            // Emit each block
            //
            for (var i = 0; i < f.Blocks.Length; i++) {
                var b = f.Blocks[i];
                var nextBlock = i + 1 < f.Blocks.Length ? f.Blocks[i + 1] : null;

                //
                // Emit the assignments
                //
                prev = blockFirstInstr[b.Symbol];
                foreach (var a in b.Assignments) {
                    if (!ShouldInline (a.Result)
                        && !(a.Instruction is IR.PhiInstruction)) {

                        EmitInstruction (a.Result, a.Instruction, nextBlock);

                        // If we need to assign it, do so
                        if (locals.TryGetValue (a.Result, out var vd)) {
                            Emit (il.Create (OpCodes.Stloc, vd));
                        }
                        else {
                            // If it produced a value but it's discarded, pop it
                            if (a.Result != LocalSymbol.None && localCounts[a.Result] == 0) {
                                Emit (il.Create (OpCodes.Pop));
                            }
                        }
                    }
                }

                //
                // Emit phi variables
                //
                foreach (var ob in f.Blocks) {
                    foreach (var oa in ob.Assignments) {
                        if (oa.Result != LocalSymbol.None && oa.Instruction is IR.PhiInstruction phi) {
                            var phiV = phi.Values.FirstOrDefault (x => x.Label is IR.LocalValue l && l.Symbol == b.Symbol);
                            if (phiV != null) {
                                var phiLocal = GetPhiLocal (oa.Result);
                                if (phiV.Value is IR.LocalValue lv
                                    && phiLocals.TryGetValue (lv.Symbol, out var vd)
                                    && ReferenceEquals (phiLocal, vd)) {

                                    // Redundant assignment
                                }
                                else {
                                    EmitValue (phiV.Value, phi.Type);
                                    Emit (il.Create (OpCodes.Stloc, phiLocal));
                                }
                            }
                        }
                    }
                }

                //
                // Emit terminator
                //
                EmitInstruction (LocalSymbol.None, b.Terminator, nextBlock);
            }

            body.Optimize ();

            var scopeDbg = new ScopeDebugInformation (body.Instructions.First (), body.Instructions.Last ());
            foreach (var d in vdbgs) {
                scopeDbg.Variables.Add (d);
            }

            md.DebugInformation.Scope = scopeDbg;
            md.Body = body;

            bool ShouldInline (LocalSymbol symbol)
            {
                return shouldInline.TryGetValue (symbol, out var s) && s;
            }

            VariableDefinition GetPhiLocal (Symbol assignment)
            {
                return phiLocals[assignment];
            }

            void EmitInstruction (LocalSymbol assignedSymbol, IR.Instruction instruction, IR.Block nextBlock)
            {
                switch (instruction) {
                    case IR.AddInstruction add:
                        EmitValue (add.Op1, add.Type);
                        EmitValue (add.Op2, add.Type);
                        Emit (il.Create (OpCodes.Add));
                        break;
                    case IR.AllocaInstruction add:
                        Emit (il.Create (OpCodes.Ldc_I4, (int)add.Type.GetByteSize (function.IRModule)));
                        Emit (il.Create (OpCodes.Conv_U));
                        Emit (il.Create (OpCodes.Localloc));
                        break;
                    case IR.AndInstruction and:
                        EmitValue (and.Op1, and.Type);
                        EmitValue (and.Op2, and.Type);
                        Emit (il.Create (OpCodes.And));
                        break;
                    case IR.BitcastInstruction bitcast:
                        EmitTypedValue (bitcast.Input);
                        break;
                    case IR.CallInstruction call:
                        EmitCall (call);
                        break;
                    case IR.ConditionalBrInstruction cbr:
                        EmitBrtrue (cbr.Condition, Types.IntegerType.I1, GetLabel (cbr.IfTrue));
                        if (cbr.IfFalse.Symbol != nextBlock?.Symbol)
                            Emit (il.Create (OpCodes.Br, GetLabel (cbr.IfFalse)));
                        break;
                    case IR.ExtractElementInstruction ee: {
                            EmitTypedValue (ee.Value);
                            var index = ((IR.Constant)ee.Index.Value).Int32Value;
                            var v = GetVectorType ((VectorType)ee.Value.Type);
                            var field = v.ElementFields[index];
                            Emit (il.Create (OpCodes.Ldfld, field));
                        }
                        break;
                    case IR.FaddInstruction add:
                        if (add.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Add, add.Op1, add.Op2, (Types.VectorType)add.Type);
                        }
                        else {
                            EmitValue (add.Op1, add.Type);
                            EmitValue (add.Op2, add.Type);
                            Emit (il.Create (OpCodes.Add));
                        }
                        break;
                    case IR.FcmpInstruction fcmp:
                        EmitValue (fcmp.Op1, fcmp.Type);
                        EmitValue (fcmp.Op2, fcmp.Type);
                        switch (fcmp.Condition) {
                            case IR.FcmpCondition.OrderedEqual:
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.OrderedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt));
                                break;
                            case IR.FcmpCondition.OrderedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.OrderedLessThan:
                                Emit (il.Create (OpCodes.Clt));
                                break;
                            case IR.FcmpCondition.OrderedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.UnorderedNotEqual:
                                Emit (il.Create (OpCodes.Ceq));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.UnorderedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                break;
                            case IR.FcmpCondition.UnorderedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt_Un));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.UnorderedLessThan:
                                Emit (il.Create (OpCodes.Clt_Un));
                                break;
                            case IR.FcmpCondition.UnorderedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            default:
                                throw new NotSupportedException ("fcmp condition " + fcmp.Condition);
                        }
                        break;
                    case IR.FdivInstruction add:
                        if (add.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Div, add.Op1, add.Op2, (Types.VectorType)add.Type);
                        }
                        else {
                            EmitValue (add.Op1, add.Type);
                            EmitValue (add.Op2, add.Type);
                            Emit (il.Create (OpCodes.Div));
                        }
                        break;
                    case IR.FmulInstruction fmul:
                        if (fmul.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Mul, fmul.Op1, fmul.Op2, (Types.VectorType)fmul.Type);
                        }
                        else {
                            EmitValue (fmul.Op1, fmul.Type);
                            EmitValue (fmul.Op2, fmul.Type);
                            Emit (il.Create (OpCodes.Mul));
                        }
                        break;
                    case IR.FptosiInstruction sext:
                        EmitTypedValue (sext.Value);
                        switch (sext.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_I2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_I4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_I8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot fptoui {sext.Type}");
                        }
                        break;
                    case IR.FptouiInstruction sext:
                        EmitTypedValue (sext.Value);
                        switch (sext.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_U1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_U2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_U4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_U8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot fptoui {sext.Type}");
                        }
                        break;
                    case IR.FsubInstruction fsub:
                        if (fsub.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Sub, fsub.Op1, fsub.Op2, (Types.VectorType)fsub.Type);
                        }
                        else {
                            EmitValue (fsub.Op1, fsub.Type);
                            EmitValue (fsub.Op2, fsub.Type);
                            Emit (il.Create (OpCodes.Sub));
                        }
                        break;
                    case IR.GetElementPointerInstruction gep:
                        EmitGetElementPointer (gep.Pointer, gep.Indices);
                        break;
                    case IR.IcmpInstruction icmp:
                        EmitValue (icmp.Op1, icmp.Type);
                        EmitValue (icmp.Op2, icmp.Type);
                        switch (icmp.Condition) {
                            case IR.IcmpCondition.Equal:
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.IcmpCondition.NotEqual:
                                Emit (il.Create (OpCodes.Ceq));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt_Un));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.IcmpCondition.UnsignedLessThan:
                                Emit (il.Create (OpCodes.Clt_Un));
                                break;
                            case IR.IcmpCondition.UnsignedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.IcmpCondition.SignedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt));
                                break;
                            case IR.IcmpCondition.SignedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.IcmpCondition.SignedLessThan:
                                Emit (il.Create (OpCodes.Clt));
                                break;
                            case IR.IcmpCondition.SignedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                        }
                        break;
                    case IR.InsertElementInstruction insertElement:
                        EmitTypedValue (insertElement.Value);
                        break;
                    case IR.LoadInstruction load:
                        EmitLoad (load);
                        break;
                    case IR.LshrInstruction lshr:
                        EmitValue (lshr.Op1, lshr.Type);
                        EmitValue (lshr.Op2, lshr.Type);
                        Emit (il.Create (OpCodes.Shr_Un));
                        break;
                    case IR.MultiplyInstruction mul:
                        EmitValue (mul.Op1, mul.Type);
                        EmitValue (mul.Op2, mul.Type);
                        Emit (il.Create (OpCodes.Mul));
                        break;
                    case IR.OrInstruction or:
                        EmitValue (or.Op1, or.Type);
                        EmitValue (or.Op2, or.Type);
                        Emit (il.Create (OpCodes.Or));
                        break;
                    case IR.PhiInstruction phi:
                        Emit (il.Create (OpCodes.Ldloc, GetPhiLocal (assignedSymbol)));
                        break;
                    case IR.PtrtointInstruction zext:
                        EmitTypedValue (zext.Value);
                        switch (zext.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_I2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_I4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_I8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot ptrtoint {zext.Type}");
                        }
                        break;
                    case IR.RetInstruction ret:
                        EmitTypedValue (ret.Value);
                        Emit (il.Create (OpCodes.Ret));
                        break;
                    case IR.SdivInstruction sdiv:
                        if (sdiv.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Div, sdiv.Op1, sdiv.Op2, (Types.VectorType)sdiv.Type);
                        }
                        else {
                            EmitValue (sdiv.Op1, sdiv.Type);
                            EmitValue (sdiv.Op2, sdiv.Type);
                            Emit (il.Create (OpCodes.Div));
                        }
                        break;
                    case IR.SextInstruction sext:
                        EmitTypedValue (sext.Value);
                        switch (sext.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_I2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_I4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_I8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot sext {sext.Type}");
                        }
                        break;
                    case IR.SelectInstruction sel: {
                            var end = il.Create (OpCodes.Nop);
                            var trueI = il.Create (OpCodes.Nop);

                            EmitBrtrue (sel.Condition, sel.Type, trueI);

                            EmitTypedValue (sel.Value2);
                            Emit (il.Create (OpCodes.Br, end));

                            Emit (trueI);
                            EmitTypedValue (sel.Value1);

                            Emit (end);
                        }
                        break;
                    case IR.ShlInstruction shl:
                        EmitValue (shl.Op1, shl.Type);
                        EmitValue (shl.Op2, shl.Type);
                        Emit (il.Create (OpCodes.Shl));
                        break;
                    case IR.ShuffleVectorInstruction sh: {
                            var type1 = (VectorType)sh.Value1.Type;
                            var type2 = (VectorType)sh.Value2.Type;
                            var len1 = type1.Length;
                            var ctype1 = GetVectorType (type1);
                            var ctype2 = GetVectorType (type2);
                            var crt = GetVectorType (sh.Type);
                            var local1 = GetVectorTempVariable (ctype1, sh.Value1.Value, 0);
                            var local2 = GetVectorTempVariable (ctype2, sh.Value2.Value, 0);
                            foreach (var c in ((IR.VectorConstant)sh.Mask.Value).Constants) {
                                var index = c.Constant.Int32Value;
                                var loc = index >= len1 ? local2 : local1;
                                var loci = index >= len1 ? index - len1 : index;
                                var typ = index >= len1 ? ctype2 : ctype1;
                                Emit (il.Create (OpCodes.Ldloc, loc));
                                Emit (il.Create (OpCodes.Ldfld, typ.ElementFields[loci]));
                            }
                            Emit (il.Create (OpCodes.Newobj, crt.Ctor));
                        }
                        break;
                    case IR.SitofpInstruction sitofp:
                        EmitTypedValue (sitofp.Value);
                        switch (sitofp.Type) {
                            case Types.FloatType fltt:
                                switch (fltt.Bits) {
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_R4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_R8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot sitofp {sitofp.Type}");
                        }
                        break;
                    case IR.StoreInstruction store:
                        EmitStore (store);
                        break;
                    case IR.SubInstruction sub:
                        EmitValue (sub.Op1, sub.Type);
                        EmitValue (sub.Op2, sub.Type);
                        Emit (il.Create (OpCodes.Sub));
                        break;
                    case IR.SwitchInstruction sw:
                        EmitSwitch (sw, nextBlock);
                        break;
                    case IR.TruncInstruction trunc:
                        EmitTypedValue (trunc.Value);
                        switch (trunc.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_I2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_I4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_I8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot trunc {trunc.Type}");
                        }
                        break;
                    case IR.UitofpInstruction uitofp:
                        EmitTypedValue (uitofp.Value);
                        switch (uitofp.Type) {
                            case Types.FloatType fltt:
                                switch (fltt.Bits) {
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_R4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_R8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot uitofp {uitofp.Type}");
                        }
                        break;
                    case IR.UnconditionalBrInstruction br:
                        if (br.Destination.Symbol != nextBlock?.Symbol)
                            Emit (il.Create (OpCodes.Br, GetLabel (br.Destination)));
                        break;
                    case IR.UremInstruction urem:
                        if (urem.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Rem_Un, urem.Op1, urem.Op2, (Types.VectorType)urem.Type);
                        }
                        else {
                            EmitValue (urem.Op1, urem.Type);
                            EmitValue (urem.Op2, urem.Type);
                            Emit (il.Create (OpCodes.Rem_Un));
                        }
                        break;
                    case IR.XorInstruction xor:
                        if (xor.Type is Types.VectorType) {
                            EmitVectorOp (OpCodes.Xor, xor.Op1, xor.Op2, (Types.VectorType)xor.Type);
                        }
                        else {
                            EmitValue (xor.Op1, xor.Type);
                            EmitValue (xor.Op2, xor.Type);
                            Emit (il.Create (OpCodes.Xor));
                        }
                        break;
                    case IR.ZextInstruction zext:
                        EmitTypedValue (zext.Value);
                        switch (zext.Type) {
                            case Types.IntegerType intt:
                                switch (intt.Bits) {
                                    case 1:
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_U1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_U2));
                                        break;
                                    case 32:
                                        Emit (il.Create (OpCodes.Conv_U4));
                                        break;
                                    default:
                                        Emit (il.Create (OpCodes.Conv_U8));
                                        break;
                                }
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot zext {zext.Type}");
                        }
                        break;
                    default:
                        throw new NotImplementedException (instruction.ToString ());
                }
            }

            void EmitTypedValue (IR.TypedValue value)
            {
                EmitValue (value.Value, value.Type);
            }

            void EmitTypedValuePointer (IR.TypedValue value)
            {
                EmitValuePointer (value.Value, value.Type);
            }

            void EmitValue (IR.Value value, LType type)
            {
                switch (value) {
                    case IR.BooleanConstant b:
                        Emit (il.Create (b.IsTrue ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
                        break;
                    case IR.FloatConstant flt:
                        Emit (il.Create (
                            ((FloatType)type).Bits == 64 ? OpCodes.Ldc_R8 : OpCodes.Ldc_R4,
                            flt.Value));
                        break;
                    case IR.GetElementPointerValue gep:
                        EmitGetElementPointer (gep.Pointer, gep.Indices);
                        break;
                    case IR.GlobalValue g:
                        if (compilation.TryGetFunction (g.Symbol, out var ff)) {
                            Emit (il.Create (OpCodes.Ldftn, ff.ILDefinition));
                        }
                        else {
                            throw new NotSupportedException ($"Cannot emit value {g}");
                        }
                        break;
                    case IR.HexIntegerConstant i:
                        if (type is FloatType fltt) {
                            var ba = new byte[8];
                            var da = new double[1];
                            ba[7] = (byte)((i.Value >> 56) & 0xFF);
                            ba[6] = (byte)((i.Value >> 48) & 0xFF);
                            ba[5] = (byte)((i.Value >> 40) & 0xFF);
                            ba[4] = (byte)((i.Value >> 32) & 0xFF);
                            ba[3] = (byte)((i.Value >> 24) & 0xFF);
                            ba[2] = (byte)((i.Value >> 16) & 0xFF);
                            ba[1] = (byte)((i.Value >> 8) & 0xFF);
                            ba[0] = (byte)((i.Value >> 0) & 0xFF);
                            Buffer.BlockCopy (ba, 0, da, 0, 8);
                            switch (fltt.Bits) {
                                case 32:
                                    Emit (il.Create (OpCodes.Ldc_R4, (float)da[0]));
                                    break;
                                case 64:
                                    Emit (il.Create (OpCodes.Ldc_R8, da[0]));
                                    break;
                                default:
                                    throw new NotSupportedException ($"{((IntegerType)type).Bits}-bit float integers");
                            }
                        }
                        else {
                            switch (((IntegerType)type).Bits) {
                                case 8:
                                    Emit (il.Create (OpCodes.Ldc_I4, ((int)i.Value) & 0xFF));
                                    break;
                                case 16:
                                    Emit (il.Create (OpCodes.Ldc_I4, ((int)i.Value) & 0xFFFF));
                                    break;
                                case 32:
                                    Emit (il.Create (OpCodes.Ldc_I4, (int)i.Value));
                                    break;
                                case 64:
                                    Emit (il.Create (OpCodes.Ldc_I8, (long)i.Value));
                                    break;
                                default:
                                    throw new NotSupportedException ($"{((IntegerType)type).Bits}-bit integers");
                            }
                        }
                        break;
                    case IR.IntegerConstant i:
                        switch (((IntegerType)type).Bits) {
                            case 8:
                                Emit (il.Create (OpCodes.Ldc_I4, ((int)i.Value) & 0xFF));
                                break;
                            case 16:
                                Emit (il.Create (OpCodes.Ldc_I4, ((int)i.Value) & 0xFFFF));
                                break;
                            case 32:
                                Emit (il.Create (OpCodes.Ldc_I4, (int)i.Value));
                                break;
                            case 64:
                                Emit (il.Create (OpCodes.Ldc_I8, (long)i.Value));
                                break;
                            default:
                                throw new NotSupportedException ($"{((IntegerType)type).Bits}-bit integers");
                        }
                        break;
                    case IR.IntToPointerValue itop:
                        EmitTypedValue (itop.Value);
                        Emit (il.Create (OpCodes.Conv_U));
                        break;
                    case IR.LocalValue local:
                        if (locals.TryGetValue (local.Symbol, out var vd)) {
                            Emit (il.Create (OpCodes.Ldloc, vd));
                        }
                        else {
                            if (paramSyms.TryGetValue (local.Symbol, out var pd)) {
                                Emit (il.Create (OpCodes.Ldarg, pd));
                            }
                            else {
                                var a = f.GetAssignment (local);
                                EmitInstruction (a.Result, a.Instruction, null);
                            }
                        }
                        break;
                    case IR.NullConstant nll:
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        Emit (il.Create (OpCodes.Conv_U));
                        break;
                    case IR.UndefinedConstant und:
                        EmitZeroValue (type);
                        break;
                    case IR.VectorConstant vec:
                        foreach (var c in vec.Constants) {
                            EmitValue (c.Constant, c.Type);
                        } {
                            var vt = GetVectorType ((VectorType)type);
                            Emit (il.Create (OpCodes.Newobj, vt.Ctor));
                        }
                        break;
                    case IR.VoidValue vv:
                        // Should only be used in returns
                        break;
                    case IR.ZeroConstant zero:
                        EmitZeroValue (type);
                        break;
                    default:
                        throw new NotSupportedException ($"Cannot emit value {value} ({value?.GetType()?.Name}) with type {type}");
                }
            }

            void EmitValuePointer (IR.Value value, LType type)
            {
                if (value is IR.GlobalValue g) {
                    Emit (il.Create (OpCodes.Ldsflda, compilation.GetGlobal (g.Symbol)));
                    Emit (il.Create (OpCodes.Conv_U));
                }
                else {
                    EmitValue (value, type);
                }
            }

            void EmitStore (IR.StoreInstruction store)
            {
                // Shortcut Store Field
                if (store.Pointer.Value is IR.LocalValue pointerLocal
                    && ShouldInline (pointerLocal.Symbol)) {

                    var pointerInst = f.FindAssignment (pointerLocal)?.Instruction;
                    if (pointerInst is IR.GetElementPointerInstruction gep
                        && gep.Indices.Length == 2
                        && gep.Indices[1].Value is IR.Constant indexConst
                        && gep.Pointer.Type is Types.PointerType gepPointerType
                        && gepPointerType.ElementType.Resolve (function.IRModule) is LiteralStructureType structType) {

                        var td = GetClrType (gepPointerType.ElementType).Resolve ();
                        var field = td.Fields[indexConst.Int32Value];

                        EmitTypedValue (gep.Pointer);
                        EmitTypedValue (store.Value);
                        Emit (il.Create (OpCodes.Stfld, field));
                        return;
                    }
                }

                EmitTypedValue (store.Pointer);
                EmitTypedValue (store.Value);
                var et = GetClrType (store.Value.Type);
                if (store.Value.Type is IntegerType intt) {
                    switch (intt.Bits) {
                        case 8:
                            Emit (il.Create (OpCodes.Stind_I1));
                            break;
                        case 16:
                            Emit (il.Create (OpCodes.Stind_I2));
                            break;
                        case 32:
                            Emit (il.Create (OpCodes.Stind_I4));
                            break;
                        case 64:
                            Emit (il.Create (OpCodes.Stind_I8));
                            break;
                        default:
                            Emit (il.Create (OpCodes.Stobj, et));
                            break;
                    }
                }
                else if (store.Value.Type is FloatType fltt) {
                    switch (fltt.Bits) {
                        case 32:
                            Emit (il.Create (OpCodes.Stind_R4));
                            break;
                        default:
                            Emit (il.Create (OpCodes.Stind_R8));
                            break;
                    }
                }
                else {
                    Emit (il.Create (OpCodes.Stobj, et));
                }
            }

            void EmitLoad (IR.LoadInstruction load)
            {
                // Shortcut Load Field
                if (load.Pointer.Value is IR.LocalValue pointerLocal
                    && ShouldInline (pointerLocal.Symbol)) {

                    var pointerInst = f.FindAssignment (pointerLocal)?.Instruction;
                    if (pointerInst is IR.GetElementPointerInstruction gep
                        && gep.Indices.Length == 2
                        && gep.Indices[1].Value is IR.Constant indexConst
                        && gep.Pointer.Type is Types.PointerType gepPointerType
                        && gepPointerType.ElementType.Resolve (function.IRModule) is LiteralStructureType structType) {

                        var td = GetClrType (gepPointerType.ElementType).Resolve ();
                        var field = td.Fields[indexConst.Int32Value];

                        EmitTypedValue (gep.Pointer);
                        Emit (il.Create (OpCodes.Ldfld, field));
                        return;
                    }
                }

                EmitTypedValue (load.Pointer);

                var et = GetClrType (load.Type);
                if (load.Type is IntegerType intt) {
                    switch (intt.Bits) {
                        case 8:
                            Emit (il.Create (OpCodes.Ldind_I1));
                            break;
                        case 16:
                            Emit (il.Create (OpCodes.Ldind_I2));
                            break;
                        case 32:
                            Emit (il.Create (OpCodes.Ldind_I4));
                            break;
                        case 64:
                            Emit (il.Create (OpCodes.Ldind_I8));
                            break;
                        default:
                            Emit (il.Create (OpCodes.Ldobj, et));
                            break;
                    }
                }
                else if (load.Type is FloatType fltt) {
                    switch (fltt.Bits) {
                        case 32:
                            Emit (il.Create (OpCodes.Ldind_R4));
                            break;
                        default:
                            Emit (il.Create (OpCodes.Ldind_R8));
                            break;
                    }
                }
                else if (load.Type is Types.PointerType pt) {
                    Emit (il.Create (OpCodes.Ldind_I));
                }
                else {
                    Emit (il.Create (OpCodes.Ldobj, et));
                }
            }

            void EmitSwitch (IR.SwitchInstruction sw, IR.Block nextBlock)
            {
                var rem = new List<IR.SwitchCase> (sw.Cases.OrderBy (x => x.Value.Constant.Int32Value));

                while (rem.Count > 0) {

                    var offset = rem[0].Value.Constant.Int32Value;

                    int nextValue = offset + 1;
                    int endIndex = 1;
                    while (endIndex < rem.Count && rem[endIndex].Value.Constant.Int32Value == nextValue) {
                        endIndex++;
                        nextValue++;
                    }

                    if (offset != 0) {
                        EmitValue (rem[0].Value.Constant, rem[0].Value.Type);
                    }
                    EmitTypedValue (sw.Value);

                    if (endIndex > 1) {
                        var labels =
                            rem.Take (endIndex)
                            .Select (x => GetLabel (x.Label))
                            .ToArray ();
                        if (offset != 0) {
                            Emit (il.Create (OpCodes.Sub));
                        }
                        Emit (il.Create (OpCodes.Switch, labels));
                        rem.RemoveRange (0, endIndex);
                    }
                    else {
                        var c = rem[0];
                        if (offset == 0) {
                            EmitValue (new IR.IntegerConstant (0), c.Value.Type);
                        }
                        Emit (il.Create (OpCodes.Beq, GetLabel (c.Label)));
                        rem.RemoveAt (0);
                    }
                }

                if (nextBlock.Symbol != sw.DefaultLabel.Symbol)
                    Emit (il.Create (OpCodes.Br, GetLabel (sw.DefaultLabel)));
            }

            VariableDefinition GetVectorTempVariable (SimdVector type, IR.Value value, int uid)
            {
                //
                // First check if this value is already stored into a local
                // If so, just use that.
                //
                if (value is IR.LocalValue lv && locals.TryGetValue (lv.Symbol, out var vd))
                    return vd;

                //
                // Ah, the value was inlined. Lookup/Allocate a register for it.
                //
                var key = (type.ClrType.FullName, uid);
                if (vectorTemps.TryGetValue (key, out vd))
                    return vd;


                vd = new VariableDefinition (type.ClrType);
                vectorTemps[key] = vd;
                body.Variables.Add (vd);

                var name = $"vectorTemp{vectorTemps.Count}";
                var dbg = new VariableDebugInformation (vd, name);
                vdbgs.Add (dbg);

                return vd;
            }

            void EmitVectorOp (OpCode op, IR.Value op1, IR.Value op2, Types.VectorType type)
            {
                EmitValue (op1, type);
                EmitValue (op2, type);
                var v = GetVectorType (type);
                if (op.Code == Code.Sub) {
                    Emit (il.Create (OpCodes.Call, v.Subtract));
                }
                else {
                    throw new NotSupportedException ($"Cannot {op.Code} {type}");
                }
            }

            void EmitBrtrue (IR.Value condition, LType conditionType, Instruction trueTarget)
            {
                if (condition is IR.LocalValue local && ShouldInline (local.Symbol)) {
                    var a = function.IRDefinition.GetAssignment (local);
                    if (a.Instruction is IR.IcmpInstruction icmp) {
                        var op = OpCodes.Brtrue;
                        switch (icmp.Condition) {
                            case IR.IcmpCondition.Equal:
                                op = OpCodes.Beq;
                                break;
                            case IR.IcmpCondition.NotEqual:
                                op = OpCodes.Bne_Un;
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThan:
                                op = OpCodes.Bgt_Un;
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThanOrEqual:
                                op = OpCodes.Bge_Un;
                                break;
                            case IR.IcmpCondition.UnsignedLessThan:
                                op = OpCodes.Blt_Un;
                                break;
                            case IR.IcmpCondition.UnsignedLessThanOrEqual:
                                op = OpCodes.Ble_Un;
                                break;
                            case IR.IcmpCondition.SignedGreaterThan:
                                op = OpCodes.Bgt;
                                break;
                            case IR.IcmpCondition.SignedGreaterThanOrEqual:
                                op = OpCodes.Bge;
                                break;
                            case IR.IcmpCondition.SignedLessThan:
                                op = OpCodes.Blt;
                                break;
                            case IR.IcmpCondition.SignedLessThanOrEqual:
                                op = OpCodes.Ble;
                                break;
                        }
                        EmitValue (icmp.Op1, icmp.Type);
                        EmitValue (icmp.Op2, icmp.Type);
                        Emit (il.Create (op, trueTarget));
                        return;
                    }
                }

                EmitValue (condition, conditionType);
                Emit (il.Create (OpCodes.Brtrue, trueTarget));
            }

            void EmitCall (IR.CallInstruction call)
            {
                if (call.Pointer is IR.GlobalValue gv) {
                    switch (gv.Symbol.Text) {
                        case "@llvm.lifetime.start.p0i8":
                        case "@llvm.lifetime.end.p0i8":
                        case "@llvm.dbg.declare":
                        case "@llvm.dbg.value":
                            return;
                        case "@llvm.fabs.f64":
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            Emit (il.Create (OpCodes.Call, compilation.sysMathAbsD));
                            return;
                        case "@llvm.sqrt.f64":
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            Emit (il.Create (OpCodes.Call, compilation.sysMathSqrtD));
                            return;
                        // declare void @llvm.memset.p0i8.i32(i8* <dest>, i8 <val>,
                        //                                    i32<len>, i1<isvolatile>)
                        case "@llvm.memset.p0i8.i32" when call.Arguments.Length >= 3:
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                            EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                            Emit (il.Create (OpCodes.Initblk));
                            return;
                        // declare void @llvm.memset.p0i8.i64 (i8 * < dest >, i8<val>,
                        //                                     i64<len>, i1<isvolatile>)
                        case "@llvm.memset.p0i8.i64" when call.Arguments.Length >= 3:
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                            EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                            Emit (il.Create (OpCodes.Conv_U4));
                            Emit (il.Create (OpCodes.Initblk));
                            return;
                        // declare void @llvm.memcpy.p0i8.p0i8.i32(i8* <dest>, i8* <src>,
                        //                                         i32 <len>, i1 <isvolatile>)
                        case "@llvm.memcpy.p0i8.p0i8.i32" when call.Arguments.Length >= 3:
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                            EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                            Emit (il.Create (OpCodes.Cpblk));
                            return;
                        // declare void @llvm.memcpy.p0i8.p0i8.i64(i8* <dest>, i8* <src>,
                        //                                         i64 <len>, i1 <isvolatile>)
                        case "@llvm.memcpy.p0i8.p0i8.i64" when call.Arguments.Length >= 3:
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                            EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                            Emit (il.Create (OpCodes.Conv_U4));
                            Emit (il.Create (OpCodes.Cpblk));
                            return;
                        default:
                            if (compilation.TryGetFunction (gv.Symbol, out var m)) {
                                foreach (var a in call.Arguments) {
                                    EmitValue (a.Value, a.Type);
                                }
                                Emit (il.Create (OpCodes.Call, m.ILDefinition));
                                return;
                            }
                            break;
                    }
                }
                else if (call.Pointer is IR.LocalValue lv) {
                    LType ltype;
                    if (paramSyms.TryGetValue (lv.Symbol, out var p)) {
                        ltype = f.Parameters.First (x => x.Symbol == lv.Symbol).ParameterType;
                    }
                    else {
                        var lva = f.GetAssignment (lv);
                        ltype = lva.Instruction.ResultType (function.IRModule);
                    }
                    foreach (var a in call.Arguments) {
                        EmitValue (a.Value, a.Type);
                    }
                    EmitValue (lv, ltype);
                    Emit (il.Create (OpCodes.Calli, CreateCallSite (ltype)));
                    return;
                }
                throw new NotSupportedException ("Cannot call " + call.Pointer);
            }

            void EmitGetElementPointer (IR.TypedValue pointer, IR.TypedValue[] indices)
            {
                var t = pointer.Type;
                EmitTypedValuePointer (pointer);
                var n = indices.Length;
                for (var i = 0; i < n; i++) {
                    var index = indices[i];
                    if (t is Types.PointerType pt) {
                        t = pt.ElementType;
                        if (index.Value is IR.Constant c && c.Int32Value == 0) {
                            // Do nothing
                        }
                        else {
                            var esize = t.GetByteSize (function.IRModule);
                            if (index.Value is IR.Constant ic) {
                                EmitValue (new IR.IntegerConstant (esize * ic.Int32Value), index.Type);
                            }
                            else {
                                EmitValue (index.Value, index.Type);
                                EmitValue (new IR.IntegerConstant (esize), index.Type);
                                Emit (il.Create (OpCodes.Mul));
                            }
                            Emit (il.Create (OpCodes.Conv_U));
                            Emit (il.Create (OpCodes.Add));
                        }
                    }
                    else if (t is NamedType
                             && t.Resolve (function.IRModule) is Types.LiteralStructureType st
                             && index.Value is IR.IntegerConstant iconst) {
                        var cst = GetClrType (t).Resolve ();
                        var field = cst.Fields[(int)iconst.Value];
                        Emit (il.Create (OpCodes.Ldflda, field));
                        Emit (il.Create (OpCodes.Conv_U));
                        t = st.Elements[i];
                    }
                    else if (t is Types.ArrayType artt) {
                        var esize = artt.ElementType.GetByteSize (function.IRModule);
                        EmitValue (index.Value, index.Type);
                        EmitValue (new IR.IntegerConstant (esize), index.Type);
                        Emit (il.Create (OpCodes.Mul));
                        Emit (il.Create (OpCodes.Conv_U));
                        Emit (il.Create (OpCodes.Add));
                        t = artt.ElementType;
                    }
                    else {

                        if (i + 1 < n) {
                            throw new NotSupportedException ("Cannot get pointer to " + t);
                        }

                    }
                }
            }

            CecilInstruction GetLabel (IR.LabelValue label)
            {
                return blockFirstInstr[label.Symbol];
            }
        }

        class SharedVariable
        {
            public readonly HashSet<LocalSymbol> Users =
                new HashSet<LocalSymbol> ();
            public VariableDefinition Variable;
            public TypeReference ClrType => Variable.VariableType;
        }
        readonly Dictionary<string, List<SharedVariable>> phiVariablesByType =
            new Dictionary<string, List<SharedVariable>> ();
        readonly Dictionary<string, List<SharedVariable>> sharedVariablesByType =
            new Dictionary<string, List<SharedVariable>> ();

        VariableDefinition GetFreeVariable (LocalSymbol symbol, TypeReference clrType, bool isPhi)
        {
            //
            // Get the right list
            //
            var types = isPhi ? phiVariablesByType : sharedVariablesByType;
            if (!types.TryGetValue (clrType.FullName, out var variables)) {
                variables = new List<SharedVariable> ();
                types[clrType.FullName] = variables;
            }

            //
            // Has it already been assigned?
            //
            foreach (var v in variables) {
                if (v.Users.Contains (symbol))
                    return v.Variable;
            }

            //
            // Find an existing variable with no interference
            //
            SharedVariable variable = null;
            foreach (var v in variables) {
                var interferes = liveliness.VariablesInterfere (symbol, v.Users);
                if (!interferes) {
                    variable = v;
                    break;
                }
            }

            //
            // If we didn't find one, create one
            //
            if (variable == null) {
                var vd = new VariableDefinition (clrType);
                variable = new SharedVariable {
                    Variable = vd,
                };
                body.Variables.Add (vd);
                variables.Add (variable);
            }
            variable.Users.Add (symbol);
            return variable.Variable;
        }

        void EmitZeroValue (LType type)
        {
            if (type is VectorType vt) {
                var v = GetVectorType (vt);
                for (var i = 0; i < vt.Length; i++) {
                    EmitZeroValue (vt.ElementType);
                }
                Emit (il.Create (OpCodes.Newobj, v.Ctor));
            }
            else if (type is Types.IntegerType intt) {
                Emit (il.Create (OpCodes.Ldc_I4_0));
            }
            else if (type is Types.PointerType ptr) {
                Emit (il.Create (OpCodes.Ldc_I4_0));
                Emit (il.Create (OpCodes.Conv_U));
            }
            else if (type is Types.FloatType floatt) {
                if (floatt.Bits == 32) {
                    Emit (il.Create (OpCodes.Ldc_R4, 0.0f));
                }
                else {
                    Emit (il.Create (OpCodes.Ldc_R8, 0.0));
                }
            }
            else {
                throw new NotSupportedException ("Cannot get 0 for " + type);
            }
        }

        void Emit (CecilInstruction i)
        {
            il.InsertAfter (prev, i);
            prev = i;
        }

        CallSite CreateCallSite (LType functionPointerType)
        {
            var ft = (FunctionType)((Types.PointerType)functionPointerType).ElementType;
            var c = new CallSite (GetClrType (ft.ReturnType));
            foreach (var p in ft.ParameterTypes) {
                var pd = new ParameterDefinition (GetClrType (p));
                c.Parameters.Add (pd);
            }
            return c;
        }

        SimdVector GetVectorType (VectorType vt) => compilation.GetVectorType (vt);

        TypeReference GetClrType (LType irType, bool? unsigned = false) => compilation.GetClrType (irType, unsigned);
    }
}
