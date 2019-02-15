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
        public MethodReference sysMathAbsD;
        public MethodReference sysMathSqrtD;
        TypeReference sysEventArgs;
        TypeReference sysIAsyncResult;
        TypeReference sysAsyncCallback;

        readonly Dictionary<(int, string), SimdVector> vectorTypes =
            new Dictionary<(int, string), SimdVector> ();

        readonly SymbolTable<DefinedFunction> methodDefs =
            new SymbolTable<DefinedFunction> ();

        public bool TryGetFunction (Symbol symbol, out DefinedFunction function) =>
            methodDefs.TryGetValue (symbol, out function);

        readonly SymbolTable<FieldDefinition> globals =
            new SymbolTable<FieldDefinition> ();

        public bool TryGetGlobal (Symbol symbol, out FieldDefinition global) =>
            globals.TryGetValue (symbol, out global);

        public FieldDefinition GetGlobal (Symbol symbol) => globals[symbol];

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
                        var fc = new FunctionCompiler (this, m.Value);
                        fc.CompileFunction ();
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

        public TypeReference GetClrType (LType irType, bool? unsigned = false)
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
                case VectorType vt:
                    return GetVectorType (vt).ClrType;
                case VoidType vdt:
                    return sysVoid;
                case VarArgsType vat:
                    return sysIntPtr;
                default:
                    throw new NotSupportedException ($"{irType} not supported");
            }
        }

        public SimdVector GetVectorType (VectorType vt)
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
                sub.Parameters.Add (new ParameterDefinition ("a", ParameterAttributes.None, td));
                sub.Parameters.Add (new ParameterDefinition ("b", ParameterAttributes.None, td));

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
    }
}
