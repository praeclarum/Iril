using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Iril.Types;
using Mono.Cecil.Rocks;
using Mono.Cecil.Cil;
using System.Text;
using Mono.Cecil.Mdb;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Iril
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

        bool hasEntryPoint = false;

        const string pidTypeName = "<PrivateImplementationDetails>";

        readonly SymbolTable<(LiteralStructureType, TypeDefinition)> globalStructs =
            new SymbolTable<(LiteralStructureType, TypeDefinition)> ();
        readonly SymbolTable<SymbolTable<(LiteralStructureType, TypeDefinition)>> moduleStructs =
            new SymbolTable<SymbolTable<(LiteralStructureType, TypeDefinition)>> ();

        AssemblyDefinition sysAsm;
        public TypeReference sysVoid;
        TypeReference sysArray;
        TypeReference sysRuntimeFieldHandle;
        TypeReference sysRuntimeHelpers;
        MethodReference sysRuntimeHelpersInitArray;
        TypeReference sysVoidPtr;
        TypeReference sysVoidPtrPtr;
        public TypeReference sysObj;
        public MethodReference sysObjToString;
        public TypeReference sysObjArray;
        TypeReference sysVal;
        TypeReference sysBoolean;
        public TypeReference sysByte;
        public TypeReference sysBytePtr;
        public TypeReference sysByteArray;
        TypeReference sysInt16;
        public TypeReference sysInt32;
        public TypeReference sysUInt32;
        TypeReference sysInt64;
        public TypeReference sysIntPtr;
        TypeReference sysSingle;
        TypeReference sysDouble;
        TypeReference sysString;
        TypeReference sysChar;
        TypeReference sysCharArray;
        MethodReference sysStringToCharArray;
        TypeReference sysCompGen;
        MethodReference sysCompGenCtor;
        TypeReference sysGCHandle;
        TypeReference sysGCHandleType;
        MethodReference sysGCHandleAlloc;
        MethodReference sysGCHandleAddrOfPinnedObject;
        TypeReference sysNotImpl;
        MethodReference sysNotImplCtor;
        TypeReference sysNotSupp;
        MethodReference sysNotSuppCtor;
        TypeReference sysMath;
        public TypeReference sysException;
        TypeReference sysParamsAttr;
        public MethodReference sysParamsAttrCtor;
        public MethodReference sysExceptionCtor;
        public MethodReference sysExceptionCtorVoid;
        public MethodReference sysMathAbsD;
        public MethodReference sysMathCeilD;
        public MethodReference sysMathFloorD;
        public MethodReference sysMathSqrtD;
        public MethodReference sysMathPowD;
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
        public MethodReference sysAllocHGlobalInt;
        public MethodReference sysReAllocHGlobal;
        public MethodReference sysFreeHGlobal;
        public MethodReference sysPtrToStringAuto;
        public MethodReference sysMarshalCopyIntToArray;
        public MethodReference sysMarshalCopyArrayToInt;
        TypeReference sysDebugger;
        public MethodReference sysDebuggerBreak;
        public TypeReference sysStream;
        public MethodReference sysStreamWrite;
        public MethodReference sysStreamRead;
        TypeReference sysTextWriter;
        public MethodReference sysTextWriterFlush;
        TypeReference sysConsole;
        public MethodReference sysConsoleWrite;
        public MethodReference sysConsoleWriteChar;
        public MethodReference sysConsoleWriteObj;
        public MethodReference sysConsoleWriteLine;
        public MethodReference sysConsoleGetOut;
        public MethodReference sysConsoleOpenStandardInput;
        public MethodReference sysConsoleOpenStandardOutput;
        TypeReference sysEncoding;
        public MethodReference sysAscii;
        public MethodReference sysAsciiGetBytes;
        TypeReference sysStackTrace;
        public MethodReference sysStackTraceCtor;
        public MethodReference sysStackTraceGetFrameCount;
        public MethodReference sysStringCharCountCtor;
        public MethodReference sysStringConcat;

        public Lazy<TypeDefinition> nativeException;
        public MethodDefinition nativeExceptionCtor => nativeException.Value.GetConstructors ().First ();
        public FieldDefinition nativeExceptionData => nativeException.Value.Fields.First ();

        readonly Dictionary<(int, string), SimdVector> vectorTypes =
            new Dictionary<(int, string), SimdVector> ();

        readonly Dictionary<LType[], AnonymousStruct> astructTypes =
            new Dictionary<LType[], AnonymousStruct> (AnonymousStruct.LTypesEquality);

        readonly SymbolTable<DefinedFunction> externalMethodDefs =
            new SymbolTable<DefinedFunction> ();
        readonly SymbolTable<SymbolTable<DefinedFunction>> moduleMethodDefs =
            new SymbolTable<SymbolTable<DefinedFunction>> ();

        public bool TryGetFunction (Module module, Symbol symbol, out DefinedFunction function)
        {
            var r = (moduleMethodDefs.TryGetValue (module.Symbol, out var mdefs) && mdefs.TryGetValue (symbol, out function))
                    || externalMethodDefs.TryGetValue (symbol, out function);
            if (r) {
                function.ReferenceCount++;

                // Syscall dependencies
                var deps = syscalls.GetDependencies (symbol);
                foreach (var d in deps) {
                    externalMethodDefs[d].ReferenceCount++;
                }
            }
            return r;
        }

        readonly SymbolTable<SymbolTable<(IR.GlobalVariable Global, FieldDefinition Field)>> globals =
            new SymbolTable<SymbolTable<(IR.GlobalVariable, FieldDefinition)>> ();

        int compiledFunctionCount;

        public int CompiledFunctionCount => compiledFunctionCount;

        public bool TryGetGlobal (Symbol module, Symbol symbol, out (IR.GlobalVariable Global, FieldDefinition Field) global)
        {
            if (globals.TryGetValue (module, out var mglobals))
                return mglobals.TryGetValue (symbol, out global);
            global = (null, null);
            return false;
        }

        public FieldDefinition GetGlobal (Symbol module, Symbol symbol) => globals[module][symbol].Field;

        Syscalls syscalls;

        readonly Lazy<TypeDefinition> dataType;
        readonly Dictionary<int, TypeDefinition> dataFieldTypes = new Dictionary<int, TypeDefinition> ();

        readonly SymbolTable<Library> libraries;

        public int MaxFunctions { get; set; } = int.MaxValue;

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
            dataType = new Lazy<TypeDefinition> (CreateDataType);

            globalName = new NameNode {
                Name = "global"
            };

            libraries = Library.StandardLibraries;
        }

        public void Compile ()
        {
            FindSystemTypes ();
            FindStructures ();
            FindFunctions ();
            //PrintNameTree ();
            CompileStructures ();
            EmitSyscalls ();
            EmitNativeExceptions ();
            EmitGlobalVariables ();
            CreateFunctionDefinitions ();
            EmitGlobalInitializers ();
            CompileFunctions ();
            EmitEntrypoint ();
            RemoveUnusedFunctions ();
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
                var rt = et.Resolve ();
                var t = new TypeReference (et.Namespace, et.Name, sysAsm.MainModule, scope);
                t.IsValueType = rt.IsValueType;
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
            sysVoid = Import ("System.Void");
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
            sysArray = Import ("System.Array");
            sysCompGen = Import ("System.Runtime.CompilerServices.CompilerGeneratedAttribute");
            sysCompGenCtor = ImportMethod (sysCompGen, sysVoid, ".ctor");
            sysVoidPtr = sysVoid.MakePointerType ();
            sysVoidPtrPtr = sysVoidPtr.MakePointerType ();
            sysString = Import ("System.String");
            sysObjToString = ImportMethod (sysObj, sysString, "ToString");
            sysChar = Import ("System.Char");
            sysBytePtr = sysByte.MakePointerType ();
            sysByteArray = sysByte.MakeArrayType ();
            sysCharArray = sysChar.MakeArrayType ();
            sysObjArray = sysObj.MakeArrayType ();
            sysStringToCharArray = ImportMethod (sysString, sysCharArray, "ToCharArray");
            sysRuntimeFieldHandle = Import ("System.RuntimeFieldHandle");
            sysRuntimeHelpers = Import ("System.Runtime.CompilerServices.RuntimeHelpers");
            sysRuntimeHelpersInitArray = ImportMethod (sysRuntimeHelpers, sysVoid, "InitializeArray", sysArray, sysRuntimeFieldHandle);
            sysGCHandle = Import ("System.Runtime.InteropServices.GCHandle");
            sysGCHandleType = Import ("System.Runtime.InteropServices.GCHandleType");
            sysGCHandleAlloc = ImportMethod (sysGCHandle, sysGCHandle, "Alloc", sysObj, sysGCHandleType);
            sysGCHandleAddrOfPinnedObject = ImportMethod (sysGCHandle, sysIntPtr, "AddrOfPinnedObject");
            sysDebugger = Import ("System.Diagnostics.Debugger");
            sysDebuggerBreak = ImportMethod (sysDebugger, sysVoid, "Break");
            sysStream = Import ("System.IO.Stream");
            sysStreamRead = ImportMethod (sysStream, sysInt32, "Read", sysByteArray, sysInt32, sysInt32);
            sysStreamWrite = ImportMethod (sysStream, sysVoid, "Write", sysByteArray, sysInt32, sysInt32);
            sysTextWriter = Import ("System.IO.TextWriter");
            sysTextWriterFlush = ImportMethod (sysTextWriter, sysVoid, "Flush");
            sysConsole = Import ("System.Console");
            sysConsoleWrite = ImportMethod (sysConsole, sysVoid, "Write", sysString);
            sysConsoleWriteChar = ImportMethod (sysConsole, sysVoid, "Write", sysChar);
            sysConsoleWriteObj = ImportMethod (sysConsole, sysVoid, "Write", sysObj);
            sysConsoleWriteLine = ImportMethod (sysConsole, sysVoid, "WriteLine", sysString);
            sysConsoleOpenStandardInput = ImportMethod (sysConsole, sysStream, "OpenStandardInput", sysInt32);
            sysConsoleOpenStandardOutput = ImportMethod (sysConsole, sysStream, "OpenStandardOutput", sysInt32);
            sysConsoleGetOut = ImportMethod (sysConsole, sysTextWriter, "get_Out");
            sysSingleIsNaN = ImportMethod (sysSingle, sysBoolean, "IsNaN", sysSingle);
            sysDoubleIsNaN = ImportMethod (sysDouble, sysBoolean, "IsNaN", sysDouble);
            sysException = Import ("System.Exception");
            sysExceptionCtor = ImportMethod (sysException, sysVoid, ".ctor", sysString);
            sysExceptionCtorVoid = ImportMethod (sysException, sysVoid, ".ctor");
            sysNotImpl = Import ("System.NotImplementedException");
            sysNotImplCtor = ImportMethod (sysNotImpl, sysVoid, ".ctor");
            sysNotSupp = Import ("System.NotSupportedException");
            sysNotSuppCtor = ImportMethod (sysNotSupp, sysVoid, ".ctor", sysString);
            sysMath = Import ("System.Math");
            sysMathAbsD = ImportMethod (sysMath, sysDouble, "Abs", sysDouble);
            sysMathCeilD = ImportMethod (sysMath, sysDouble, "Ceiling", sysDouble);
            sysMathFloorD = ImportMethod (sysMath, sysDouble, "Floor", sysDouble);
            sysMathSqrtD = ImportMethod (sysMath, sysDouble, "Sqrt", sysDouble);
            sysMathPowD = ImportMethod (sysMath, sysDouble, "Pow", sysDouble, sysDouble);
            sysEventArgs = Import ("System.EventArgs");
            sysIAsyncResult = Import ("System.IAsyncResult");
            sysAsyncCallback = Import ("System.AsyncCallback");
            sysIntPtrFromInt64 = ImportMethod (sysIntPtr, sysIntPtr, "op_Explicit", sysInt64);
            sysIntPtrFromPointer = ImportMethod (sysIntPtr, sysIntPtr, "op_Explicit", sysVoidPtr);
            sysPointerFromIntPtr = ImportMethod (sysIntPtr, sysVoidPtr, "op_Explicit", sysIntPtr);
            sysMarshal = Import ("System.Runtime.InteropServices.Marshal");
            sysAllocHGlobal = ImportMethod (sysMarshal, sysIntPtr, "AllocHGlobal", sysIntPtr);
            sysAllocHGlobalInt = ImportMethod (sysMarshal, sysIntPtr, "AllocHGlobal", sysInt32);
            sysReAllocHGlobal = ImportMethod (sysMarshal, sysIntPtr, "ReAllocHGlobal", sysIntPtr, sysIntPtr);
            sysFreeHGlobal = ImportMethod (sysMarshal, sysVoid, "FreeHGlobal", sysIntPtr);
            sysPtrToStringAuto = ImportMethod (sysMarshal, sysString, "PtrToStringAuto", sysIntPtr);
            sysMarshalCopyIntToArray = ImportMethod (sysMarshal, sysVoid, "Copy", sysIntPtr, sysByteArray, sysInt32, sysInt32);
            sysMarshalCopyArrayToInt = ImportMethod (sysMarshal, sysVoid, "Copy", sysByteArray, sysInt32, sysIntPtr, sysInt32);
            sysParamsAttr = Import (typeof(ParamArrayAttribute).FullName);
            sysParamsAttrCtor = ImportMethod (sysParamsAttr, sysVoid, ".ctor");
            sysEncoding = Import ("System.Text.Encoding");
            sysAscii = ImportMethod (sysEncoding, sysEncoding, "get_ASCII");
            sysAsciiGetBytes = ImportMethod (sysEncoding, sysByteArray, "GetBytes", sysString);
            sysStackTrace = Import ("System.Diagnostics.StackTrace");
            sysStackTraceCtor = ImportMethod (sysStackTrace, sysVoid, ".ctor");
            sysStackTraceGetFrameCount = ImportMethod (sysStackTrace, sysInt32, "get_FrameCount");
            sysStringCharCountCtor = ImportMethod (sysString, sysVoid, ".ctor", sysChar, sysInt32);
            sysStringConcat = ImportMethod (sysString, sysString, "Concat", sysString, sysString);
        }

        readonly SymbolTable<Mono.Cecil.Cil.Document> fileDocuments = new SymbolTable<Mono.Cecil.Cil.Document> ();

        public Mono.Cecil.Cil.Document GetScopeDocument (Module module, MetaSymbol scopeRef)
        {
            if (module.Metadata.TryGetValue (scopeRef, out var scopeO) && scopeO is SymbolTable<object> scope) {
                if (scope.TryGetValue (Symbol.File, out var fileO) && fileO is MetaSymbol fileRef) {
                    if (fileDocuments.TryGetValue (fileRef, out var doc))
                        return doc;

                    if (module.Metadata.TryGetValue (fileRef, out fileO)
                        && fileO is SymbolTable<object> file
                        && file.TryGetValue (Symbol.Filename, out var fno)
                        && file.TryGetValue (Symbol.Directory, out var diro)) {

                        var fullPath = System.IO.Path.Combine (diro.ToString (), fno.ToString ());
                        //var url = new Uri (fullPath).AbsoluteUri;
                        var url = fullPath;
                        doc = new Mono.Cecil.Cil.Document (url);
                        if (File.Exists (fullPath)) {
                            doc.HashAlgorithm = DocumentHashAlgorithm.MD5;
                            doc.Hash = CalculateMD5 (fullPath);
                        }
                        doc.Language = DocumentLanguage.C;

                        fileDocuments[fileRef] = doc;
                        return doc;
                    }
                }
            }
            return null;
        }

        static byte[] CalculateMD5 (string fileName)
        {
            byte[] checksum;
            try {
                using (StreamReader streamReader = new StreamReader (fileName)) {
                    using (HashAlgorithm hashAlgorithm = MD5.Create ()) {
                        checksum = hashAlgorithm.ComputeHash (streamReader.BaseStream);
                    }
                }
            }
            catch (IOException) {
                checksum = null;
            }
            catch (UnauthorizedAccessException) {
                checksum = null;
            }
            return checksum;
        }

        class NameNode
        {
            public NameNode Parent;
            public string Name;
            public Symbol Symbol;

            public Module Module;
            public IR.FunctionDefinition Function;
            public IR.FunctionDeclaration FunctionDecl;
            public Types.StructureType Structure;

            public List<NameNode> Children = new List<NameNode> ();

            public bool IsFunction => Function != null || FunctionDecl != null;
            public override string ToString () => Name;
        }

        SymbolTable<SymbolTable<object>> functionDebugs = new SymbolTable<SymbolTable<object>> ();
        SymbolTable<NameNode> functionNodes = new SymbolTable<NameNode> ();
        readonly NameNode globalName;

        void AddNameNode (NameNode nn, string[] ancestry)
        {
            var parent = globalName;
            foreach (var a in ancestry) {
                var newParent = parent.Children.FirstOrDefault (x => x.Name == a);
                if (newParent == null) {
                    newParent = new NameNode {
                        Parent = parent,
                        Name = a,
                    };
                    parent.Children.Add (newParent);
                }
                parent = newParent;
            }
            parent.Children.Add (nn);
        }

        void PrintNameTree ()
        {
            Print ("", globalName);

            void Print(string indent, NameNode node)
            {
                var k = (node.Structure != null ? "C" : (node.IsFunction ? "M" : "?"));
                Console.WriteLine ($"{indent}{k}: {node.Name}");
                var nindent = "    " + indent;
                foreach (var c in node.Children.OrderBy (x => x.Name)) {
                    Print (nindent, c);
                }
            }
        }

        void FindFunctions ()
        {
            //
            // Generate function name nodes
            //
            foreach (var m in Modules) {
                foreach (var iskv in m.FunctionDefinitions) {
                    var sym = iskv.Key;
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
                    functionDebugs[sym] = dbgMeth;

                    //
                    // Create the method node
                    //
                    var mname = new IR.MangledName (sym);
                    var nn = new NameNode {
                        Name = mname.Identifier,
                        Symbol = mname.Symbol,
                        Module = m,
                        Function = f,
                    };
                    functionNodes[sym] = nn;

                    AddFunctionNode (nn, f.IsExternal, mname.Ancestry);
                }
            }

            //
            // Generate method definitions for declations
            //
            foreach (var m in Modules) {
                foreach (var iskv in m.FunctionDeclarations) {
                    if (iskv.Key.Text.StartsWith ("@llvm.", StringComparison.Ordinal))
                        continue;
                    if (functionNodes.ContainsKey (iskv.Key))
                        continue;

                    var sym = iskv.Key;
                    var f = iskv.Value;
                    var mname = new IR.MangledName (sym);
                    var nn = new NameNode {
                        Name = mname.Identifier,
                        Symbol = mname.Symbol,
                        Module = m,
                        FunctionDecl = f,
                    };
                    functionNodes[sym] = nn;

                    AddFunctionNode (nn, true, mname.Ancestry);
                }
            }

            void AddFunctionNode (NameNode nn, bool isExternal, string[] ancestry)
            {
                var a = ancestry;
                if (a.Length == 0) {
                    if (isExternal) {
                        a = new[] { namespac, "Globals" };
                    }
                    else {
                        a = new[] { namespac, IR.MangledName.SanitizeIdentifier (nn.Module.Symbol.Text) };
                    }
                }
                AddNameNode (nn, a);
            }
        }

        bool IsAnonymousTypeName (Symbol symbol)
        {
            return symbol.Text.IndexOf (".anon", StringComparison.Ordinal) > 0;
        }

        void FindStructures ()
        {
            var externals = new SymbolTable<NameNode> ();

            foreach (var m in Modules) {
                foreach (var iskv in m.IdentifiedStructures) {
                    var sym = iskv.Key;

                    if (!IsAnonymousTypeName (sym)) {
                        var s = iskv.Value;

                        if (externals.ContainsKey (sym)) {
                            var onn = externals[sym];
                            if (onn.Structure is OpaqueStructureType && !(s is OpaqueStructureType)) {
                                onn.Structure = s;
                                onn.Module = m;
                            }
                            continue;
                        }

                        var tname = new IR.MangledName (sym, prefixWithTypeKind: false);
                        var nn = new NameNode {
                            Name = tname.Identifier,
                            Symbol = tname.Symbol,
                            Module = m,
                            Structure = s
                        };

                        var a = tname.Ancestry;
                        if (a.Length == 0) {
                            a = new[] { namespac };
                        }
                        AddNameNode (nn, a);
                        externals.Add (sym, nn);
                    }
                }
            }
        }

        void CompileStructures ()
        {
            var todo = new List<(Module, LiteralStructureType, TypeDefinition)> ();

            foreach (var c in globalName.Children) {
                CompileStructures ("", null, c, todo);
            }

            foreach (var (m, l, td) in todo) {
                var n = l.Elements.Length;
                var offset = 0;
                td.IsExplicitLayout = true;
                for (var i = 0; i < n; i++) {
                    var e = l.Elements[i];
                    var fn = GetFieldPrefix (e) + i;
                    long byteSize;
                    TypeReference fieldType;
                    if (e is Types.ArrayType arrt) {
                        var ee = arrt.ElementType;
                        var len = arrt.Length;
                        byteSize = ee.GetByteSize (m) * len;
                        fieldType = GetClrType (ee, module: m);
                    }
                    else {
                        byteSize = e.GetByteSize (m);
                        fieldType = GetClrType (e, module: m);
                    }
                    var field = new FieldDefinition (fn, FieldAttributes.Public, fieldType);
                    offset = Align (offset, e, (int)byteSize, m);
                    field.Offset = offset;
                    td.Fields.Add (field);
                    offset += (int)byteSize;
                }
                td.ClassSize = offset;
                td.PackingSize = (short)m.PointerByteSize;
            }
        }

        void CompileStructures (string namesp, TypeDefinition parentType, NameNode node, List<(Module, LiteralStructureType, TypeDefinition)> todo)
        {
            if (node.IsFunction) {
            }
            else {
                var isNamespace =
                    node.Structure == null
                    && parentType == null
                    && node.Children.All (x => !x.IsFunction);

                if (isNamespace) {
                    var newNamespace = namesp.Length > 0 ? namesp + "." + node.Name : node.Name;
                    foreach (var c in node.Children) {
                        CompileStructures (newNamespace, null, c, todo);
                    }
                }
                else {
                    if (node.Structure != null || node.Children.Count > 0) {
                        TypeDefinition td;
                        var ns = parentType == null ? namesp : null;
                        var vis = parentType == null ? TypeAttributes.Public : TypeAttributes.NestedPublic;
                        if (node.Structure != null) {
                            LiteralStructureType lst;
                            var tattrs = TypeAttributes.BeforeFieldInit | vis | TypeAttributes.Sealed | TypeAttributes.SequentialLayout;
                            if (node.Structure is LiteralStructureType l) {
                                td = new TypeDefinition (ns, node.Name, tattrs, sysVal);
                                lst = l;                                
                                todo.Add ((node.Module, l, td));
                            }
                            else if (node.Structure is OpaqueStructureType) {
                                td = new TypeDefinition (ns, node.Name, tattrs, sysVal);
                                lst = null;
                            }
                            else {
                                throw new NotSupportedException ($"Cannot compile {node.Structure}");
                            }

                            globalStructs[node.Symbol] = (lst, td);
                            if (!moduleStructs.TryGetValue (node.Module.Symbol, out var mstructs)) {
                                mstructs = new SymbolTable<(LiteralStructureType, TypeDefinition)> ();
                                moduleStructs.Add (node.Module.Symbol, mstructs);
                            }
                            mstructs[node.Symbol] = (lst, td);
                        }
                        else {
                            var tattrs = TypeAttributes.BeforeFieldInit | vis | TypeAttributes.Abstract | TypeAttributes.Sealed;
                            td = new TypeDefinition (ns, node.Name, tattrs, sysObj);
                        }

                        if (parentType != null) {
                            parentType.NestedTypes.Add (td);
                        }
                        else {
                            mod.Types.Add (td);
                        }
                        //Console.WriteLine ("EMIT " + td);

                        foreach (var c in node.Children) {
                            CompileStructures (ns, td, c, todo);
                        }
                    }
                }
            }
        }

        readonly List<Action> globalInits = new List<Action> ();
        readonly SymbolTable<TypeDefinition> moduleTypes = new SymbolTable<TypeDefinition> ();

        void EmitGlobalVariables ()
        {
            //var publicGlobalsType = new TypeDefinition (namespac, "Globals", TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, sysObj);
            //mod.Types.Add (publicGlobalsType);

            //
            // Define globals
            //
            foreach (var m in Modules) {

                if (m.GlobalVariables.All (x => x.Value.IsExternal) && m.FunctionDefinitions.All (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new SymbolTable<(IR.GlobalVariable Global, FieldDefinition Field)> ();
                    globals.Add (m.Symbol, mglobals);
                }

                var allVarsPrivate = m.GlobalVariables.All (x => x.Value.IsExternal || x.Value.IsPrivate);

                var moduleType = GetModuleType (m, allVarsPrivate);

                var needsInit = new List<(IR.GlobalVariable, FieldDefinition)> ();
                foreach (var kv in m.GlobalVariables) {
                    var symbol = kv.Key;
                    var g = kv.Value;
                    if (g.IsExternal)
                        continue;
                    if (globals.ContainsKey (symbol))
                        continue;
                    try {
                        var gname = new IR.MangledName (symbol);

                        var gtype = GetClrType (g.Type, module: m);
                        var field = new FieldDefinition (
                            gname.Identifier,
                            FieldAttributes.Static | (FieldAttributes.Public), gtype);

                        moduleType.Fields.Add (field);
                        mglobals.Add (symbol, (g, field));

                        if (g.Initializer != null) {
                            needsInit.Add ((g, field));
                        }
                    }
                    catch (Exception ex) {
                        ErrorMessage (m.SourceFilename, $"Failed to emit global variable `{IR.MangledName.Demangle (symbol)}` ({symbol}): {ex.Message}", ex);
                    }
                }

                if (needsInit.Count > 0) {
                    globalInits.Add (() => EmitGlobalInitializers (m, moduleType, needsInit));
                }
            }

            //
            // Link module variables
            //
            foreach (var m in Modules) {

                if (!m.GlobalVariables.Any (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new SymbolTable<(IR.GlobalVariable Global, FieldDefinition Field)> ();
                    globals.Add (m.Symbol, mglobals);
                }

                foreach (var kv in m.GlobalVariables) {

                    var symbol = kv.Key;
                    var ident = new IR.MangledName (symbol).Identifier;
                    var g = kv.Value;

                    if (!g.IsExternal)
                        continue;

                    var fieldq = from ms in globals.Values
                                from mkv in ms
                                let mg = mkv.Value
                                where mg.Field.IsPublic && mg.Field.Name == ident
                                select mg;
                    var f = fieldq.FirstOrDefault ();
                    if (f.Field == null) {
                        ErrorMessage (m.SourceFilename, $"Undefined global variable `{IR.MangledName.Demangle (symbol)}` ({symbol})");

                        var gname = new IR.MangledName (symbol);

                        var gtype = GetClrType (g.Type, module: m);
                        var field = new FieldDefinition (
                            gname.Identifier,
                            FieldAttributes.Static | (FieldAttributes.Public), gtype);

                        var moduleType = GetModuleType (m, false);
                        moduleType.Fields.Add (field);
                        mglobals.Add (symbol, (g, field));
                    }
                    if (f.Field != null) {
                        mglobals.Add (symbol, f);
                    }
                }
            }
        }

        TypeDefinition GetModuleType (Module m, bool allVarsPrivate)
        {
            var moduleTypeName = new IR.MangledName (m.Symbol).Identifier;
            var moduleType = mod.Types.FirstOrDefault (x => x.Namespace == namespac && x.Name == moduleTypeName);
            if (moduleType == null) {
                moduleTypes.TryGetValue (m.Symbol, out moduleType);
            }
            if (moduleType == null) {
                moduleType = new TypeDefinition (
                                    namespac, m.Symbol.ToString (),
                                    TypeAttributes.BeforeFieldInit | TypeAttributes.Abstract | TypeAttributes.Sealed
                                    | (allVarsPrivate ? 0 : TypeAttributes.Public),
                                    sysObj);
                mod.Types.Add (moduleType);
            }
            moduleTypes[m.Symbol] = moduleType;
            return moduleType;
        }

        void EmitGlobalInitializers ()
        {
            foreach (var i in globalInits) {
                i ();
            }
        }

        void EmitGlobalInitializers (Module m, TypeDefinition moduleGlobalsType, List<(IR.GlobalVariable, FieldDefinition)> needsInit)
        {
            var mattrs = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
            var cctor = new MethodDefinition (".cctor", mattrs, sysVoid);
            moduleGlobalsType.Methods.Add (cctor);
            var compiler = new GlobalInitializersCompiler (this, m, cctor);
            compiler.Compile (needsInit);
        }

        TypeDefinition CreateDataType ()
        {
            var td = new TypeDefinition ("", pidTypeName, TypeAttributes.AnsiClass | TypeAttributes.Sealed, sysObj);
            var compGen = new CustomAttribute (sysCompGenCtor);
            td.CustomAttributes.Add (compGen);
            mod.Types.Add (td);
            return td;
        }

        FieldDefinition AddDataField (byte[] data)
        {
            var td = dataType.Value;
            var size = data.Length;
            if (!dataFieldTypes.TryGetValue (size, out var dft)) {
                dft = new TypeDefinition ("", $"__StaticArrayInitTypeSize={size}", TypeAttributes.ExplicitLayout | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.NestedPrivate, sysVal) {
                    PackingSize = 1,
                    ClassSize = size
                };
                td.NestedTypes.Add (dft);
                dataFieldTypes[size] = dft;
            }
            var name = "D" + Guid.NewGuid ().ToString ("N").ToUpperInvariant ();
            var fd = new FieldDefinition (name, FieldAttributes.Static | FieldAttributes.Assembly | FieldAttributes.InitOnly | FieldAttributes.HasFieldRVA, dft) {
                InitialValue = data
            };
            td.Fields.Add (fd);
            return fd;
        }

        class GlobalInitializersCompiler : InnerGlobalInitializersCompiler
        {
            public GlobalInitializersCompiler (Compilation compilation, Module module, MethodDefinition methodDefinition)
                : base (compilation, module, methodDefinition)
            {
            }

            // Uncomment to make a new function for each field (helps in debugging)
            //public new void Compile (List<(IR.GlobalVariable, FieldDefinition)> needsInit)
            //{
            //    foreach (var (g, f) in needsInit) {
            //        var mattrs = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig;
            //        var cctor = new MethodDefinition ("__init" + f.Name, mattrs, compilation.sysVoid);
            //        method.DeclaringType.Methods.Add (cctor);
            //        var compiler = new InnerGlobalInitializersCompiler (compilation, module, cctor);
            //        compiler.Compile (new List<(IR.GlobalVariable, FieldDefinition)> { (g, f) });
            //        Emit (il.Create (OpCodes.Call, cctor));
            //    }
            //    Emit (OpCodes.Ret);
            //    body.Optimize ();
            //    method.Body = body;
            //}
        }

        class InnerGlobalInitializersCompiler : Emitter
        {
            public InnerGlobalInitializersCompiler (Compilation compilation, Module module, MethodDefinition methodDefinition)
                : base (compilation, module, methodDefinition)
            {
            }

            public void Compile (List<(IR.GlobalVariable, FieldDefinition)> needsInit)
            {
                var gcHandleV = new Lazy<VariableDefinition> (() => {
                    var v = new VariableDefinition (compilation.sysGCHandle);
                    body.Variables.Add (v);
                    return v;
                });

                foreach (var (g, f) in SortFields (needsInit)) {
                    try {
                        if (ShouldTrace >= 3) {
                            Emit (il.Create (OpCodes.Ldstr, $"Init Field: {f.DeclaringType}::{f.Name}"));
                            Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
                            Emit (il.Create (OpCodes.Call, compilation.sysConsoleGetOut));
                            Emit (il.Create (OpCodes.Callvirt, compilation.sysTextWriterFlush));
                        }
                        var store = il.Create (OpCodes.Stsfld, f);
                        EmitInitializer (g.Initializer, g.Type, gcHandleV, store);
                    }
                    catch (Exception ex) {
                        compilation.ErrorMessage ("", $"Failed to init global `{g.Symbol}`", ex);
                    }
                }

                Emit (OpCodes.Ret);

                body.Optimize ();
                body.InitLocals = true;
                method.Body = body;
            }

            List<(IR.GlobalVariable, FieldDefinition)> SortFields (List<(IR.GlobalVariable, FieldDefinition)> needsInit)
            {
                var r = new List<(IR.GlobalVariable, FieldDefinition)> ();
                var added = new SymbolTable<bool> ();
                var adding = new SymbolTable<bool> ();
                var all = new SymbolTable<(IR.GlobalVariable, FieldDefinition)> ();
                foreach (var i in needsInit) {
                    all[i.Item1.Symbol] = i;
                }
                void Init (GlobalSymbol symbol)
                {
                    if (added.ContainsKey (symbol))
                        return;
                    if (adding.ContainsKey (symbol))
                        return;
                    if (!all.ContainsKey (symbol))
                        return;
                    //Console.WriteLine ("INIT " + symbol);
                    var i = all[symbol];
                    var deps = i.Item1.Initializer.ReferencedGlobals;
                    adding[symbol] = true;
                    foreach (var d in deps) {
                        Init (d);
                    }
                    if (!added.ContainsKey (symbol)) {
                        r.Add (i);
                        added.Add (symbol, true);
                    }
                    adding.Remove (symbol);
                }
                foreach (var i in needsInit) {
                    Init (i.Item1.Symbol);
                }                
                return r;
            }

            readonly Dictionary<string, List<VariableDefinition>> pinnedRefs = new Dictionary<string, List<VariableDefinition>> ();

            VariableDefinition GetPinnedRef (TypeReference typeReference)
            {
                var key = typeReference.FullName;
                if (!pinnedRefs.TryGetValue (key, out var vars)) {
                    vars = new List<VariableDefinition> ();
                    pinnedRefs.Add (key, vars);
                }
                if (vars.Count > 0) {
                    var ev = vars[vars.Count - 1];
                    vars.RemoveAt (vars.Count - 1);
                    return ev;
                }
                var nv = new VariableDefinition (typeReference.MakePinnedType ());
                body.Variables.Add (nv);
                return nv;
            }

            void ReleasePinnedRef (TypeReference typeReference, VariableDefinition variable)
            {
                Emit (il.Create (OpCodes.Ldc_I4_0));
                Emit (il.Create (OpCodes.Conv_U));
                Emit (il.Create (OpCodes.Stloc, variable));
                pinnedRefs[typeReference.FullName].Add (variable);
            }

            void EmitInitializer (IR.Value initializer, LType type, Lazy<VariableDefinition> gcHandleV, Instruction store)
            {
                switch (initializer) {
                    case IR.ArrayConstant c: {
                            //Console.WriteLine ("EMIT ARRAY TO " + store);
                            var size = c.Elements.Length;
                            var et = c.Elements[0].Type;
                            var cet = compilation.GetClrType (et, module: module);
                            if (store.OpCode == OpCodes.Stfld) {
                                var operand = (FieldReference)store.Operand;
                                Emit (il.Create (OpCodes.Ldflda, operand));
                                var locRefType = operand.FieldType.MakePointerType ();
                                var locRef = GetPinnedRef (locRefType);
                                Emit (il.Create (OpCodes.Stloc, locRef));
                                Emit (il.Create (OpCodes.Ldloc, locRef));
                                Emit (il.Create (OpCodes.Conv_U));
                                for (int i = 0; i < c.Elements.Length; i++) {
                                    if (i + 1 < c.Elements.Length) {
                                        Emit (il.Create (OpCodes.Dup));
                                    }
                                    Emit (il.Create (OpCodes.Ldc_I4, i));
                                    Emit (il.Create (OpCodes.Conv_I));
                                    Emit (il.Create (OpCodes.Sizeof, cet));
                                    Emit (il.Create (OpCodes.Mul));
                                    Emit (il.Create (OpCodes.Add));
                                    var e = c.Elements[i];
                                    var storee = il.Create (OpCodes.Stobj, cet);
                                    EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                                }
                                ReleasePinnedRef (locRefType, locRef);
                            }
                            else if (store.OpCode == OpCodes.Stsfld) {
                                var field = (FieldReference)store.Operand;
                                Emit (il.Create (OpCodes.Ldc_I4, c.Elements.Length));
                                Emit (il.Create (OpCodes.Sizeof, cet));
                                Emit (il.Create (OpCodes.Mul));
                                Emit (il.Create (OpCodes.Call, compilation.sysAllocHGlobalInt));
                                Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                                Emit (store);
                                for (int i = 0; i < c.Elements.Length; i++) {
                                    Emit (il.Create (OpCodes.Ldsfld, field));
                                    Emit (il.Create (OpCodes.Ldc_I4, i));
                                    Emit (il.Create (OpCodes.Conv_I));
                                    Emit (il.Create (OpCodes.Sizeof, cet));
                                    Emit (il.Create (OpCodes.Mul));
                                    Emit (il.Create (OpCodes.Add));
                                    var e = c.Elements[i];
                                    var storee = il.Create (OpCodes.Stobj, cet);
                                    EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                                }
                            }
                            else {
                                var ocet = cet;
                                if (cet.IsPointer) {
                                    ocet = compilation.sysIntPtr;
                                }
                                Emit (il.Create (OpCodes.Ldc_I4, size));
                                Emit (il.Create (OpCodes.Ldc_I4, size));
                                Emit (il.Create (OpCodes.Newarr, ocet));
                                Emit (il.Create (OpCodes.Dup));
                                for (int i = 0; i < c.Elements.Length; i++) {
                                    var e = c.Elements[i];
                                    Emit (il.Create (OpCodes.Ldc_I4, i));
                                    if (cet.IsPointer) {
                                        var converte = il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer);
                                        EmitInitializer (e.Value, e.Type, gcHandleV, converte);
                                        Emit (il.Create (OpCodes.Stelem_Any, ocet));
                                    }
                                    else {
                                        var storee = il.Create (OpCodes.Stelem_Any, ocet);
                                        EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                                    }
                                    Emit (il.Create (OpCodes.Dup));
                                }
                                Emit (il.Create (OpCodes.Pop));
                                Emit (OpCodes.Ldc_I4_3);
                                Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                                Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                                Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                                Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                                Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                                Emit (store);
                            }
                        }
                        break;
                    case IR.ZeroConstant _ when type is Types.ArrayType art: {
                            var size = (int)art.Length;
                            var et = art.ElementType;
                            var cet = compilation.GetClrType (et, module: module);
                            var ocet = cet;
                            if (cet.IsPointer) {
                                ocet = compilation.sysIntPtr;
                            }
                            Emit (il.Create (OpCodes.Ldc_I4, size));
                            Emit (il.Create (OpCodes.Newarr, ocet));
                            Emit (OpCodes.Ldc_I4_3);
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                            Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                            Emit (store);
                        }
                        break;
                    case IR.UndefinedConstant _ when type is Types.ArrayType art: {
                            var size = (int)art.Length;
                            var et = art.ElementType;
                            var cet = compilation.GetClrType (et, module: module);
                            var ocet = cet;
                            if (cet.IsPointer) {
                                ocet = compilation.sysIntPtr;
                            }
                            Emit (il.Create (OpCodes.Ldc_I4, size));
                            Emit (il.Create (OpCodes.Newarr, ocet));
                            Emit (OpCodes.Ldc_I4_3);
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                            Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                            Emit (store);
                        }
                        break;
                    case IR.BytesConstant c: {
                            var chars = new List<byte> ();
                            var s = c.Bytes.Text;
                            var i = 2;
                            var n = s.Length - 1;
                            while (i < n) {
                                if (s[i] == '\\' && i + 1 < n && s[i + 1] == '\\') {
                                    chars.Add ((byte)'\\');
                                    i += 2;
                                }
                                else if (s[i] == '\\' && i + 2 < n) {
                                    var hex = s.Substring (i + 1, 2);
                                    var v = int.Parse (hex, System.Globalization.NumberStyles.HexNumber);
                                    var sv = Math.Min (255, Math.Max (0, v));
                                    chars.Add ((byte)sv);
                                    i += 3;
                                }
                                else {
                                    chars.Add ((byte)s[i]);
                                    i++;
                                }
                            }
                            var bytes = chars.ToArray ();
                            var isAscii = bytes.All (x => (x == '\n' || x == '\r' || x == '\0') || (x >= ' ' && x < 128));
                            var size = bytes.Length;
                            if (isAscii) {
                                var str = System.Text.Encoding.ASCII.GetString (bytes);
                                Emit (il.Create (OpCodes.Call, compilation.sysAscii));
                                Emit (il.Create (OpCodes.Ldstr, str));
                                Emit (il.Create (OpCodes.Callvirt, compilation.sysAsciiGetBytes));
                            }
                            else {
                                var dataField = compilation.AddDataField (bytes);
                                Emit (il.Create (OpCodes.Ldc_I4, size));
                                Emit (il.Create (OpCodes.Newarr, compilation.sysByte));
                                Emit (il.Create (OpCodes.Dup));
                                Emit (il.Create (OpCodes.Ldtoken, dataField));
                                Emit (il.Create (OpCodes.Call, compilation.sysRuntimeHelpersInitArray));
                            }
                            Emit (OpCodes.Ldc_I4_3);
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                            Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                            Emit (store);
                        }
                        break;
                    case IR.StructureConstant c:
                        if (store.OpCode.Code == Code.Stsfld) {
                            var f = (FieldReference)store.Operand;
                            var td = f.FieldType.Resolve ();
                            var n = c.Elements.Length;
                            for (int i = 0; i < n; i++) {
                                var e = c.Elements[i];
                                Emit (il.Create (OpCodes.Ldsflda, f));
                                var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                                EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                            }
                        }
                        else if (store.OpCode.Code == Code.Stelem_Any && type is NamedType namedType) {
                            var td = ((TypeReference)store.Operand).Resolve ();
                            var v = GetStructTempLocal (namedType);
                            var n = c.Elements.Length;
                            for (int i = 0; i < n; i++) {
                                var e = c.Elements[i];
                                Emit (il.Create (OpCodes.Ldloca, v));
                                var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                                EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                            }
                            Emit (il.Create (OpCodes.Ldloc, v));
                            Emit (store);
                        }
                        else {
                            TypeDefinition td;
                            if (store.OpCode.Code == Code.Stfld) {
                                var f = (FieldReference)store.Operand;
                                td = f.FieldType.Resolve ();
                            }
                            else {
                                td = ((TypeReference)store.Operand).Resolve ();
                            }
                            var v = GetStructTempLocal (td);
                            var n = c.Elements.Length;
                            for (int i = 0; i < n; i++) {
                                var e = c.Elements[i];
                                if (ShouldTrace >= 3) {
                                    Emit (il.Create (OpCodes.Ldstr, $"Init field #{i}: {e.Type} = {e.Value}   (for type {type})"));
                                    Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
                                }
                                Emit (il.Create (OpCodes.Ldloca, v));
                                var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                                EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                            }
                            Emit (il.Create (OpCodes.Ldloc, v));
                            Emit (store);
                        }
                        break;
                    default:
                        EmitValue (initializer, type);
                        Emit (store);
                        break;
                }
            }
        }

        void EmitSyscalls ()
        {
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Abstract | TypeAttributes.Sealed;
            var syscallstd = new TypeDefinition ("", "<CrtImplementationDetails>", tattrs, sysObj);
            mod.Types.Add (syscallstd);

            syscalls = new Syscalls (this, syscallstd);
            syscalls.Emit ();

            foreach (var iskv in syscalls.Calls) {
                externalMethodDefs[iskv.Key] = new DefinedFunction {
                    Symbol = iskv.Key,
                    IRModule = null,
                    IRDefinition = null,
                    ILDefinition = iskv.Value,
                    ParamSyms = new SymbolTable<ParameterDefinition> (),
                };
            }
        }

        void EmitNativeExceptions ()
        {
            nativeException = new Lazy<TypeDefinition> (() => {
                var td = new TypeDefinition (namespac, "Exception", TypeAttributes.Public | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit, sysException);
                var data = new FieldDefinition ("NativeData", FieldAttributes.Public, sysObj);
                td.Fields.Add (data);
                var ctor = new MethodDefinition (".ctor", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, sysVoid);
                var p = new ParameterDefinition ("data", ParameterAttributes.None, sysObj);
                ctor.Parameters.Add (p);
                var body = new MethodBody (ctor);
                var il = body.GetILProcessor ();
                il.Append (il.Create (OpCodes.Ldarg_0));
                il.Append (il.Create (OpCodes.Call, sysExceptionCtorVoid));
                il.Append (il.Create (OpCodes.Ldarg_0));
                il.Append (il.Create (OpCodes.Ldarg, p));
                il.Append (il.Create (OpCodes.Stfld, data));
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();
                ctor.Body = body;
                td.Methods.Add (ctor);
                mod.Types.Add (td);
                return td;
            });
        }

        void CreateFunctionDefinitions ()
        {
            foreach (var c in globalName.Children) {
                CreateFunctionDefinitions ("", null, c);
            }
        }

        void CreateFunctionDefinitions (string namesp, TypeDefinition parentType, NameNode node)
        {            
            if (node.IsFunction) {
                var declaringType = parentType;
                if (declaringType == null) {
                    throw new InvalidOperationException ($"No parent type for {node.Name}");
                }

                if (node.Function != null) {

                    var f = node.Function;
                    var m = node.Module;
                    var sym = f.Symbol;

                    //
                    // Create the method
                    //
                    var mident = node.Name;
                    var dbgMeth = functionDebugs[sym];
                    var mattrs = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Static;
                    var md = new MethodDefinition (mident, mattrs, GetClrType (f.ReturnType, module: m));

                    //
                    // Create parameters
                    //
                    var dbgVars = Array.Empty<object> ();                    
                    if (dbgMeth.TryGetValue (Symbol.Variables, out var o) && o is MetaSymbol) {
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

                    declaringType.Methods.Add (md);
                    //Console.WriteLine ("EMIT " + md);

                    var def = new DefinedFunction {
                        Symbol = sym,
                        IRModule = m,
                        IRDefinition = f,
                        ILDefinition = md,
                        ParamSyms = paramSyms,
                    };

                    if (f.IsExternal) {
                        externalMethodDefs[def.Symbol] = def;
                    }
                    else {
                        if (!moduleMethodDefs.TryGetValue (m.Symbol, out var mdefs)) {
                            mdefs = new SymbolTable<DefinedFunction> ();
                            moduleMethodDefs.Add (m.Symbol, mdefs);
                        }
                        mdefs[def.Symbol] = def;
                    }
                }
                else if (node.FunctionDecl != null) {
                    var sym = node.FunctionDecl.Symbol;
                    var m = node.Module;

                    if (!externalMethodDefs.ContainsKey (sym)) {

                        var f = node.FunctionDecl;
                        var mident = node.Name;

                        var mattrs = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Static;
                        var md = new MethodDefinition (mident, mattrs, GetClrType (f.ReturnType, module: m));

                        //
                        // Create parameters
                        //
                        var paramSyms = new SymbolTable<ParameterDefinition> ();
                        for (var i = 0; i < f.Parameters.Length; i++) {
                            var fp = f.Parameters[i];
                            var pname = "p" + i;
                            var pt = GetClrType (fp.ParameterType, module: m);
                            var p = new ParameterDefinition (pname, ParameterAttributes.None, pt);
                            md.Parameters.Add (p);
                            paramSyms[fp.Symbol] = p;
                        }

                        declaringType.Methods.Add (md);

                        externalMethodDefs[sym] = new DefinedFunction {
                            Symbol = sym,
                            IRModule = node.Module,
                            IRDeclaration = f,
                            ILDefinition = md,
                            ParamSyms = paramSyms,
                        };
                    }
                }
            }
            else {
                var isNamespace = node.Structure == null && parentType == null;
                if (isNamespace) {
                    isNamespace = node.Children.All (x => !x.IsFunction);
                }

                if (isNamespace) {
                    var newNamespace = namesp.Length > 0 ? namesp + "." + node.Name : node.Name;
                    foreach (var c in node.Children) {
                        CreateFunctionDefinitions (newNamespace, parentType, c);
                    }
                }
                else {
                    if (node.Structure != null || node.Children.Count > 0) {
                        TypeDefinition td;
                        if (parentType != null) {
                            td = parentType.NestedTypes.FirstOrDefault (x => x.Name == node.Name);
                            if (td == null) {
                                throw new InvalidOperationException ($"Failed to find nested {node.Name} in {parentType}");
                            }
                        }
                        else {
                            td = mod.GetType (namesp + "." + node.Name);
                        }
                        Debug.Assert (td != null);
                        foreach (var c in node.Children) {
                            CreateFunctionDefinitions (namesp, td, c);
                        }
                    }
                }
            }
        }

        void CompileFunctions ()
        {
            var methods =
                externalMethodDefs.Values.Concat (
                    from mdefs in moduleMethodDefs.Values
                    from m in mdefs.Values
                    select m).ToList ();

            foreach (var m in methods) {

                if (m.ILDefinition.HasBody && m.ILDefinition.Body.Instructions.Count > 0)
                    continue;

                //Console.WriteLine ($"{compiledFunctionCount} /// {MaxFunctions}");
                if (compiledFunctionCount >= MaxFunctions) {
                    CompileTrialFunction (m);
                }
                else if (m.IRDefinition != null) {
                    try {
                        compiledFunctionCount++;
                        var fc = new FunctionCompiler (this, m);
                        fc.CompileFunction ();
                        Messages.AddRange (fc.Messages);
                    }
                    catch (Exception ex) {
                        ErrorMessage (m.IRModule.SourceFilename, $"Failed to compile function `{IR.MangledName.Demangle (m.Symbol)}` ({m.Symbol}): {ex.Message}", ex);
                        CompileFailedFunction (m, ex);
                    }
                }
                else {
                    ErrorMessage (m.IRModule.SourceFilename, $"Undefined function `{IR.MangledName.Demangle (m.Symbol)}` ({m.Symbol})");
                    CompileMissingFunction (m);
                }
            }
        }

        void EmitEntrypoint ()
        {
            var q = from em in externalMethodDefs.Values
                    let d = em.ILDefinition
                    where d.Name == "main"
                    where d.IsStatic
                    where d.Parameters.Count == 0 || d.Parameters.Count == 2
                    orderby d.Parameters.Count descending
                    select em;
            var maind = q.FirstOrDefault ();
            if (maind != null) {
                var main = maind.ILDefinition;

                var md = new MethodDefinition ("Main", MethodAttributes.Static | MethodAttributes.Public, sysInt32);
                var em = new EntrypointEmitter (this, maind, md);
                em.Run ();

                main.DeclaringType.Methods.Add (md);
                mod.EntryPoint = md;
                hasEntryPoint = true;
            }
        }

        class EntrypointEmitter : Emitter
        {
            readonly DefinedFunction maind;

            public EntrypointEmitter (Compilation compilation, DefinedFunction main, MethodDefinition methodDefinition) : base (compilation, main.IRModule, methodDefinition)
            {
                this.maind = main;
            }

            public void Run()
            {
                var args = new ParameterDefinition ("args", ParameterAttributes.None, compilation.sysString.MakeArrayType ());
                method.Parameters.Add (args);

                foreach (var p in maind.IRDefinition.Parameters) {
                    EmitZeroValue (p.ParameterType);
                }
                il.Append (il.Create (OpCodes.Call, maind.ILDefinition));

                if (maind.ILDefinition.ReturnType.FullName == "System.Void") {
                    il.Append (il.Create (OpCodes.Ldc_I4_0));
                }

                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();
                method.Body = body;
            }
        }

        void RemoveUnusedFunctions ()
        {
            foreach (var s in syscalls.Calls) {
                if (externalMethodDefs.TryGetValue (s.Key, out var f)) {
                    if (f.ReferenceCount == 0) {
                        s.Value.DeclaringType.Methods.Remove (s.Value);
                    }
                }
            }
        }

        public readonly List<Message> Messages = new List<Message> ();

        public bool HasErrors => Messages.Exists (m => m.Type == MessageType.Error);
        public int ErrorCount => Messages.Count (m => m.Type == MessageType.Error);

        public void ErrorMessage (string filePath, string message, Exception exception = null)
        {
            var msg = new Message (message, exception);
            msg.FilePath = filePath;
            Messages.Add (msg);
        }

        public void WarningMessage (string filePath, string message)
        {
            var msg = new Message (MessageType.Warning, message);
            msg.FilePath = filePath;
            Messages.Add (msg);
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

        void CompileTrialFunction (DefinedFunction function)
        {
            var f = function.IRDefinition;

            var md = function.ILDefinition;
            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "Trial version of Iril"));
            il.Append (il.Create (OpCodes.Newobj, sysNotSuppCtor));
            il.Append (il.Create (OpCodes.Throw));

            body.Optimize ();
            md.Body = body;
        }

        void AddDebugInfoToStruct (Symbol symbol, SymbolTable<object> debugInfo, Module module)
        {
            var td = globalStructs[symbol].Item2;
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

            return GetClrType (irType, module: module, unsigned: unsigned);
        }

        public static int RoundUpIntBits (int bits) {
            if (bits > 128)
                return 256;
            if (bits > 64)
                return 128;
            if (bits > 32)
                return 64;
            if (bits > 16)
                return 32;
            if (bits > 8)
                return 16;
            return 8;
        }

        public TypeReference GetClrType (LType irType, Module module, bool? unsigned = false)
        {
            if (module == null)
                throw new ArgumentNullException (nameof (module));

            switch (irType) {
                case FloatType floatt:
                    switch (floatt.Bits) {
                        case 32:
                            return sysSingle;
                        default:
                            return sysDouble;
                    }
                case IntegerType intt:
                    switch (RoundUpIntBits (intt.Bits)) {
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
                    return GetClrType (art.ElementType, module: module).MakePointerType ();
                case Types.PointerType pt when pt.ElementType is LiteralStructureType ls && ls.Elements.Length == 0:
                    return sysVoidPtr;
                case Types.PointerType pt:
                    return GetClrType (pt.ElementType, module: module).MakePointerType ();
                case FunctionType ft:
                    return sysVoidPtr;
                case NamedType nt:
                    if (IsAnonymousTypeName (nt.Symbol)) {
                        var st = (LiteralStructureType)module.IdentifiedStructures[nt.Symbol];
                        return GetAnonymousStructType (st, module: module).ClrType;
                    }
                    else {
                        if (module != null && moduleStructs.TryGetValue (module.Symbol, out var structs)) {
                            if (structs.TryGetValue (nt.Symbol, out var lntSym))
                                return lntSym.Item2;
                        }
                        if (globalStructs.TryGetValue (nt.Symbol, out var gntSym))
                            return gntSym.Item2;
                        throw new Exception ($"Cannot find `{nt.Symbol}` from module `{module}`");
                    }
                case LiteralStructureType st:
                    return GetAnonymousStructType (st, module: module).ClrType;
                case VectorType vt:
                    return GetVectorType (vt, module: module).ClrType;
                case VoidType vdt:
                    return sysVoid;
                case VarArgsType vat:
                    return sysObjArray;
                default:
                    throw new NotSupportedException ($"Cannot get CLR type for `{irType}` ({irType?.GetType().Name})");
            }
        }

        public AnonymousStruct GetAnonymousStructType (LiteralStructureType st, Module module)
        {
            if (module is null) {
                throw new ArgumentNullException (nameof (module));
            }

            var key = st.Elements.Select (x => x.Resolve (module: module)).ToArray ();
            if (astructTypes.TryGetValue (key, out var vct)) {
                return vct;
            }
            return AddAnonymousStruct (st.Elements, key, st, module);
        }

        AnonymousStruct AddAnonymousStruct (LType[] elements, LType[] resolvedElements, LiteralStructureType st, Module module)
        {
            var n = elements.Length;
            var tname = $"Struct{n}_{string.Join("_", resolvedElements.Select(GetLTypeClrIdentifier))}";

            var ns = namespac + ".AnonymousTypes";
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public;
            var td = new TypeDefinition (ns, tname, tattrs, sysVal);
            td.IsExplicitLayout = true;
            var elementsClrTypes = new List<TypeReference> (n);
            var offset = 0;
            for (var i = 0; i < n; i++) {
                int size;
                TypeReference clrType;
                if (resolvedElements[i] is Types.ArrayType arrt) {
                    var e = arrt.ElementType;
                    size = (int)(e.GetByteSize (module: module) * arrt.Length);
                    clrType = GetClrType (e, module: module);
                }
                else {
                    size = (int)resolvedElements[i].GetByteSize(module: module);
                    clrType = GetClrType (resolvedElements[i], module: module);
                }
                elementsClrTypes.Add (clrType);
                string pre = GetFieldPrefix (resolvedElements[i]);
                var f = new FieldDefinition (pre + i, FieldAttributes.Public, clrType);
                offset = Align (offset, resolvedElements[i], (int)size, module: module);
                f.Offset = offset;
                td.Fields.Add (f);
                offset += size;
            }
            td.ClassSize = offset;
            td.PackingSize = (short)module.PointerByteSize;

            var r = new AnonymousStruct {
                ElementClrTypes = elementsClrTypes.ToArray (),
                ClrType = td,
                ElementFields = td.Fields.Select (x => (FieldReference)x).ToArray (),
            };

            mod.Types.Add (td);
            astructTypes[resolvedElements] = r;

            return r;
        }

        static string GetFieldPrefix (LType lType)
        {
            switch (lType) {
                case Types.ArrayType t:
                    return GetFieldPrefix (t.ElementType) + "Array";
                case Types.FunctionType _:
                    return "Function";
                case Types.PointerType t:
                    return GetFieldPrefix (t.ElementType) + "Pointer";
                case Types.IntegerType t when t.Bits == 8:
                    return "Byte";
                case Types.IntegerType t:
                    return "Integer";
                case Types.FloatType t:
                    return "Float";
                case Types.NamedType _:
                case Types.StructureType _:
                    return "Struct";
                default:
                    return "F";
            }
        }

        static int Align (int offset, LType dataType, int dataTypeByteSize, Module module)
        {
            int alignment = dataType.GetAlignment (module);
            while ((offset % alignment) != 0)
                offset++;
            return offset;
        }

        static string GetLTypeClrIdentifier (LType type)
        {
            switch (type) {
                case Types.NamedType t:
                    return IR.MangledName.Demangle (t.Symbol);
                case Types.FloatType t:
                    return t.Symbol.Text;
                case Types.IntegerType t:
                    return t.Symbol.Text;
                case Types.FunctionType t:
                    return "f" + t.ParameterTypes.Length + GetLTypeClrIdentifier (t.ReturnType) + string.Join ("", t.ParameterTypes.Select (GetLTypeClrIdentifier));
                case Types.LiteralStructureType t:
                    return "s" + string.Join ("", t.Elements.Select (GetLTypeClrIdentifier));
                case Types.PointerType t:
                    return GetLTypeClrIdentifier (t.ElementType) + "p";
                case Types.ArrayType t:
                    return "a" + t.Length + GetLTypeClrIdentifier (t.ElementType);
                default:
                    return type.ToString ();
            }
        }

        public SimdVector GetVectorType (VectorType vt, Module module)
        {
            var et = GetClrType (vt.ElementType, module: module);
            var key = (vt.Length, et.FullName);
            if (vectorTypes.TryGetValue (key, out var vct)) {
                return vct;
            }
            return AddVectorType (key, vt, et, module);
        }

        SimdVector AddVectorType ((int Length, string TypeFullName) key, VectorType irType, TypeReference elementType, Module module)
        {
            var tname = $"Vector{key.Length}{elementType.Name}";

            var td = new TypeDefinition (pidTypeName, tname, TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout, sysVal);

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
                ("ToInt8", OpCodes.Conv_I1, new VectorType (irType.Length, IntegerType.I8)),
                ("ToInt16", OpCodes.Conv_I2, new VectorType (irType.Length, IntegerType.I16)),
                ("ToInt32", OpCodes.Conv_I4, new VectorType (irType.Length, IntegerType.I32)),
                ("ToInt64", OpCodes.Conv_I8, new VectorType (irType.Length, IntegerType.I64)),
            };
            foreach (var (name, opcode, vt) in unopMethods) {
                var cvt = GetClrType (vt, module: module);
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
            MethodReference cmpctor = null;
            if (elementType.FullName != "System.Boolean") {
                var cmpt = GetClrType (new VectorType (irType.Length, IntegerType.I1), module: module);
                cmpctor = cmpt.Resolve ().Methods.First (x => x.Name == ".ctor" && x.Parameters.Count > 0);
            }
            var opMethods = new (string, MethodReference, OpCode[])[] {
                ("Add", ctor, new[] { OpCodes.Add }),
                ("Subtract", ctor, new[] { OpCodes.Sub }),
                ("Multiply", ctor, new[] { OpCodes.Mul }),
                ("Divide", ctor, new[] { OpCodes.Div }),
                ("IcmpNotEqual", cmpctor, new[] { OpCodes.Ceq, OpCodes.Ldc_I4_0, OpCodes.Ceq }),
                ("IcmpSignedLessThan", cmpctor, new[] { OpCodes.Clt }),
                ("IcmpSignedGreaterThan", cmpctor, new[] { OpCodes.Cgt }),
                ("FcmpOrderedLessThan", cmpctor, new[] { OpCodes.Clt }),
                ("FcmpOrderedGreaterThan", cmpctor, new[] { OpCodes.Cgt }),
            };
            foreach (var (name, c, opcodes) in opMethods)
            {
                if (c == null)
                    continue;
                var mop = new MethodDefinition (name, MethodAttributes.Public | MethodAttributes.Static, c.DeclaringType);
                mop.Parameters.Add (new ParameterDefinition ("a", ParameterAttributes.None, td));
                mop.Parameters.Add (new ParameterDefinition ("b", ParameterAttributes.None, td));

                var body = new MethodBody (mop);
                var il = body.GetILProcessor ();
                for (var i = 0; i < key.Length; i++) {
                    il.Append (il.Create (OpCodes.Ldarg_0));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (OpCodes.Ldarg_1));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    foreach (var opcode in opcodes) {
                        il.Append (il.Create (opcode));
                    }
                }
                il.Append (il.Create (OpCodes.Newobj, c));
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                mop.Body = body;
                td.Methods.Add (mop);
                typeof (SimdVector).GetField (name).SetValue (r, mop);
            }

            var select = new MethodDefinition ("Select", MethodAttributes.Public | MethodAttributes.Static, ctor.DeclaringType);
            {
                var btd = GetClrType (new VectorType (key.Length, Types.IntegerType.I1), module: module).Resolve ();
                select.Parameters.Add (new ParameterDefinition ("s", ParameterAttributes.None, btd));
                select.Parameters.Add (new ParameterDefinition ("a", ParameterAttributes.None, td));
                select.Parameters.Add (new ParameterDefinition ("b", ParameterAttributes.None, td));

                var body = new MethodBody (select);
                var il = body.GetILProcessor ();
                var next = il.Create (OpCodes.Ldarg_0);
                var last = il.Create (OpCodes.Newobj, ctor);
                for (var i = 0; i < key.Length; i++) {
                    il.Append (next);
                    var loadTrue = il.Create (OpCodes.Ldarg_1);
                    next = i + 1 < key.Length ? il.Create (OpCodes.Ldarg_0) : last;

                    il.Append (il.Create (OpCodes.Ldfld, btd.Fields[i]));
                    il.Append (il.Create (OpCodes.Brtrue, loadTrue));

                    il.Append (il.Create (OpCodes.Ldarg_2));
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                    il.Append (il.Create (OpCodes.Br, next));

                    il.Append (loadTrue);
                    il.Append (il.Create (OpCodes.Ldfld, td.Fields[i]));
                }
                il.Append (next);
                il.Append (il.Create (OpCodes.Ret));
                body.Optimize ();

                select.Body = body;
                td.Methods.Add (select);
                r.Select = select;
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

            if (hasEntryPoint) {
                var text = @"{
    ""runtimeOptions"": {
        ""tfm"": ""netcoreapp2.1"",
        ""framework"": {
            ""name"": ""Microsoft.NETCore.App"",
            ""version"": ""2.1.0""
        }
    }
}
";
                var rtjsonPath = Path.ChangeExtension (path, ".runtimeconfig.json");
                File.WriteAllText (rtjsonPath, text);
            }
        }
    }
}
