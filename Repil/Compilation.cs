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
        TypeReference sysVoidPtrPtr;
        TypeReference sysObj;
        TypeReference sysVal;
        TypeReference sysBoolean;
        TypeReference sysByte;
        TypeReference sysInt16;
        TypeReference sysInt32;
        public TypeReference sysUInt32;
        TypeReference sysInt64;
        public TypeReference sysIntPtr;
        TypeReference sysSingle;
        TypeReference sysDouble;
        TypeReference sysString;
        TypeReference sysNotImpl;
        MethodReference sysNotImplCtor;
        TypeReference sysNotSupp;
        MethodReference sysNotSuppCtor;
        TypeReference sysMath;
        public MethodReference sysMathAbsD;
        public MethodReference sysMathCeilD;
        public MethodReference sysMathFloorD;
        public MethodReference sysMathSqrtD;
        public MethodReference sysSingleIsNaN;
        public MethodReference sysDoubleIsNaN;
        TypeReference sysEventArgs;
        TypeReference sysIAsyncResult;
        TypeReference sysAsyncCallback;
        public MethodReference sysPointerFromIntPtr;
        public MethodReference sysIntPtrFromInt64;
        public MethodReference sysIntPtrFromPointer;
        TypeReference sysMarshal;
        public MethodReference sysAllocHGlobal;
        public MethodReference sysReAllocHGlobal;
        public MethodReference sysFreeHGlobal;

        readonly Dictionary<(int, string), SimdVector> vectorTypes =
            new Dictionary<(int, string), SimdVector> ();

        readonly SymbolTable<DefinedFunction> methodDefs =
            new SymbolTable<DefinedFunction> ();

        public bool TryGetFunction (Symbol symbol, out DefinedFunction function) =>
            methodDefs.TryGetValue (symbol, out function);

        readonly SymbolTable<SymbolTable<FieldDefinition>> globals =
            new SymbolTable<SymbolTable<FieldDefinition>> ();

        int compiledFunctionCount;

        public int CompiledFunctionCount => compiledFunctionCount;

        public bool TryGetGlobal (Symbol module, Symbol symbol, out FieldDefinition global)
        {
            if (globals.TryGetValue (module, out var mglobals))
                return mglobals.TryGetValue (symbol, out global);
            global = null;
            return false;
        }

        public FieldDefinition GetGlobal (Symbol module, Symbol symbol) => globals[module][symbol];

        Syscalls syscalls;

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
            EmitSyscalls ();
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
            MethodReference ImportMethod (TypeReference declType, TypeReference returnType, string name, params TypeReference[] argTypes)
            {
                var td = declType.Resolve ();
                var ms = td.Methods.Where (x => x.Name == name);
                foreach (var m in ms) {
                    if (m.Parameters.Count != argTypes.Length)
                        continue;
                    var match = m.ReturnType.FullName == returnType.FullName;
                    for (var i = 0; match && i < m.Parameters.Count; i++) {
                        var p = m.Parameters[i];
                        if (p.ParameterType.FullName != argTypes[i].FullName)
                            match = false;
                    }
                    if (match) {
                        var mr = new MethodReference (name, returnType, declType);
                        mr.ExplicitThis = m.ExplicitThis;
                        mr.CallingConvention = m.CallingConvention;
                        mr.HasThis = m.HasThis;
                        foreach (var p in argTypes) {
                            mr.Parameters.Add (new ParameterDefinition (p));
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
            sysUInt32 = Import ("System.UInt32");
            sysInt64 = Import ("System.Int64");
            sysIntPtr = Import ("System.IntPtr");
            sysSingle = Import ("System.Single");
            sysDouble = Import ("System.Double");
            sysSingleIsNaN = ImportMethod (sysSingle, sysBoolean, "IsNaN", sysSingle);
            sysDoubleIsNaN = ImportMethod (sysDouble, sysBoolean, "IsNaN", sysDouble);
            sysVoid = Import ("System.Void");
            sysVoidPtr = sysVoid.MakePointerType ();
            sysVoidPtrPtr = sysVoidPtr.MakePointerType ();
            sysString = Import ("System.String");
            sysNotImpl = Import ("System.NotImplementedException");
            sysNotImplCtor = ImportMethod (sysNotImpl, sysVoid, ".ctor");
            sysNotSupp = Import ("System.NotSupportedException");
            sysNotSuppCtor = ImportMethod (sysNotSupp, sysVoid, ".ctor", sysString);
            sysMath = Import ("System.Math");
            sysMathAbsD = ImportMethod (sysMath, sysDouble, "Abs", sysDouble);
            sysMathCeilD = ImportMethod (sysMath, sysDouble, "Ceiling", sysDouble);
            sysMathFloorD = ImportMethod (sysMath, sysDouble, "Floor", sysDouble);
            sysMathSqrtD = ImportMethod (sysMath, sysDouble, "Sqrt", sysDouble);
            sysEventArgs = Import ("System.EventArgs");
            sysIAsyncResult = Import ("System.IAsyncResult");
            sysAsyncCallback = Import ("System.AsyncCallback");
            sysIntPtrFromInt64 = ImportMethod (sysIntPtr, sysIntPtr, "op_Explicit", sysInt64);
            sysIntPtrFromPointer = ImportMethod (sysIntPtr, sysIntPtr, "op_Explicit", sysVoidPtr);
            sysPointerFromIntPtr = ImportMethod (sysIntPtr, sysVoidPtr, "op_Explicit", sysIntPtr);
            sysMarshal = Import ("System.Runtime.InteropServices.Marshal");
            sysAllocHGlobal = ImportMethod (sysMarshal, sysIntPtr, "AllocHGlobal", sysIntPtr);
            sysReAllocHGlobal = ImportMethod (sysMarshal, sysIntPtr, "ReAllocHGlobal", sysIntPtr, sysIntPtr);
            sysFreeHGlobal = ImportMethod (sysMarshal, sysVoid, "FreeHGlobal", sysIntPtr);
        }

        void CompileStructures ()
        {
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public;

            var todo = new List<(Module, LiteralStructureType, TypeDefinition)> ();

            foreach (var m in Modules) {
                foreach (var iskv in m.IdentifiedStructures) {
                    if (structs.ContainsKey (iskv.Key))
                        continue;
                    var tname = GetIdentifier (iskv.Key);
                    structNames.Add (tname);
                    if (iskv.Value is LiteralStructureType l) {
                        var td = new TypeDefinition (namespac, tname, tattrs, sysVal);
                        mod.Types.Add (td);
                        structs[iskv.Key] = (l, td);
                        todo.Add ((m, l, td));
                    }
                    else if (iskv.Value is OpaqueStructureType) {
                        var td = new TypeDefinition (namespac, tname, tattrs, sysVal);
                        mod.Types.Add (td);
                        structs[iskv.Key] = (null, td);
                    }
                    else {
                        throw new NotSupportedException ($"Cannot compile {iskv.Value}");
                    }
                }
            }

            foreach (var (m, l, td) in todo) {
                var fields =
                    from e in l.Elements.Zip(Enumerable.Range(0, l.Elements.Length), (e, i) => (e, i))
                    let fn = "F" + e.i
                    select new FieldDefinition (fn, FieldAttributes.Public, GetClrType (e.e));

                foreach (var f in fields) {
                    td.Fields.Add (f);
                }
            }
        }

        void EmitGlobalVariables ()
        {
            //var publicGlobalsType = new TypeDefinition (namespac, "Globals", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            //mod.Types.Add (publicGlobalsType);

            //
            // Define globals
            //
            foreach (var m in Modules) {

                if (m.GlobalVariables.All (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new SymbolTable<FieldDefinition> ();
                    globals.Add (m.Symbol, mglobals);
                }

                var isPrivate = false;//m.GlobalVariables.All (x => x.Value.IsExternal || x.Value.IsPrivate);

                var moduleGlobalsType = new TypeDefinition (
                    namespac, m.Symbol.ToString (),
                    TypeAttributes.BeforeFieldInit | TypeAttributes.Abstract | TypeAttributes.Sealed
                    | (isPrivate ? 0 : TypeAttributes.Public),
                    sysObj);
                mod.Types.Add (moduleGlobalsType);

                foreach (var kv in m.GlobalVariables) {

                    var symbol = kv.Key;
                    var g = kv.Value;

                    if (g.IsExternal)
                        continue;
                    if (globals.ContainsKey (symbol))
                        continue;

                    var gname = GetIdentifier (symbol);

                    var gtype = GetClrType (g.Type);
                    var field = new FieldDefinition (
                        gname,
                        FieldAttributes.Static | (FieldAttributes.Public), gtype);

                    moduleGlobalsType.Fields.Add (field);
                    mglobals.Add (symbol, field);
                }
            }

            //
            // Link module variables
            //
            foreach (var m in Modules) {

                if (!m.GlobalVariables.Any (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new SymbolTable<FieldDefinition> ();
                    globals.Add (m.Symbol, mglobals);
                }

                foreach (var kv in m.GlobalVariables) {

                    var symbol = kv.Key;
                    var ident = GetIdentifier (symbol);
                    var g = kv.Value;

                    if (!g.IsExternal)
                        continue;

                    var field = from ms in globals.Values
                                from mkv in ms
                                let mg = mkv.Value
                                where mg.IsPublic && mg.Name == ident
                                select mg;
                    var f = field.FirstOrDefault ();
                    if (f != null) {
                        mglobals.Add (symbol, f);
                    }
                    else {
                        ErrorMessage ($"Global variable `{symbol}` is undefined");
                    }
                }
            }
        }

        void EmitSyscalls ()
        {
            var syscallstd = new TypeDefinition (namespac, "Syscalls", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            mod.Types.Add (syscallstd);

            syscalls = new Syscalls (this, syscallstd);
            syscalls.Emit ();

            foreach (var iskv in syscalls.Calls) {
                methodDefs[iskv.Key] = new DefinedFunction {
                    Symbol = iskv.Key,
                    IRModule = null,
                    IRDefinition = null,
                    ILDefinition = iskv.Value,
                    ParamSyms = new SymbolTable<ParameterDefinition> (),
                };
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
                    var tname = GetIdentifier (iskv.Key);
                    if (dbgMeth.TryGetValue (Symbol.Name, out var o)) {
                        tname = o.ToString ();
                    }
                    var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static;
                    var md = new MethodDefinition (tname, mattrs, GetClrType (f.ReturnType));

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
                    var tname = GetIdentifier (iskv.Key);

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

                if (m.Value.ILDefinition.HasBody && m.Value.ILDefinition.Body.Instructions.Count > 0)
                    continue;

                if (m.Value.IRDefinition != null) {
                    try {
                        var fc = new FunctionCompiler (this, m.Value);
                        fc.CompileFunction ();
                        compiledFunctionCount++;
                    }
                    catch (Exception ex) {
                        ErrorMessage ($"Failed to compile {m.Key}: {ex.Message}", ex);
                        CompileFailedFunction (m.Value, ex);
                    }
                }
                else {
                    CompileMissingFunction (m.Value);
                }
            }
        }

        public readonly List<Message> Messages = new List<Message> ();

        public bool HasErrors => Messages.Exists (m => m.Type == MessageType.Error);
        public int ErrorCount => Messages.Count (m => m.Type == MessageType.Error);

        void ErrorMessage (string message, Exception exception = null)
        {
            Messages.Add (new Message (message, exception));
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

        public string GetIdentifier (Symbol symbol)
        {
            var b = new System.Text.StringBuilder ();
            foreach (var c in symbol.Text.Skip (1)) {
                if (char.IsDigit (c)) {
                    if (b.Length == 0)
                        b.Append ('_');
                    b.Append (c);
                }
                else if (char.IsLetter (c)) {
                    b.Append (c);
                }
                else {
                    b.Append ('_');
                }
            }
            return b.ToString ();
        }

        public TypeReference GetClrType (LType irType, bool? unsigned = false)
        {
            switch (irType) {
                case FloatType floatt:
                    switch (floatt.Bits) {
                        case 32:
                            return sysSingle;
                        default:
                            return sysDouble;
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
                case Types.PointerType pt when pt.ElementType is LiteralStructureType ls && ls.Elements.Length == 0:
                    return sysVoidPtr;
                case Types.PointerType pt:
                    return GetClrType (pt.ElementType).MakePointerType ();
                case FunctionType ft:
                    return sysVoidPtr;
                case NamedType nt:
                    if (structs.TryGetValue (nt.Symbol, out var ntSym))
                        return ntSym.Item2;
                    else
                        throw new Exception ($"Cannot find {nt.Symbol}");
                case VectorType vt:
                    return GetVectorType (vt).ClrType;
                case VoidType vdt:
                    return sysVoid;
                case VarArgsType vat:
                    return sysIntPtr;
                default:
                    throw new NotSupportedException ($"{irType} ({irType?.GetType().Name}) not supported");
            }
        }

        public SimdVector GetVectorType (VectorType vt)
        {
            var et = GetClrType (vt.ElementType);
            var key = (vt.Length, et.FullName);
            if (vectorTypes.TryGetValue (key, out var vct)) {
                return vct;
            }
            return AddVectorType (key, vt, et);
        }

        SimdVector AddVectorType ((int Length, string TypeFullName) key, VectorType irType, TypeReference elementType)
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

            var r = new SimdVector {
                ElementClrType = elementType,
                ClrType = td,
                Ctor = ctor,
                ElementFields = td.Fields.Select (x => (FieldReference)x).ToArray (),
            };

            mod.Types.Add (td);
            vectorTypes[key] = r;

            //
            // Generate operations
            //
            var unopMethods = new[] {
                ("ToInt8", OpCodes.Conv_I1, new VectorType (irType.Length, IntegerType.I1)),
                ("ToInt16", OpCodes.Conv_I2, new VectorType (irType.Length, IntegerType.I16)),
                ("ToInt32", OpCodes.Conv_I4, new VectorType (irType.Length, IntegerType.I32)),
                ("ToInt64", OpCodes.Conv_I8, new VectorType (irType.Length, IntegerType.I64)),
            };
            foreach (var (name, opcode, vt) in unopMethods) {
                var cvt = GetClrType (vt);
                var cvtCtor = cvt.Resolve ().Methods.First (x => x.Name == ".ctor" && x.Parameters.Count > 0);
                var mop = new MethodDefinition (name, MethodAttributes.Public | MethodAttributes.Static, cvt);
                mop.Parameters.Add (new ParameterDefinition ("a", ParameterAttributes.None, td));

                var body = new MethodBody (ctor);
                var il = body.GetILProcessor ();
                for (var i = 0; i < key.Length; i++) {
                    il.Append (il.Create (OpCodes.Ldarg_0));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (opcode));
                }
                il.Append (il.Create (OpCodes.Newobj, cvtCtor));
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                mop.Body = body;
                td.Methods.Add (mop);
                typeof (SimdVector).GetField (name).SetValue (r, mop);
            }
            var opMethods = new[] {
                ("Add", OpCodes.Add),
                ("Subtract", OpCodes.Sub),
                ("Multiply", OpCodes.Mul),
                ("Divide", OpCodes.Div),
            };
            foreach (var (name, opcode) in opMethods)
            {
                var mop = new MethodDefinition (name, MethodAttributes.Public | MethodAttributes.Static, td);
                mop.Parameters.Add (new ParameterDefinition ("a", ParameterAttributes.None, td));
                mop.Parameters.Add (new ParameterDefinition ("b", ParameterAttributes.None, td));

                var body = new MethodBody (ctor);
                var il = body.GetILProcessor ();
                for (var i = 0; i < key.Length; i++) {
                    il.Append (il.Create (OpCodes.Ldarg_0));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (OpCodes.Ldarg_1));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (opcode));
                }
                il.Append (il.Create (OpCodes.Newobj, ctor));
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                mop.Body = body;
                td.Methods.Add (mop);
                typeof (SimdVector).GetField (name).SetValue (r, mop);
            }


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
