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
        readonly Resolver resolver = new Resolver ();

        readonly HashSet<string> structNames = new HashSet<string> ();
        readonly SymbolTable<(LiteralStructureType, TypeDefinition)> structs =
            new SymbolTable<(LiteralStructureType, TypeDefinition)> ();

        AssemblyDefinition sysAsm;
        TypeReference sysVoid;
        TypeReference sysVoidPtr;
        TypeReference sysObj;
        TypeReference sysVal;
        TypeReference sysBoolean;
        TypeReference sysByte;
        TypeReference sysInt16;
        TypeReference sysInt32;
        TypeReference sysInt64;
        TypeReference sysIntPtr;
        TypeReference sysSingle;
        TypeReference sysDouble;
        TypeReference sysString;
        TypeReference sysNotImpl;
        MethodReference sysNotImplCtor;
        TypeReference sysNotSupp;
        MethodReference sysNotSuppCtor;
        TypeReference sysMath;
        MethodReference sysMathAbsD;
        MethodReference sysMathSqrtD;
        TypeReference sysEventArgs;
        TypeReference sysIAsyncResult;
        TypeReference sysAsyncCallback;

        readonly Dictionary<(int, string), SimdVector> vectorTypes =
            new Dictionary<(int, string), SimdVector> ();

        readonly SymbolTable<DefinedFunction> methodDefs =
            new SymbolTable<DefinedFunction> ();

        readonly SymbolTable<FieldDefinition> globals =
            new SymbolTable<FieldDefinition> ();

        public Compilation (IEnumerable<Module> documents, string assemblyName)
        {
            Modules = documents.ToArray ();
            AssemblyName = assemblyName;
            SystemAssemblyPath = typeof(object).Assembly.Location;
            var version = new Version (1, 0);
            var asmName = new AssemblyNameDefinition (Path.GetFileNameWithoutExtension (AssemblyName), version);
            var modName = AssemblyName;
            namespac = asmName.Name;
            var mps = new ModuleParameters {
                AssemblyResolver = resolver,
                Kind = ModuleKind.Dll,
            };
            asm = AssemblyDefinition.CreateAssembly (asmName, modName, mps);
            mod = asm.MainModule;
            Compile ();
        }

        void Compile ()
        {
            FindSystemTypes ();
            CompileStructures ();
            EmitGlobalVariables ();
            FindFunctions ();
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
            MethodReference ImportMethod (TypeReference declType, string name, params TypeReference[] argTypes)
            {
                var td = declType.Resolve ();
                var ms = td.Methods.Where (x => x.Name == name);
                foreach (var m in ms) {
                    if (m.Parameters.Count != argTypes.Length)
                        continue;
                    var match = true;
                    for (var i = 0; match && i < m.Parameters.Count; i++) {
                        var p = m.Parameters[i];
                        if (p.ParameterType.FullName != argTypes[i].FullName)
                            match = false;
                    }
                    if (match) {
                        var mr = new MethodReference (name, Import (m.ReturnType.FullName), declType);
                        mr.ExplicitThis = m.ExplicitThis;
                        mr.CallingConvention = m.CallingConvention;
                        mr.HasThis = m.HasThis;
                        foreach (var p in m.Parameters) {
                            mr.Parameters.Add (new ParameterDefinition (Import (p.ParameterType.FullName)));
                        }
                        var imr = mod.ImportReference (mr);
                        return imr;
                    }
                }
                throw new Exception ($"Cannot find {name} in {declType}");
            }
            sysObj = Import ("System.Object");
            sysVal = Import ("System.ValueType");
            sysByte = Import ("System.Byte");
            sysBoolean = Import ("System.Boolean");
            sysInt16 = Import ("System.Int16");
            sysInt32 = Import ("System.Int32");
            sysInt64 = Import ("System.Int64");
            sysIntPtr = Import ("System.IntPtr");
            sysSingle = Import ("System.Single");
            sysDouble = Import ("System.Double");
            sysVoid = Import ("System.Void");
            sysVoidPtr = sysVoid.MakePointerType ();
            sysString = Import ("System.String");
            sysNotImpl = Import ("System.NotImplementedException");
            sysNotImplCtor = new MethodReference (".ctor", sysVoid, sysNotImpl);
            sysNotSupp = Import ("System.NotSupportedException");
            sysNotSuppCtor = new MethodReference (".ctor", sysVoid, sysNotSupp);
            sysNotSuppCtor.Parameters.Add (new ParameterDefinition (sysString));
            sysMath = Import ("System.Math");
            sysMathAbsD = ImportMethod (sysMath, "Abs", sysDouble);
            sysMathSqrtD = ImportMethod (sysMath, "Sqrt", sysDouble);
            sysEventArgs = Import ("System.EventArgs");
            sysIAsyncResult = Import ("System.IAsyncResult");
            sysAsyncCallback = Import ("System.AsyncCallback");
        }

        void CompileStructures ()
        {
            foreach (var m in Modules) {
                foreach (var iskv in m.IdentifiedStructures) {

                    if (structs.ContainsKey (iskv.Key))
                        continue;

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

        void EmitGlobalVariables ()
        {
            if (!Modules.Any (m => m.GlobalVariables.Count > 0))
                return;

            var gstd = new TypeDefinition (namespac, "Globals", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            mod.Types.Add (gstd);

            foreach (var m in Modules) {
                foreach (var kv in m.GlobalVariables) {

                    var symbol = kv.Key;
                    var g = kv.Value;

                    var gname = symbol.Text.Substring (1).Split ('.').Last ();

                    var gtype = GetClrType (g.Type);
                    var field = new FieldDefinition (gname, FieldAttributes.Static | FieldAttributes.Public, gtype);

                    gstd.Fields.Add (field);
                    globals.Add (symbol, field);
                }
            }
        }

        class DefinedFunction
        {
            public Symbol Symbol;
            public Repil.Module IRModule;
            //public IR.FunctionDeclaration IRDeclaration;
            public IR.FunctionDefinition IRDefinition;
            public MethodDefinition ILDefinition;
            public SymbolTable<ParameterDefinition> ParamSyms;
        }

        void FindFunctions ()
        {
            var funcstd = new TypeDefinition (namespac, "Functions", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            mod.Types.Add (funcstd);

            foreach (var m in Modules) {
                foreach (var iskv in m.FunctionDefinitions) {
                    var f = iskv.Value;

                    //
                    // Load debug info
                    //
                    var dbgMeth = new SymbolTable<object> ();
                    if (f.MetaRefs.TryGetValue (MetaSymbol.Dbg, out var dbgSym)) {
                        if (m.Metadata.TryGetValue (dbgSym, out var d) && d is SymbolTable<object> s) {
                            dbgMeth = s;
                        }
                    }

                    //
                    // Create the method
                    //
                    var tname = iskv.Key.Text.Substring (1).Split ('.').Last ();
                    if (dbgMeth.TryGetValue (Symbol.Name, out var o)) {
                        tname = o.ToString ();
                    }

                    var md = new MethodDefinition (tname, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static, GetClrType (f.ReturnType));

                    //
                    // Create parameters
                    //
                    var dbgVars = Array.Empty<object> ();
                    if (dbgMeth.TryGetValue (Symbol.Variables, out o) && o is MetaSymbol) {
                        if (m.Metadata.TryGetValue ((Symbol)o, out o)) {
                            dbgVars = ((IEnumerable<object>)o).ToArray ();
                        }
                    }
                    var paramSyms = new SymbolTable<ParameterDefinition> ();
                    for (var i = 0; i < f.Parameters.Length; i++) {
                        var fp = f.Parameters[i];

                        var pname = "p" + i;

                        SymbolTable<object> dbgType = null;
                        if (i < dbgVars.Length && dbgVars[i] is MetaSymbol pdm) {
                            if (m.Metadata.TryGetValue (pdm, out o) && o is SymbolTable<object>) {
                                var pd = (SymbolTable<object>)o;
                                if (pd.TryGetValue (Symbol.Name, out o) && o is string) {
                                    pname = o.ToString ();
                                }
                                if (pd.TryGetValue (Symbol.Type, out o) && o is Symbol) {
                                    if (m.Metadata.TryGetValue ((Symbol)o, out o) && o is SymbolTable<object>) {
                                        dbgType = (SymbolTable<object>)o;
                                    }
                                }
                            }
                        }

                        var pt = GetParameterType (fp.ParameterType, m, dbgType);
                        var p = new ParameterDefinition (pname, ParameterAttributes.None, pt);
                        md.Parameters.Add (p);
                        paramSyms[fp.Symbol] = p;
                    }

                    funcstd.Methods.Add (md);

                    methodDefs[iskv.Key] = new DefinedFunction {
                        Symbol = iskv.Key,
                        IRModule = m,
                        IRDefinition = f,
                        ILDefinition = md,
                        ParamSyms = paramSyms,
                    };
                }
            }

            foreach (var m in Modules) {
                foreach (var iskv in m.FunctionDeclarations) {
                    if (iskv.Key.Text.StartsWith ("@llvm.", StringComparison.Ordinal))
                        continue;
                    if (methodDefs.ContainsKey (iskv.Key))
                        continue;

                    var f = iskv.Value;
                    var tname = iskv.Key.Text.Substring (1).Split ('.').Last ();

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

                    funcstd.Methods.Add (md);

                    methodDefs[iskv.Key] = new DefinedFunction {
                        Symbol = iskv.Key,
                        IRModule = m,
                        //IRDeclaration = f,
                        ILDefinition = md,
                        ParamSyms = paramSyms,
                    };
                }
            }
        }

        void CompileFunctions ()
        {
            foreach (var m in methodDefs) {
                if (m.Value.IRDefinition != null) {
                    try {
                        CompileFunction (m.Value);
                    }
                    catch (Exception ex) {
                        CompileFailedFunction (m.Value, ex);
                    }
                }
                else {
                    CompileMissingFunction (m.Value);
                }
            }
        }

        void CompileFailedFunction (DefinedFunction function, Exception ex)
        {
            var f = function.IRDefinition;

            var md = function.ILDefinition;
            md.Body = null;
            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, ex.ToString ()));
            il.Append (il.Create (OpCodes.Newobj, sysNotSuppCtor));
            il.Append (il.Create (OpCodes.Throw));

            body.Optimize ();
            md.Body = body;
        }

        void CompileMissingFunction (DefinedFunction function)
        {
            var f = function.IRDefinition;

            var md = function.ILDefinition;
            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            il.Append (il.Create (OpCodes.Newobj, sysNotImplCtor));
            il.Append (il.Create (OpCodes.Throw));

            body.Optimize ();
            md.Body = body;
        }

        void CompileFunction (DefinedFunction function)
        {
            var f = function.IRDefinition;

            var paramSyms = function.ParamSyms;
            var md = function.ILDefinition;
            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

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
            foreach (var b in f.Blocks) {
                for (var i = 0; i < b.Assignments.Length; i++) {
                    var a = b.Assignments[i];
                    var symbol = a.Result;
                    if (symbol == LocalSymbol.None)
                        continue;

                    // Make sure it's used only once
                    var referencedOnce = symbol != LocalSymbol.None && localCounts.ContainsKey (symbol) && localCounts[symbol] == 1;

                    // Make sure it's used as the next variable referenced
                    // by the next instruction
                    var should = false;
                    if (referencedOnce) {
                        var ni = i + 1 < b.Assignments.Length ? b.Assignments[i + 1].Instruction : b.Terminator;
                        var fv = ni.ReferencedLocals.FirstOrDefault ();
                        should = fv == symbol;
                    }
                    shouldInline.Add (symbol, should);
                }
            }

            //
            // Create variables for non-inlined assignments
            //
            var vdbgs = new List<VariableDebugInformation> ();
            var locals = new SymbolTable<VariableDefinition> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (a.Result != LocalSymbol.None) {
                        var symbol = a.Result;
                        if (!ShouldInline (symbol)) {
                            var irtype = a.Instruction.ResultType (function.IRModule);
                            var local = new VariableDefinition (GetClrType (irtype));
                            locals[a.Result] = local;
                            body.Variables.Add (local);
                            //var name = "local" + symbol.Text.Substring (1);
                            //var dbg = new VariableDebugInformation (local, name);
                            //vdbgs.Add (dbg);
                        }
                    }
                }
            }

            //
            // Create phi locals
            //
            var phiLocals = new SymbolTable<VariableDefinition> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (a.HasResult && a.Instruction is IR.PhiInstruction phi) {
                        if (!locals.TryGetValue (a.Result, out var local)) {
                            local = new VariableDefinition (GetClrType (phi.Type));
                            body.Variables.Add (local);
                        }
                        var name = "phi" + a.Result.Text.Substring (1);
                        var dbg = new VariableDebugInformation (local, name);
                        vdbgs.Add (dbg);
                        phiLocals[a.Result] = local;
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
            var prev = default (CecilInstruction);
            for (var i = 0; i < f.Blocks.Length; i++) {
                var b = f.Blocks[i];
                var nextBlock = i + 1 < f.Blocks.Length ? f.Blocks[i + 1] : null;

                //
                // Emit the assignments
                //
                prev = blockFirstInstr[b.Symbol];
                foreach (var a in b.Assignments) {
                    if (!ShouldInline (a.Result) && !(a.Instruction is IR.PhiInstruction)) {

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
                                EmitValue (phiV.Value, phi.Type);
                                Emit (il.Create (OpCodes.Stloc, GetPhiLocal (oa.Result)));
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

            void Emit (CecilInstruction i)
            {
                il.InsertAfter (prev, i);
                prev = i;
            }

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
                            case IR.FcmpCondition.UnorderedNotEqual:
                                Emit (il.Create (OpCodes.Ceq));
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                Emit (il.Create (OpCodes.Ceq));
                                break;
                            case IR.FcmpCondition.UnorderedLessThan:
                                Emit (il.Create (OpCodes.Clt_Un));
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
                    case IR.LoadInstruction load: {

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
                            else {
                                Emit (il.Create (OpCodes.Ldobj, et));
                            }
                        }
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
                    case IR.RetInstruction ret:
                        EmitTypedValue (ret.Value);
                        Emit (il.Create (OpCodes.Ret));
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
                            Emit (il.Create (OpCodes.Br, end));

                            Emit (end);
                        }
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
                    case IR.SubInstruction sub:
                        EmitValue (sub.Op1, sub.Type);
                        EmitValue (sub.Op2, sub.Type);
                        Emit (il.Create (OpCodes.Sub));
                        break;
                    case IR.SwitchInstruction sw: {
                            EmitTypedValue (sw.Value);
                            foreach (var c in sw.Cases) {
                                Emit (il.Create (OpCodes.Dup));
                                EmitValue (c.Value.Constant, c.Value.Type);
                                Emit (il.Create (OpCodes.Beq, GetLabel (c.Label)));
                            }
                            Emit (il.Create (OpCodes.Br, GetLabel (sw.DefaultLabel)));
                        }
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
                    case IR.UitofpInstruction zext:
                        EmitTypedValue (zext.Value);
                        switch (zext.Type) {
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
                                throw new NotSupportedException ($"Cannot uitofp {zext.Type}");
                        }
                        break;
                    case IR.UnconditionalBrInstruction br:
                        Emit (il.Create (OpCodes.Br, GetLabel (br.Destination)));
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
                        if (methodDefs.TryGetValue (g.Symbol, out var ff)) {
                            Emit (il.Create (OpCodes.Ldftn, ff.ILDefinition));
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
                        switch (type) {
                            case Types.IntegerType intt:
                                Emit (il.Create (OpCodes.Ldc_I4_0));
                                switch (intt.Bits) {
                                    case 1:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 8:
                                        Emit (il.Create (OpCodes.Conv_I1));
                                        break;
                                    case 16:
                                        Emit (il.Create (OpCodes.Conv_I2));
                                        break;
                                    case 32:
                                        break;
                                    case 64:
                                        Emit (il.Create (OpCodes.Conv_I8));
                                        break;
                                    default:
                                        throw new NotSupportedException ("Undefined " + type);
                                }
                                break;
                            default:
                                throw new NotSupportedException ("Undefined " + type);
                        }
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
                        break;
                    case IR.ZeroConstant zero:
                        EmitZeroValue (type);
                        break;
                    default:
                        throw new NotImplementedException ("Cannot emit value " + value);
                }
            }

            void EmitValuePointer (IR.Value value, LType type)
            {
                if (value is IR.GlobalValue g) {
                    Emit (il.Create (OpCodes.Ldsflda, globals[g.Symbol]));
                    Emit (il.Create (OpCodes.Conv_U));
                }
                else {
                    EmitValue (value, type);
                }
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
                        case "@llvm.dbg.value":
                            return;
                        case "@llvm.fabs.f64":
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            Emit (il.Create (OpCodes.Call, sysMathAbsD));
                            return;
                        case "@llvm.sqrt.f64":
                            EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                            Emit (il.Create (OpCodes.Call, sysMathSqrtD));
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
                        default:
                            if (methodDefs.TryGetValue (gv.Symbol, out var m)) {
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
                    var lva = f.GetAssignment (lv);
                    var ltype = lva.Instruction.ResultType (function.IRModule);
                    foreach (var a in call.Arguments) {
                        EmitValue (a.Value, a.Type);
                    }
                    EmitValue (lv, ltype);
                    Emit (il.Create (OpCodes.Calli, GetCallSite (ltype)));
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
                        if (Resolve (pt.ElementType) is Types.LiteralStructureType st && index.Value is IR.IntegerConstant iconst) {
                            if (i > 0) {
                                var cst = GetClrType (pt).GetElementType ().Resolve ();
                                var field = cst.Fields[(int)iconst.Value];
                                Emit (il.Create (OpCodes.Ldflda, field));
                                Emit (il.Create (OpCodes.Conv_U));
                                t = st.Elements[i];
                            }
                            else {
                                // Skip the first index for structures
                            }
                        }
                        else {
                            var esize = pt.ElementType.GetByteSize (function.IRModule);
                            EmitValue (index.Value, index.Type);
                            EmitValue (new IR.IntegerConstant (esize), index.Type);
                            Emit (il.Create (OpCodes.Mul));
                            Emit (il.Create (OpCodes.Conv_U));
                            Emit (il.Create (OpCodes.Add));
                            t = pt.ElementType;
                        }
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
                        throw new NotSupportedException ("Element pointer for " + t);
                    }
                }
            }

            CecilInstruction GetLabel (IR.LabelValue label)
            {
                return blockFirstInstr[label.Symbol];
            }
        }

        CallSite GetCallSite (LType ltype)
        {
            var ft = (FunctionType)((Types.PointerType)ltype).ElementType;
            var c = new CallSite (GetClrType (ft.ReturnType));
            foreach (var p in ft.ParameterTypes) {
                var pd = new ParameterDefinition (GetClrType (p));
                c.Parameters.Add (pd);
            }
            return c;
        }

        LType Resolve (LType elementType)
        {
            if (elementType is NamedType nt) {
                return structs[nt.Symbol].Item1;
            }
            return elementType;
        }

        void AddDebugInfoToStruct (Symbol symbol, SymbolTable<object> debugInfo, Module module)
        {
            var td = structs[symbol].Item2;
            if (debugInfo.TryGetValue (Symbol.BaseType, out var o) && o is MetaSymbol) {
                if (module.Metadata.TryGetValue ((Symbol)o, out o) && o is SymbolTable<object>) {
                    var typedefDbg = (SymbolTable<object>)o;
                    if (typedefDbg.TryGetValue (Symbol.Name, out o) && o is string) {
                        td.Name = o.ToString ();
                    }
                    if (typedefDbg.TryGetValue (Symbol.BaseType, out o) && o is MetaSymbol) {
                        if (module.Metadata.TryGetValue ((Symbol)o, out o) && o is SymbolTable<object>) {
                            var structDbg = (SymbolTable<object>)o;
                            if (structDbg.TryGetValue (Symbol.Elements, out o) && o is MetaSymbol) {
                                if (module.Metadata.TryGetValue ((Symbol)o, out o) && o is IEnumerable<object>) {
                                    var elementDbgs = ((IEnumerable<object>)o).Cast<MetaSymbol> ().Select (x => {
                                        module.Metadata.TryGetValue (x, out var oo);
                                        return (oo as SymbolTable<object>) ?? new SymbolTable<object> ();
                                    }).ToArray ();
                                    if (elementDbgs.Length == td.Fields.Count) {
                                        for (int i = 0; i < td.Fields.Count; i++) {
                                            var f = td.Fields[i];
                                            var d = elementDbgs[i];
                                            if (d.TryGetValue (Symbol.Name, out var ooo) && ooo is string) {
                                                f.Name = ooo.ToString ();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        TypeReference GetParameterType (LType irType, Module module, SymbolTable<object> debugInfo)
        {
            bool? unsigned = null;

            if (debugInfo != null) {
                if (irType is Types.PointerType pt && pt.ElementType is NamedType nt && nt.Resolve (module) is LiteralStructureType st) {
                    AddDebugInfoToStruct (nt.Symbol, debugInfo, module);
                }
            }

            return GetClrType (irType, unsigned: unsigned);
        }

        TypeReference GetClrType (LType irType, bool? unsigned = false)
        {
            switch (irType) {
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
                        case 1:
                            return sysBoolean;
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
                case Types.ArrayType art:
                    return GetClrType (art.ElementType).MakePointerType ();
                case Types.PointerType pt when pt.ElementType is FunctionType ft:
                    return sysVoidPtr;
                case Types.PointerType pt:
                    return GetClrType (pt.ElementType).MakePointerType ();
                case NamedType nt:
                    return structs[nt.Symbol].Item2;
                case VectorType vt: {
                        return GetVectorType (vt).ClrType;
                    }
                case VoidType vdt:
                    return sysVoid;
                case VarArgsType vat:
                    return sysIntPtr;
                default:
                    throw new NotSupportedException ($"{irType} not supported");
            }
        }

        private SimdVector GetVectorType (VectorType vt)
        {
            var et = GetClrType (vt.ElementType);
            var key = (vt.Length, et.FullName);
            if (vectorTypes.TryGetValue (key, out var vct)) {
                return vct;
            }
            return AddVectorType (key, et);
        }

        SimdVector AddVectorType ((int Length, string TypeFullName) key, TypeReference elementType)
        {
            var tname = $"Vector{key.Length}{elementType.Name}";

            var td = new TypeDefinition (namespac, tname, TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public, sysVal);

            for (var i = 0; i < key.Length; i++) {
                var f = new FieldDefinition ("E" + i, FieldAttributes.Public, elementType);
                td.Fields.Add (f);
            }

            var ctor = new MethodDefinition (".ctor", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, sysVoid);
            {
                for (var i = 0; i < key.Length; i++) {
                    var p = new ParameterDefinition ("e" + i, ParameterAttributes.None, elementType);
                    ctor.Parameters.Add (p);
                }
                var body = new MethodBody (ctor);
                var il = body.GetILProcessor ();
                for (var i = 0; i < key.Length; i++) {
                    il.Append (il.Create (OpCodes.Ldarg_0));
                    il.Append (il.Create (OpCodes.Ldarg, i + 1));
                    il.Append (il.Create (OpCodes.Stfld, td.Fields[i]));
                }
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                ctor.Body = body;
                td.Methods.Add (ctor);
            }
            var sub = new MethodDefinition ("Subtract", MethodAttributes.Public | MethodAttributes.Static, td);
            {
                sub.Parameters.Add (new ParameterDefinition (td));
                sub.Parameters.Add (new ParameterDefinition (td));

                var body = new MethodBody (ctor);
                var il = body.GetILProcessor ();
                for (var i = 0; i < key.Length; i++) {
                    il.Append (il.Create (OpCodes.Ldarg_0));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (OpCodes.Ldarg_1));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (OpCodes.Sub));
                }
                il.Append (il.Create (OpCodes.Newobj, ctor));
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                sub.Body = body;
                td.Methods.Add (sub);
            }

            mod.Types.Add (td);
            var r = new SimdVector {
                ElementClrType = elementType,
                ClrType = td,
                Ctor = ctor,
                Subtract = sub,
                ElementFields = td.Fields.Select (x => (FieldReference)x).ToArray (),
            };
            vectorTypes[key] = r;

            return r;
        }

        public void WriteAssembly (string path)
        {
            var ps = new WriterParameters {
                WriteSymbols = true,
                SymbolWriterProvider = new PortablePdbWriterProvider (),
            };
            asm.Write (path, ps);
        }

        class SimdVector
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
    }
}
