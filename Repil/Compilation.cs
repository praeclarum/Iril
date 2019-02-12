using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Repil.Types;
using Mono.Cecil.Rocks;
using Mono.Cecil.Cil;

using CecilInstruction = Mono.Cecil.Cil.Instruction;

namespace Repil
{
    public class Compilation
    {
        public readonly Module[] Modules;
        public readonly string AssemblyName;
        public readonly string SystemAssemblyPath;

        readonly ModuleDefinition mod;
        readonly AssemblyDefinition asm;
        readonly string namespac;

        readonly HashSet<string> structNames = new HashSet<string> ();
        readonly SymbolTable<(LiteralStructureType, TypeDefinition)> structs =
            new SymbolTable<(LiteralStructureType, TypeDefinition)> ();

        AssemblyDefinition sysAsm;
        TypeReference sysVoid;
        TypeReference sysObj;
        TypeReference sysVal;
        TypeReference sysByte;
        TypeReference sysInt16;
        TypeReference sysInt32;
        TypeReference sysInt64;
        TypeReference sysSingle;
        TypeReference sysDouble;

        readonly Dictionary<(int, string), (TypeReference, MethodReference)> vectorTypes =
            new Dictionary<(int, string), (TypeReference, MethodReference)> ();

        public Compilation (IEnumerable<Module> documents, string assemblyName)
        {
            Modules = documents.ToArray ();
            AssemblyName = assemblyName;
            SystemAssemblyPath = typeof(object).Assembly.Location;
            var version = new Version (1, 0);
            var asmName = new AssemblyNameDefinition (Path.GetFileNameWithoutExtension (AssemblyName), version);
            var modName = AssemblyName;
            namespac = asmName.Name;
            asm = AssemblyDefinition.CreateAssembly (asmName, modName, ModuleKind.Dll);
            mod = asm.MainModule;

            Compile ();
        }

        void Compile ()
        {
            FindSystemTypes ();
            CompileStructures ();
            CompileFunctions ();
        }

        class Resolver : IAssemblyResolver
        {
            public List<string> Directories = new List<string> ();

            public void Dispose ()
            {
            }

            public AssemblyDefinition Resolve (AssemblyNameReference name)
            {
                return Resolve (name, new ReaderParameters {
                    AssemblyResolver = this,
                });
            }

            public AssemblyDefinition Resolve (AssemblyNameReference name, ReaderParameters parameters)
            {
                var fname = name.Name;
                foreach (var d in Directories) {
                    var path = Path.Combine (d, fname + ".dll");
                    if (File.Exists (path)) {
                        return AssemblyDefinition.ReadAssembly (path, parameters);
                    }
                }
                return null;
            }
        }

        void FindSystemTypes ()
        {
            var dir = Path.GetDirectoryName (SystemAssemblyPath);
            var netstdPath = Path.Combine (dir, "netstandard.dll");
            var resolver = new Resolver ();
            resolver.Directories.Add (dir);
            var rps = new ReaderParameters (ReadingMode.Deferred) {
                AssemblyResolver = resolver
            };
            sysAsm = AssemblyDefinition.ReadAssembly (netstdPath, rps);
            var types = sysAsm.MainModule.ExportedTypes;
            var scope = sysAsm.MainModule.Types.First ().Scope;
            TypeReference Import (string name)
            {
                var et = types.First (x =>
                    x.FullName == name);
                //var t = et.Resolve ();
                var t = new TypeReference (et.Namespace, et.Name, sysAsm.MainModule, scope);
                return mod.ImportReference (t);
            }
            sysObj = Import ("System.Object");
            sysVal = Import ("System.ValueType");
            sysByte = Import ("System.Byte");
            sysInt16 = Import ("System.Int16");
            sysInt32 = Import ("System.Int32");
            sysInt64 = Import ("System.Int64");
            sysSingle = Import ("System.Single");
            sysDouble = Import ("System.Double");
            sysVoid = Import ("System.Void");
        }

        void CompileStructures ()
        {
            foreach (var m in Modules) {
                foreach (var iskv in m.IdentifiedStructures) {
                    if (iskv.Value is LiteralStructureType l) {

                        var tname = iskv.Key.Text.Substring (1).Split ('.').Last ();

                        var td = new TypeDefinition (namespac, tname, TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public, sysVal);

                        var fields =
                            from e in l.Elements.Zip(Enumerable.Range(0, l.Elements.Length), (e, i) => (e, i))
                            let fn = "F" + e.i
                            select new FieldDefinition (fn, FieldAttributes.Public, GetClrType(e.e));

                        foreach (var f in fields) {
                            td.Fields.Add (f);
                        }

                        mod.Types.Add (td);
                        structNames.Add (tname);
                        structs[iskv.Key] = (l, td);
                    }
                }
            }
        }

        void CompileFunctions ()
        {
            var funcstd = new TypeDefinition (namespac, "Functions", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            mod.Types.Add (funcstd);

            foreach (var m in Modules) {
                foreach (var iskv in m.FunctionDefinitions) {
                    var md = CompileFunction (iskv.Key, iskv.Value);
                    funcstd.Methods.Add (md);
                }
            }
        }

        MethodDefinition CompileFunction (Symbol functionSymbol, IR.FunctionDefinition functionDefinition)
        {
            var f = functionDefinition;
            var tname = functionSymbol.Text.Substring (1).Split ('.').Last ();

            var md = new MethodDefinition (tname, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static, GetClrType (f.ReturnType));

            //
            // Create parameters
            //
            var paramSyms = new SymbolTable<ParameterDefinition> ();
            for (var i = 0; i < f.Parameters.Length; i++) {
                var fp = f.Parameters[i];
                var pname = "p" + i;
                var pt = GetClrType (fp.ParameterType);
                var p = new ParameterDefinition (pname, ParameterAttributes.None, pt);
                md.Parameters.Add (p);
                paramSyms[fp.Symbol] = p;
            }

            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            //
            // Create phi locals
            //
            var phiLocals = new SymbolTable<VariableDefinition> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (a.HasResult && a.Instruction is IR.PhiInstruction phi) {
                        var local = new VariableDefinition (GetClrType (phi.Type));
                        phiLocals[a.Result] = local;
                        body.Variables.Add (local);
                    }
                }
            }

            //
            // Get local usage count
            //
            var localCounts = new SymbolTable<int> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    foreach (var l in a.Instruction.ReferencedLocals) {
                        if (localCounts.ContainsKey (l)) {
                            localCounts[l]++;
                        }
                        else {
                            localCounts[l] = 1;
                        }
                    }
                }
            }

            //
            // Create variables for locals referenced multiple times
            //
            var locals = new SymbolTable<VariableDefinition> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (a.Result != null) {
                        var l = a.Result;
                        if (localCounts.ContainsKey (l) && localCounts[l] > 1) {
                            var local = new VariableDefinition (GetClrType (a.Instruction.ResultType));
                            locals[a.Result] = local;
                            body.Variables.Add (local);
                        }
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
            // Emit the instructions
            //
            var prev = default (CecilInstruction);
            for (var i = 0; i < f.Blocks.Length; i++) {
                var b = f.Blocks[i];
                prev = blockFirstInstr[b.Symbol];
                foreach (var a in b.Assignments) {
                    if (!ShouldInline (a.Result)) {
                        EmitInstruction (a, i + 1 < f.Blocks.Length ? f.Blocks[i + 1] : null);
                    }
                }
            }

            md.Body = body;
            body.Optimize ();

            return md;

            void Emit (CecilInstruction i)
            {
                il.InsertAfter (prev, i);
                prev = i;
            }

            bool ShouldInline (LocalSymbol symbol)
            {
                return symbol != null && localCounts.ContainsKey (symbol) && localCounts[symbol] == 1;
            }

            VariableDefinition GetPhiLocal (IR.Assignment assignment)
            {
                return phiLocals[assignment.Result];
            }

            void EmitInstruction (IR.Assignment assignment, IR.Block nextBlock)
            {
                switch (assignment.Instruction) {
                    case IR.BitcastInstruction bitcast:
                        EmitTypedValue (bitcast.Input);
                        break;
                    case IR.CallInstruction call:
                        break;
                    case IR.ConditionalBrInstruction cbr:
                        EmitValue (cbr.Condition, cbr.ResultType);
                        Emit (il.Create (OpCodes.Brtrue, GetLabel (cbr.IfTrue)));
                        if (cbr.IfFalse.Symbol != nextBlock?.Symbol)
                            Emit (il.Create (OpCodes.Br, GetLabel (cbr.IfFalse)));
                        break;
                    case IR.GetElementPointerInstruction gep: {
                            var t = gep.Pointer.Type;
                            EmitTypedValue (gep.Pointer);
                            var n = gep.Indices.Length;
                            for (var i = 1; i < n; i++) {
                                var index = gep.Indices[i];
                                if (t is Types.PointerType pt && Resolve (pt.ElementType) is Types.StructureType st) {
                                    var cst = GetClrType (pt).GetElementType ().Resolve ();
                                    var field = cst.Fields[index];
                                    if (i == n - 1) {
                                        Emit (il.Create (OpCodes.Ldflda, field));
                                        Emit (il.Create (OpCodes.Conv_U));
                                    }
                                    else {
                                        throw new NotSupportedException ();
                                    }
                                }
                                else {
                                    throw new NotSupportedException ();
                                }
                            }
                        }
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
                                Emit (il.Create (OpCodes.Not));
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                break;
                            case IR.IcmpCondition.UnsignedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt_Un));
                                Emit (il.Create (OpCodes.Not));
                                break;
                            case IR.IcmpCondition.UnsignedLessThan:
                                Emit (il.Create (OpCodes.Clt_Un));
                                break;
                            case IR.IcmpCondition.UnsignedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt_Un));
                                Emit (il.Create (OpCodes.Not));
                                break;
                            case IR.IcmpCondition.SignedGreaterThan:
                                Emit (il.Create (OpCodes.Cgt));
                                break;
                            case IR.IcmpCondition.SignedGreaterThanOrEqual:
                                Emit (il.Create (OpCodes.Clt));
                                Emit (il.Create (OpCodes.Not));
                                break;
                            case IR.IcmpCondition.SignedLessThan:
                                Emit (il.Create (OpCodes.Clt));
                                break;
                            case IR.IcmpCondition.SignedLessThanOrEqual:
                                Emit (il.Create (OpCodes.Cgt));
                                Emit (il.Create (OpCodes.Not));
                                break;
                        }
                        break;
                    case IR.PhiInstruction phi:
                        Emit (il.Create (OpCodes.Ldloc, GetPhiLocal (assignment)));
                        break;
                    case IR.RetInstruction ret:
                        EmitTypedValue (ret.Value);
                        Emit (il.Create (OpCodes.Ret));
                        break;
                    case IR.StoreInstruction store:
                        EmitTypedValue (store.Pointer);
                        EmitTypedValue (store.Value); {
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
                        break;
                    case IR.UnconditionalBrInstruction br:
                        Emit (il.Create (OpCodes.Br, GetLabel (br.Destination)));
                        break;
                    default:
                        throw new NotImplementedException (assignment.Instruction.ToString ());
                }
            }

            void EmitTypedValue (IR.TypedValue value)
            {
                EmitValue (value.Value, value.Type);
            }

            void EmitValue (IR.Value value, LType type)
            {
                switch (value) {
                    case IR.FloatConstant flt:
                        Emit (il.Create (
                            ((FloatType)type).Bits == 64 ? OpCodes.Ldc_R8 : OpCodes.Ldc_R4,
                            flt.Value));
                        break;
                    case IR.IntegerConstant i:
                        switch (((IntegerType)type).Bits) {
                            case 8:
                                Emit (il.Create (OpCodes.Ldc_I4, ((int)i.Value) & 0xFF));
                                break;
                            case 32:
                                Emit (il.Create (OpCodes.Ldc_I4, (int)i.Value));
                                break;
                            default:
                                throw new NotSupportedException ($"{((IntegerType)type).Bits}-bit integers");
                        }
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
                                EmitInstruction (f.GetAssignment (local), null);
                            }
                        }
                        break;
                    case IR.NullConstant nll:
                        Emit (il.Create (OpCodes.Ldnull));
                        break;
                    case IR.VectorConstant vec:
                        foreach (var c in vec.Constants) {
                            EmitValue (c.Constant, c.Type);
                        } {
                            var (vt, ctor) = GetVectorType ((VectorType)type);
                            Emit (il.Create (OpCodes.Newobj, ctor));
                        }
                        break;
                    default:
                        throw new NotImplementedException (value.ToString ());
                }
            }

            CecilInstruction GetLabel (IR.LabelValue label)
            {
                return blockFirstInstr[label.Symbol];
            }
        }

        LType Resolve (LType elementType)
        {
            if (elementType is NamedType nt) {
                return structs[nt.Symbol].Item1;
            }
            return elementType;
        }

        TypeReference GetClrType (LType e)
        {
            switch (e) {
                case FloatType floatt:
                    switch (floatt.Bits) {
                        case 32:
                            return sysSingle;
                        case 64:
                            return sysDouble;
                        default:
                            throw new NotSupportedException ($"{floatt.Bits}-bit floats not supported");
                    }
                case IntegerType intt:
                    switch (intt.Bits) {
                        case 8:
                            return sysByte;
                        case 16:
                            return sysInt16;
                        case 32:
                            return sysInt32;
                        case 64:
                            return sysInt64;
                        default:
                            throw new NotSupportedException ($"{intt.Bits}-bit integers not supported");
                    }
                case Repil.Types.PointerType pt when pt.ElementType is FunctionType ft:
                    return sysInt32.MakePointerType ();
                case Repil.Types.PointerType pt:
                    return GetClrType (pt.ElementType).MakePointerType ();
                case NamedType nt:
                    return structs[nt.Symbol].Item2;
                case VectorType vt: {
                        return GetVectorType (vt).Item1;
                    }
                case VoidType vdt:
                    return sysVoid;
                default:
                    throw new NotSupportedException ($"{e} not supported");
            }
        }

        private (TypeReference, MethodReference) GetVectorType (VectorType vt)
        {
            var et = GetClrType (vt.ElementType);
            var key = (vt.Length, et.FullName);
            if (vectorTypes.TryGetValue (key, out var vct)) {
                return vct;
            }
            return AddVectorType (key, et);
        }

        (TypeReference, MethodReference) AddVectorType ((int Length, string TypeFullName) key, TypeReference elementType)
        {
            var tname = $"Vector{key.Length}{elementType.Name}";

            var td = new TypeDefinition (namespac, tname, TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public, sysVal);

            for (var i = 0; i < key.Length; i++) {
                var f = new FieldDefinition ("E" + i, FieldAttributes.Public, elementType);
                td.Fields.Add (f);
            }

            var ctor = new MethodDefinition (".ctor", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, sysVoid);
            for (var i = 0; i < key.Length; i++) {
                var p = new ParameterDefinition ("e" + i, ParameterAttributes.None, elementType);
                ctor.Parameters.Add (p);
            }
            var cbody = new MethodBody (ctor);
            var il = cbody.GetILProcessor ();
            for (var i = 0; i < key.Length; i++) {
                il.Append (il.Create (OpCodes.Ldarg_0));
                il.Append (il.Create (OpCodes.Ldarg, i + 1));
                il.Append (il.Create (OpCodes.Stfld, td.Fields[i]));
            }
            il.Append (il.Create (OpCodes.Ret));
            cbody.Optimize ();
            ctor.Body = cbody;
            td.Methods.Add (ctor);

            mod.Types.Add (td);
            var r = ((TypeReference)td, (MethodReference)ctor);
            vectorTypes[key] = r;

            return r;
        }

        public void WriteAssembly (Stream output)
        {
            asm.Write (output);
        }
    }
}
