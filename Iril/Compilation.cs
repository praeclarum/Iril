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
using System.Collections;
using StdLib;
using Mono.CompilerServices.SymbolWriter;
using System.Transactions;
using System.Globalization;

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
        public MethodReference sysRuntimeHelpersInitArray;
        TypeReference sysVoidPtr;
        TypeReference sysVoidPtrPtr;
        public TypeReference sysObj;
        public MethodReference sysObjToString;
        public MethodReference sysObjFinalize;
        public TypeReference sysObjArray;
        TypeReference sysVal;
        TypeReference sysBoolean;
        TypeReference sysSByte;
        public TypeReference sysByte;
        public TypeReference sysBytePtr;
        public TypeReference sysByteArray;
        TypeReference sysInt16;
        TypeReference sysUInt16;
        public TypeReference sysInt32;
        public TypeReference sysUInt32;
        TypeReference sysInt64;
        TypeReference sysUInt64;
        public TypeReference sysIntPtr;
        TypeReference sysSingle;
        TypeReference sysDouble;
        TypeReference sysString;
        TypeReference sysChar;
        TypeReference sysCharArray;
        MethodReference sysStringToCharArray;
        TypeReference sysCompGen;
        MethodReference sysCompGenCtor;
        public TypeReference sysGCHandle;
        TypeReference sysGCHandleType;
        public MethodReference sysGCHandleAlloc;
        public MethodReference sysGCHandleAddrOfPinnedObject;
        TypeReference sysGC;
        MethodReference sysGCSuppressFinalize;
        TypeReference sysIDisposable;
        TypeReference sysNotImpl;
        MethodReference sysNotImplCtor;
        TypeReference sysNotSupp;
        public MethodReference sysNotSuppCtor;
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
        public MethodReference sysMathMinSByte;
        public MethodReference sysMathMinInt16;
        public MethodReference sysMathMinInt32;
        public MethodReference sysMathMinInt64;
        public MethodReference sysMathMinByte;
        public MethodReference sysMathMinUInt16;
        public MethodReference sysMathMinUInt32;
        public MethodReference sysMathMinUInt64;
        public MethodReference sysMathMinS;
        public MethodReference sysMathMinD;
        public MethodReference sysMathMaxSByte;
        public MethodReference sysMathMaxInt16;
        public MethodReference sysMathMaxInt32;
        public MethodReference sysMathMaxInt64;
        public MethodReference sysMathMaxByte;
        public MethodReference sysMathMaxUInt16;
        public MethodReference sysMathMaxUInt32;
        public MethodReference sysMathMaxUInt64;
        public MethodReference sysMathMaxS;
        public MethodReference sysMathMaxD;
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
        public MethodReference sysStreamWriteByte;
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

        readonly Lazy<TypeDefinition> longjmpException;
        public TypeDefinition LongjmpException => longjmpException.Value;

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

        readonly SymbolTable<ModuleGlobalInfo> globals =
            new SymbolTable<ModuleGlobalInfo> ();

        int compiledFunctionCount;

        public int CompiledFunctionCount => compiledFunctionCount;

        public bool TryGetGlobal (Symbol module, Symbol symbol, out (ModuleGlobalInfo Module, IR.GlobalVariable Global, FieldDefinition Field) global)
        {
            if (globals.TryGetValue (module, out var info)) {
                if (info.GlobalFields.TryGetValue (symbol, out var globali)) {
                    global = (info, globali.Global, globali.Field);
                    return true;
                }
                return info.ExternalGlobals.TryGetValue (symbol, out global);
            }
            global = (null, null, null);
            return false;
        }

        Syscalls syscalls;
        public MethodDefinition GetSystemMethod (Symbol symbol)
        {
            if (externalMethodDefs.TryGetValue (symbol, out var f)) {
                f.ReferenceCount++;
                return f.ILDefinition;
            }
            var methods = String.Join(", ", externalMethodDefs.Select(x => x.Key));
            throw new KeyNotFoundException ($"Failed to find system method `{symbol}` ({symbol.GetType()}): {methods}");
        }

        readonly Lazy<TypeDefinition> dataType;
        readonly Dictionary<int, TypeDefinition> dataFieldTypes = new Dictionary<int, TypeDefinition> ();

        readonly SymbolTable<Library> libraries;

        public int MaxFunctions { get; set; } = int.MaxValue;

        readonly CompilationOptions options;
        public CompilationOptions Options => options;

        readonly Dictionary<MethodDefinition, int> functionTokens = new Dictionary<MethodDefinition, int> ();

        public MethodDefinition LoadFunction { get; private set; }

        public Compilation (CompilationOptions options)
        {
            this.options = options;
            Modules = options.Modules;
            AssemblyName = options.AssemblyName;
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
            longjmpException = new Lazy<TypeDefinition> (CreateLongjmpException);

            globalName = new NameNode {
                Name = "global"
            };

            libraries = Library.StandardLibraries;
        }

        public void Compile ()
        {
            FindSystemTypes ();
            DeclareLoadFunction ();
            CreateSyscalls ();
            ImportAssemblies ();
            FindStructures ();
            FindFunctions ();
            //PrintNameTree ();
            CompileStructures ();
            EmitSyscalls ();
            EmitNativeExceptions ();
            EmitGlobalVariables ();
            CreateFunctionDefinitions ();
            EmitGlobalInitializers ();
            EmitInstance ();
            CompileFunctions ();
            EmitLoadFunction ();
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
            
            sysVoid = Import ("System.Void");
            sysObj = Import ("System.Object");
            sysVal = Import ("System.ValueType");
            sysBoolean = Import ("System.Boolean");
            sysSByte = Import ("System.SByte");
            sysByte = Import ("System.Byte");
            sysInt16 = Import ("System.Int16");
            sysUInt16 = Import ("System.UInt16");
            sysInt32 = Import ("System.Int32");
            sysUInt32 = Import ("System.UInt32");
            sysInt64 = Import ("System.Int64");
            sysUInt64 = Import ("System.UInt64");
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
            sysObjFinalize = ImportMethod (sysObj, sysVoid, "Finalize");
            sysChar = Import ("System.Char");
            sysBytePtr = sysByte.MakePointerType ();
            sysByteArray = sysByte.MakeArrayType ();
            sysCharArray = sysChar.MakeArrayType ();
            sysObjArray = sysObj.MakeArrayType ();
            sysStringToCharArray = ImportMethod (sysString, sysCharArray, "ToCharArray");
            sysRuntimeFieldHandle = Import ("System.RuntimeFieldHandle");
            sysRuntimeHelpers = Import ("System.Runtime.CompilerServices.RuntimeHelpers");
            sysRuntimeHelpersInitArray = ImportMethod (sysRuntimeHelpers, sysVoid, "InitializeArray", sysArray, sysRuntimeFieldHandle);
            sysIDisposable = Import ("System.IDisposable");
            sysGC = Import ("System.GC");
            sysGCSuppressFinalize = ImportMethod (sysGC, sysVoid, "SuppressFinalize", sysObj);
            sysGCHandle = Import ("System.Runtime.InteropServices.GCHandle");
            sysGCHandleType = Import ("System.Runtime.InteropServices.GCHandleType");
            sysGCHandleAlloc = ImportMethod (sysGCHandle, sysGCHandle, "Alloc", sysObj, sysGCHandleType);
            sysGCHandleAddrOfPinnedObject = ImportMethod (sysGCHandle, sysIntPtr, "AddrOfPinnedObject");
            sysDebugger = Import ("System.Diagnostics.Debugger");
            sysDebuggerBreak = ImportMethod (sysDebugger, sysVoid, "Break");
            sysStream = Import ("System.IO.Stream");
            sysStreamRead = ImportMethod (sysStream, sysInt32, "Read", sysByteArray, sysInt32, sysInt32);
            sysStreamWrite = ImportMethod (sysStream, sysVoid, "Write", sysByteArray, sysInt32, sysInt32);
            sysStreamWriteByte = ImportMethod (sysStream, sysVoid, "WriteByte", sysByte);
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
            sysMathMaxSByte = ImportMethod (sysMath, sysSByte, "Max", sysSByte, sysSByte);
            sysMathMaxInt16 = ImportMethod (sysMath, sysInt16, "Max", sysInt16, sysInt16);
            sysMathMaxInt32 = ImportMethod (sysMath, sysInt32, "Max", sysInt32, sysInt32);
            sysMathMaxInt64 = ImportMethod (sysMath, sysInt64, "Max", sysInt64, sysInt64);
            sysMathMaxByte = ImportMethod (sysMath, sysByte, "Max", sysByte, sysByte);
            sysMathMaxUInt16 = ImportMethod (sysMath, sysUInt16, "Max", sysUInt16, sysUInt16);
            sysMathMaxUInt32 = ImportMethod (sysMath, sysUInt32, "Max", sysUInt32, sysUInt32);
            sysMathMaxUInt64 = ImportMethod (sysMath, sysUInt64, "Max", sysUInt64, sysUInt64);
            sysMathMaxS = ImportMethod (sysMath, sysSingle, "Max", sysSingle, sysSingle);
            sysMathMaxD = ImportMethod (sysMath, sysDouble, "Max", sysDouble, sysDouble);
            sysMathMinSByte = ImportMethod (sysMath, sysSByte, "Min", sysSByte, sysSByte);
            sysMathMinInt16 = ImportMethod (sysMath, sysInt16, "Min", sysInt16, sysInt16);
            sysMathMinInt32 = ImportMethod (sysMath, sysInt32, "Min", sysInt32, sysInt32);
            sysMathMinInt64 = ImportMethod (sysMath, sysInt64, "Min", sysInt64, sysInt64);
            sysMathMinByte = ImportMethod (sysMath, sysByte, "Min", sysByte, sysByte);
            sysMathMinUInt16 = ImportMethod (sysMath, sysUInt16, "Min", sysUInt16, sysUInt16);
            sysMathMinUInt32 = ImportMethod (sysMath, sysUInt32, "Min", sysUInt32, sysUInt32);
            sysMathMinUInt64 = ImportMethod (sysMath, sysUInt64, "Min", sysUInt64, sysUInt64);
            sysMathMinS = ImportMethod (sysMath, sysSingle, "Min", sysSingle, sysSingle);
            sysMathMinD = ImportMethod (sysMath, sysDouble, "Min", sysDouble, sysDouble);
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

        TypeReference Import (string name)
        {
            var types = sysAsm.MainModule.ExportedTypes;
            var scope = sysAsm.MainModule.Types.First ().Scope;
            var et = types.FirstOrDefault (x =>
                x.FullName == name);
            if (et == null) {
                throw new Exception ($"Cannot find imported type `{name}`");
            }
            var rt = et.Resolve ();
            var t = new TypeReference (et.Namespace, et.Name, sysAsm.MainModule, scope) {
                IsValueType = rt.IsValueType,
            };
            if (rt.HasGenericParameters) {
                foreach (var g in rt.GenericParameters) {
                    Console.WriteLine (g.Owner);
                    t.GenericParameters.Add (new GenericParameter (g.Name, t));
                }
            }
            return mod.ImportReference (t);
        }
        MethodReference ImportMethod (TypeReference declType, TypeReference returnType, string name, params TypeReference[] argTypes)
        {
            var td = (declType.IsGenericInstance ? declType.GetElementType() : declType).Resolve ();
            var ms = td.Methods.Where (x => x.Name == name);
            foreach (var m in ms) {
                if (m.Parameters.Count != argTypes.Length)
                    continue;
                var match = m.ReturnType.IsGenericParameter ? true : (m.ReturnType.FullName == returnType.FullName);
                for (var i = 0; match && i < m.Parameters.Count; i++) {
                    var p = m.Parameters[i];
                    if (!p.ParameterType.IsGenericParameter && p.ParameterType.FullName != argTypes[i].FullName)
                        match = false;
                }
                if (match) {
                    var mr = new MethodReference (name, returnType, declType);
                    mr.ExplicitThis = m.ExplicitThis;
                    mr.CallingConvention = m.CallingConvention;
                    mr.HasThis = m.HasThis;
                    for (var i = 0; i < argTypes.Length; i++) {
                        var p = argTypes[i];
                        if (p.IsGenericParameter) {
                            mr.Parameters.Add (new ParameterDefinition (m.Parameters[i].ParameterType));
                        }
                        else {
                            mr.Parameters.Add (new ParameterDefinition (p));
                        }
                    }
                    var imr = mod.ImportReference (mr);
                    return imr;
                }
            }
            throw new Exception ($"Cannot find {name} in {declType}");
        }

        void DeclareLoadFunction ()
        {
            var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static;
            LoadFunction = new MethodDefinition ("LoadFunction", mattrs, sysBytePtr);
        }

        readonly SymbolTable<Mono.Cecil.Cil.Document> fileDocuments = new SymbolTable<Mono.Cecil.Cil.Document> ();

        public Mono.Cecil.Cil.Document GetScopeDocument (Module module, MetaSymbol scopeRef)
        {
            if (module.Metadata.TryGetValue (scopeRef, out var scopeO) && scopeO is SymbolTable<object> scope) {
                if (scope.TryGetValue (Symbol.File, out var fileO) && fileO is MetaSymbol fileRef) {
                    Symbol fullFileRef = module.Symbol.Text + "." + fileRef;
                    if (fileDocuments.TryGetValue (fullFileRef, out var doc))
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

                        fileDocuments[fullFileRef] = doc;
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
                    // Prefer StdLib functions
                    //
                    if (externalMethodDefs.ContainsKey (sym)) {
                        // Console.WriteLine($"Skipping {sym} because it is an external method");
                        continue;
                    }

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
                    var sym = iskv.Key;

                    if (sym.Text.StartsWith ("@llvm.", StringComparison.Ordinal))
                        continue;
                    if (functionNodes.ContainsKey (sym))
                        continue;
                    if (externalMethodDefs.ContainsKey (sym)) {
                        // Console.WriteLine($"Skipping {sym} decl because it is an external method");
                        continue;
                    }

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
                        a = new[] { namespac, "Modules", IR.MangledName.SanitizeIdentifier (nn.Module.Symbol.Text) };
                    }
                }
                AddNameNode (nn, a);
            }
        }

        bool IsAnonymousTypeName (Symbol symbol)
        {
            return symbol.Text.IndexOf (".anon", StringComparison.Ordinal) > 0;
        }

        void CreateSyscalls ()
        {
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Abstract | TypeAttributes.Sealed;
            var syscallstd = new TypeDefinition ("", "<CrtImplementationDetails>", tattrs, sysObj);
            mod.Types.Add (syscallstd);
            syscalls = new Syscalls (this, syscallstd);
        }

        void FindStructures ()
        {
            syscalls.FindStructures (Modules);

            var allModules = new[] { syscalls.Module }.Concat (Modules).ToArray ();

            var externals = new SymbolTable<NameNode> ();

            foreach (var m in allModules) {
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
                    offset = m.Align (offset, e, (int)byteSize);
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

        readonly List<Func<MethodDefinition>> globalInits = new List<Func<MethodDefinition>> ();
        readonly SymbolTable<TypeDefinition> moduleTypes = new SymbolTable<TypeDefinition> ();

        void EmitGlobalVariables ()
        {
            var allModules = new[] { syscalls.Module }.Concat (Modules).ToArray ();

            TypeDefinition allDataType = null;
            if (options.Reentrant) {
                allDataType = GetAllModulesDataType ();
            }

            //
            // Define globals
            //
            foreach (var m in allModules) {

                if (m.GlobalVariables.All (x => x.Value.IsExternal) && m.FunctionDefinitions.All (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new ModuleGlobalInfo ();
                    globals.Add (m.Symbol, mglobals);
                }

                var allVarsPrivate = m.GlobalVariables.All (x => x.Value.IsExternal || x.Value.IsPrivate);

                var moduleTypeName = new IR.MangledName (m.Symbol).Identifier;
                var moduleType = GetModuleType (m, allVarsPrivate);

                //
                // Find the defined (not external) global variables
                //
                var globs = new List<(Symbol, IR.GlobalVariable)> ();
                var needsInit = new List<(IR.GlobalVariable, FieldDefinition)> ();
                foreach (var g in m.OrderedGlobalVariables) {
                    var symbol = g.Symbol;
                    if (g.IsExternal)
                        continue;
                    globs.Add ((symbol, g));
                }

                //
                // If there aren't any, no need to generate a data class
                //
                if (globs.Count == 0)
                    continue;

                //
                // Create a data type to store the global variables
                //
                var (moduleStruct, _) = CreateStructType ("", "ModuleData", false, globs.Select(x => x.Item2.Type).ToArray(), m);
                moduleStruct.Attributes = (moduleStruct.Attributes & ~TypeAttributes.Public) | TypeAttributes.NestedPublic;
                var n = globs.Count;
                var gfields = new SymbolTable<(IR.GlobalVariable, FieldDefinition)> ();
                for (var i = 0; i < n; i++) {
                    var s = globs[i].Item1;
                    var f = moduleStruct.Fields[i];
                    f.Name = new IR.MangledName (s).Identifier;
                    var g = globs[i].Item2;
                    gfields[s] = (g, f);
                    if (g.Initializer != null) {
                        needsInit.Add ((g, f));
                    }
                }
                moduleType.NestedTypes.Add (moduleStruct);

                if (allDataType != null) {
                    //
                    // Create a field in the all data type to store the module data
                    //
                    var f = new FieldDefinition (moduleTypeName, FieldAttributes.Public, moduleStruct);
                    allDataType.Fields.Add (f);
                    mglobals.AllDataField = f;
                }
                else {
                    //
                    // Create a variable to hold a reference to the data
                    //
                    var dataFieldType = moduleStruct.MakePointerType ();
                    var dataField = new FieldDefinition ("Data", FieldAttributes.Public | (options.Reentrant ? 0 : FieldAttributes.Static), dataFieldType);
                    moduleType.Fields.Add (dataField);
                    mglobals.DataField = dataField;
                }

                //
                // Remember this data
                //
                mglobals.NeedsInit = needsInit;
                mglobals.DataType = moduleStruct;
                mglobals.GlobalFields = gfields;

                //
                // Add the initializers to the global list of inits
                //
                globalInits.Add (() => EmitGlobalInitializers (m, moduleType, mglobals));
            }

            //
            // Link external module variables
            //
            foreach (var m in allModules) {

                if (!m.GlobalVariables.Any (x => x.Value.IsExternal))
                    continue;

                if (!globals.TryGetValue (m.Symbol, out var mglobals)) {
                    mglobals = new ModuleGlobalInfo ();
                    globals.Add (m.Symbol, mglobals);
                }

                foreach (var g in m.OrderedGlobalVariables) {

                    var symbol = g.Symbol;

                    if (!g.IsExternal)
                        continue;

                    var fieldq = from ms in globals.Values
                                 where ms.GlobalFields.ContainsKey (symbol)
                                 let mg = ms.GlobalFields[symbol]
                                 select (ms, mg.Global, mg.Field);
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
                        mglobals.GlobalFields.Add (symbol, (g, field));
                    }
                    if (f.Field != null) {
                        mglobals.ExternalGlobals.Add (symbol, f);
                    }
                }
            }
        }

        TypeDefinition allModulesDataType;
        TypeDefinition GetAllModulesDataType ()
        {
            if (allModulesDataType != null)
                return allModulesDataType;
            var ns = namespac;
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public;
            var td = new TypeDefinition (ns, "GlobalData", tattrs, sysVal);
            mod.Types.Add (td);
            allModulesDataType = td;
            return td;
        }
        Mono.Cecil.PointerType globalDataPointerType;
        public Mono.Cecil.PointerType GetGlobalDataPointerType ()
        {
            if (globalDataPointerType != null)
                return globalDataPointerType;
            var t = GetAllModulesDataType ().MakePointerType ();
            globalDataPointerType = t;
            return t;
        }

        TypeDefinition instanceType;
        FieldDefinition instanceDataField;
        FieldDefinition instanceDisposedField;

        TypeDefinition GetInstanceType ()
        {
            if (instanceType != null)
                return instanceType;
            var ns = namespac;
            var tattrs = TypeAttributes.Public | TypeAttributes.BeforeFieldInit;
            var td = new TypeDefinition (ns, "Instance", tattrs, sysObj);
            td.Interfaces.Add (new InterfaceImplementation (sysIDisposable));

            var allDataType = GetAllModulesDataType ();
            var allDataField = new FieldDefinition ("Data", FieldAttributes.Public, allDataType.MakePointerType ());
            instanceDataField = allDataField;
            td.Fields.Add (allDataField);

            var disposedField = new FieldDefinition ("disposed", FieldAttributes.Private, sysBoolean);
            instanceDisposedField = disposedField;
            td.Fields.Add (disposedField);

            mod.Types.Add (td);
            instanceType = td;
            return td;
        }

        TypeDefinition GetModuleType (Module m, bool allVarsPrivate)
        {
            var moduleTypeName = new IR.MangledName (m.Symbol).Identifier;
            var ns = namespac + ".Modules";
            var moduleType = mod.Types.FirstOrDefault (x => x.Namespace == ns && x.Name == moduleTypeName);
            if (moduleType == null) {
                moduleTypes.TryGetValue (m.Symbol, out moduleType);
            }
            if (moduleType == null) {
                var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed;
                moduleType = new TypeDefinition (
                                    ns, m.Symbol.ToString (),
                                    tattrs,
                                    sysObj);
                mod.Types.Add (moduleType);
            }
            moduleTypes[m.Symbol] = moduleType;
            return moduleType;
        }

        readonly List<MethodDefinition> moduleInitMethods = new List<MethodDefinition> ();

        void EmitGlobalInitializers ()
        {
            foreach (var i in globalInits) {
                if (i () is MethodDefinition m)
                    moduleInitMethods.Add (m);
            }
        }

        MethodDefinition EmitGlobalInitializers (Module m, TypeDefinition moduleGlobalsType, ModuleGlobalInfo info)
        {
            try {
                var mattrs = MethodAttributes.HideBySig | MethodAttributes.Static;
                var mname = "Initialize";
                if (options.Reentrant) {
                    mattrs |= MethodAttributes.Public;
                }
                else {
                    mattrs |= MethodAttributes.Private | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
                    mname = ".cctor";
                }
                var ctor = new MethodDefinition (mname, mattrs, sysVoid);
                if (options.Reentrant) {
                    var globalsType = GetGlobalDataPointerType ();
                    var pdata = new ParameterDefinition("globals", ParameterAttributes.None, globalsType);
                    ctor.Parameters.Add (pdata);
                }
                moduleGlobalsType.Methods.Add (ctor);
                var compiler = new GlobalInitializersCompiler (this, m, ctor, info);
                compiler.Compile ();
                return ctor;
            }
            catch (Exception ex) {
                ErrorMessage (m.SourceFilename, "Failed to compile global initializers", ex);
                return null;
            }
        }

        void EmitInstance ()
        {
            if (!options.Reentrant)
                return;

            var instanceType = GetInstanceType ();
            var dataPointerType = GetGlobalDataPointerType ();

            EmitInstCtor ();
            var disposeBool = EmitDisposeBool ();
            var dispose = EmitDispose ();
            EmitFinalize ();
            EmitInstExternals ();

            MethodDefinition EmitFinalize () {
                var md = new MethodDefinition ("Finalize", MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.Virtual, sysVoid);
                var b = new MethodBody (md);
                var il = b.GetILProcessor ();
                var ret = il.Create (OpCodes.Ret);
                var tryStart = il.Create (OpCodes.Ldarg_0);
                il.Append (tryStart);
                il.Emit (OpCodes.Ldc_I4_0);
                il.Emit (OpCodes.Callvirt, disposeBool);
                il.Emit (OpCodes.Leave, ret);
                var hStart = il.Create (OpCodes.Ldarg_0);
                il.Append (hStart);
                il.Emit (OpCodes.Call, sysObjFinalize);
                il.Emit (OpCodes.Endfinally);
                il.Append (ret);
                b.ExceptionHandlers.Add (new ExceptionHandler (ExceptionHandlerType.Finally) {
                    TryStart = tryStart,
                    TryEnd = hStart,
                    HandlerStart = hStart,
                    HandlerEnd = ret,
                });
                b.Optimize ();
                md.Body = b;
                instanceType.Methods.Add (md);
                return md;
            }

            MethodDefinition EmitDispose () {
                var md = new MethodDefinition ("Dispose", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, sysVoid);
                var b = new MethodBody (md);
                var il = b.GetILProcessor ();
                var ret = il.Create (OpCodes.Ret);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Ldc_I4_1);
                il.Emit (OpCodes.Callvirt, disposeBool);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Call, sysGCSuppressFinalize);
                il.Append (ret);
                b.Optimize ();
                md.Body = b;
                instanceType.Methods.Add (md);
                return md;
            }

            MethodDefinition EmitDisposeBool () {
                var md = new MethodDefinition ("Dispose", MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, sysVoid);
                var paramDisposing = new ParameterDefinition ("disposing", ParameterAttributes.None, sysBoolean);
                md.Parameters.Add (paramDisposing);
                var b = new MethodBody (md);
                var il = b.GetILProcessor ();
                var ret = il.Create (OpCodes.Ret);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Ldfld, instanceDisposedField);
                il.Emit (OpCodes.Brtrue, ret);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Ldfld, instanceDataField);
                il.Emit (OpCodes.Call, sysIntPtrFromPointer);
                il.Emit (OpCodes.Call, sysFreeHGlobal);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Ldc_I4_0);
                il.Emit (OpCodes.Conv_U);
                il.Emit (OpCodes.Stfld, instanceDataField);
                il.Emit (OpCodes.Ldarg_0);
                il.Emit (OpCodes.Ldc_I4_1);
                il.Emit (OpCodes.Stfld, instanceDisposedField);
                il.Append (ret);
                b.Optimize ();
                md.Body = b;
                instanceType.Methods.Add (md);
                return md;
            }

            void EmitInstExternals () {
                var externalMethods =
                    mod.GetType(namespac + ".Globals").Methods
                    .Where (x => x.IsStatic && x.IsPublic && x.Parameters.Count > 0 && x.Parameters[0].ParameterType.IsPointer && x.Parameters[0].ParameterType == dataPointerType);
                foreach (var m in externalMethods) {
                    var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public;
                    var mname = m.Name;
                    var md = new MethodDefinition (mname, mattrs, m.ReturnType);
                    for (int i = 1; i < m.Parameters.Count; i++) {
                        var p = m.Parameters[i];
                        var pdef = new ParameterDefinition (p.Name, p.Attributes, p.ParameterType);
                        md.Parameters.Add (pdef);
                    }
                    var b = new MethodBody (md);
                    var il = b.GetILProcessor ();
                    il.Emit (OpCodes.Ldarg_0);
                    il.Emit (OpCodes.Ldfld, instanceDataField);
                    for (int i = 1; i < m.Parameters.Count; i++) {
                        il.Emit (OpCodes.Ldarg, i);
                    }
                    il.Emit (OpCodes.Call, m);
                    il.Emit (OpCodes.Ret);
                    b.Optimize ();
                    md.Body = b;
                    instanceType.Methods.Add (md);
                }
            }

            void EmitInstCtor () {
                var ctor = new MethodDefinition (".ctor", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, sysVoid);
                var b = new MethodBody (ctor);
                var localGlobals = new VariableDefinition (dataPointerType);
                b.Variables.Add(localGlobals);
                var il = b.GetILProcessor ();

                il.Emit (OpCodes.Ldarg_0);

                // Allocate the globals
                il.Emit (OpCodes.Sizeof, GetAllModulesDataType ());
                il.Emit (OpCodes.Call, sysAllocHGlobalInt);

                // Zero init
                il.Emit (OpCodes.Dup);
                il.Emit (OpCodes.Ldc_I4_0);
                il.Emit (OpCodes.Conv_U1);
                il.Emit (OpCodes.Sizeof, GetAllModulesDataType ());
                il.Emit (OpCodes.Initblk);

                // Register with the memory manager
                if (options.SafeMemory) {
                    il.Emit (OpCodes.Dup);
                    il.Emit (OpCodes.Sizeof, GetAllModulesDataType ());
                    il.Emit (OpCodes.Conv_I8);
                    il.Emit (OpCodes.Ldstr, "globals");
                    il.Emit (OpCodes.Call, GetSystemMethod ("@_register_memory"));
                }

                // Store them for later
                il.Emit (OpCodes.Call, sysPointerFromIntPtr);
                il.Emit (OpCodes.Stloc, localGlobals);
                il.Emit (OpCodes.Ldloc, localGlobals);
                il.Emit (OpCodes.Stfld, instanceDataField);

                // Init the modules
                foreach (var i in moduleInitMethods) {
                    il.Emit (OpCodes.Ldloc, localGlobals);
                    il.Emit (OpCodes.Call, i);
                }
                il.Emit(OpCodes.Ret);
                b.Optimize ();
                ctor.Body = b;
                instanceType.Methods.Add (ctor);
            }
        }

        TypeDefinition CreateLongjmpException ()
        {
            var td = new TypeDefinition ("", "LongjmpException", TypeAttributes.AnsiClass | TypeAttributes.Sealed, sysException);
            var compGen = new CustomAttribute (sysCompGenCtor);
            td.CustomAttributes.Add (compGen);

            var ctor = new MethodDefinition (".ctor", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, sysVoid);
            ctor.Parameters.Add (new ParameterDefinition ("code", ParameterAttributes.None, sysInt32));
            var b = new MethodBody (ctor);
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
            ctor.Body = b;
            td.Methods.Add (ctor);

            mod.Types.Add (td);
            return td;
        }

        TypeDefinition CreateDataType ()
        {
            var td = new TypeDefinition ("", pidTypeName, TypeAttributes.AnsiClass | TypeAttributes.Sealed, sysObj);
            var compGen = new CustomAttribute (sysCompGenCtor);
            td.CustomAttributes.Add (compGen);
            mod.Types.Add (td);
            return td;
        }

        public FieldDefinition AddDataField (byte[] data)
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

        void ImportAssemblies ()
        {
            var parameters = new ReaderParameters (ReadingMode.Immediate) {
                AssemblyResolver = resolver,
            };
            var a = typeof (StdLib.Memory).Assembly;
            ImportAssembly (a.Location, parameters);
        }

        void ImportAssembly (string path, ReaderParameters parameters)
        {
            var assembly = AssemblyDefinition.ReadAssembly (path, parameters);
            var module = assembly.MainModule;
            var itypes = new Dictionary<string, TypeDefinition> ();
            var importMethodBodies = new List<Action> ();

            foreach (var t in module.Types) {
                if (HasExportAttribute (t, needsArg: false)) {
                    var it = ImportType (t, x => mod.Types.Add (x));
                }
            }

            foreach (var b in importMethodBodies) {
                b ();
            }

            bool HasExportAttribute (ICustomAttributeProvider m, bool needsArg)
            {
                return FindExportAttribute (m, needsArg) != null;
            }

            CustomAttribute FindExportAttribute (ICustomAttributeProvider m, bool needsArg)
            {
                if (!m.HasCustomAttributes)
                    return null;
                var dllExportAttr = m.CustomAttributes.FirstOrDefault (x =>
                    x.Constructor.DeclaringType.Name == "DllExportAttribute"
                    && (!needsArg || x.HasConstructorArguments && x.ConstructorArguments.Count > 0));
                return dllExportAttr;
            }

            TypeDefinition ImportType (TypeDefinition type, Action<TypeDefinition> add)
            {
                var it = new TypeDefinition (type.Namespace, type.Name, type.Attributes);
                it.BaseType = ImportTypeRef (type.BaseType) ?? sysObj;
                itypes[it.FullName] = it;
                add (it);
                foreach (var nt in type.NestedTypes) {
                    if (HasExportAttribute (nt, needsArg: false)) {
                        ImportType (nt, x => it.NestedTypes.Add (x));
                    }
                }
                foreach (var f in type.Fields) {
                    var iff = new FieldDefinition (f.Name, f.Attributes, ImportTypeRef (f.FieldType));
                    it.Fields.Add (iff);
                }
                foreach (var m in type.Methods) {
                    var export = FindExportAttribute (m, needsArg: true);
                    if (export == null && !m.IsConstructor)
                        continue;

                    var symbol = export != null ? Symbol.Intern (export.ConstructorArguments[0].Value.ToString ()) : null;
                    
                    var (im, imImport) = ImportTypeMethod (m, it, symbol);
                    if (im == null)
                        continue;

                    importMethodBodies.Add (imImport);

                    if (export != null) {
                        externalMethodDefs[symbol] = new DefinedFunction ("Imported") {
                            Symbol = symbol,
                            IRModule = null,
                            IRDefinition = null,
                            ILDefinition = im,
                            ParamSyms = new SymbolTable<ParameterDefinition> (),
                        };
                        // Console.WriteLine($"FOUND EXPORT `{symbol}` ({symbol.GetType()})");
                    }
                    else if (im.IsConstructor) {
                        // OK
                    }
                }
                return it;
            }

            (MethodDefinition, Action) ImportTypeMethod (MethodDefinition methodDefinition, TypeDefinition importType, Symbol symbol)
            {
                var irt = ImportTypeRef (methodDefinition.ReturnType);
                var im = new MethodDefinition (methodDefinition.Name, methodDefinition.Attributes, irt);
                importType.Methods.Add (im);
                foreach (var p in methodDefinition.Parameters) {
                    var ip = new ParameterDefinition (p.Name, p.Attributes, ImportTypeRef (p.ParameterType));
                    im.Parameters.Add (ip);
                }
                Action importMethodBody = () => {
                    var ib = new MethodBody (im);
                    try {
                        ImportMethodBody (methodDefinition, im, ib, importType);
                        im.Body = ib;
                    }
                    catch (Exception ex) {
                        ErrorMessage ("", $"Failed to import function {symbol}: {ex.Message}", ex);
                        if (symbol != null) {
                            externalMethodDefs.Remove (symbol);
                        }
                        importType.Methods.Remove (im);
                    }
                };
                return (im, importMethodBody);
            }

            void ImportMethodBody (MethodDefinition methodDefinition, MethodDefinition im, MethodBody ib, TypeDefinition importType)
            {
                var b = methodDefinition.Body;
                ib.InitLocals = b.InitLocals;
                foreach (var v in b.Variables) {
                    var iv = new VariableDefinition (ImportTypeRef (v.VariableType));
                    ib.Variables.Add (iv);
                }

                var il = ib.GetILProcessor ();
                var importedInstructions = new Dictionary<Instruction, Instruction> ();
                foreach (var i in b.Instructions) {
                    il.Append (ImportInstruction (i));
                }

                Instruction ImportInstruction (Instruction i)
                {
                    if (importedInstructions.TryGetValue (i, out var existing))
                        return existing;
                    //var ii = il.Create (i.OpCode);
                    //importedInstructions[i] = ii;
                    Instruction ii;
                    if (i.Operand == null) {
                        ii = il.Create (i.OpCode);
                    }
                    else if (i.Operand is Instruction oi) {
                        ii = il.Create (i.OpCode, ImportInstruction (oi));
                    }
                    else if (i.Operand is ParameterDefinition op) {
                        ii = il.Create (i.OpCode, im.Parameters[op.Index]);
                    }
                    else if (i.Operand is VariableDefinition ov) {
                        ii = il.Create (i.OpCode, ib.Variables[ov.Index]);
                    }
                    else if (i.Operand is TypeReference ot) {
                        ii = il.Create (i.OpCode, ImportTypeRef (ot));
                    }
                    else if (i.Operand is MethodReference omr) {
                        ii = il.Create (i.OpCode, ImportMethodRef (omr));
                    }
                    else if (i.Operand is FieldReference ofr) {
                        ii = il.Create (i.OpCode, ImportFieldRef (ofr, methodDefinition.DeclaringType, importType));
                    }
                    else if (i.Operand is string os) {
                        ii = il.Create (i.OpCode, os);
                    }
                    else if (i.Operand is sbyte osbyte) {
                        ii = il.Create (i.OpCode, osbyte);
                    }
                    else if (i.Operand is byte obyte) {
                        ii = il.Create (i.OpCode, obyte);
                    }
                    else if (i.Operand is short oshort) {
                        ii = il.Create (i.OpCode, oshort);
                    }
                    else if (i.Operand is ushort oushort) {
                        ii = il.Create (i.OpCode, oushort);
                    }
                    else if (i.Operand is int oint) {
                        ii = il.Create (i.OpCode, oint);
                    }
                    else if (i.Operand is uint ouint) {
                        ii = il.Create (i.OpCode, ouint);
                    }
                    else if (i.Operand is long olong) {
                        ii = il.Create (i.OpCode, olong);
                    }
                    else if (i.Operand is ulong oulong) {
                        ii = il.Create (i.OpCode, oulong);
                    }
                    else if (i.Operand is float ofloat) {
                        ii = il.Create (i.OpCode, ofloat);
                    }
                    else if (i.Operand is double odouble) {
                        ii = il.Create (i.OpCode, odouble);
                    }
                    else {
                        throw new NotSupportedException ($"Instruction operand {i.Operand} ({i.Operand.GetType().Name}) not supported.");
                    }
                    importedInstructions[i] = ii;

                    if (i.OpCode == OpCodes.Ldc_I4_1 &&
                        i.Next.OpCode == OpCodes.Stsfld &&
                        i.Next.Operand is FieldReference sfr &&
                        sfr.Name == "Safe" &&
                        !options.SafeMemory) {
                        ii.OpCode = OpCodes.Ldc_I4_0;
                    }

                    return ii;
                }
            }

            FieldReference ImportFieldRef (FieldReference fr, TypeReference type, TypeDefinition importType)
            {
                if (fr.DeclaringType.FullName == type.FullName) {
                    var fd = importType.Fields.First (x => x.Name == fr.Name);
                    return fd;
                }
                else if (fr.DeclaringType.Namespace == type.Namespace) {
                    var fd = fr.Resolve ();
                    var fieldIndex = fd.DeclaringType.Fields.IndexOf (fd);
                    var it = ImportTypeDef (fr.DeclaringType.Namespace, fr.DeclaringType.Name);
                    var ifr = it.Fields.FirstOrDefault (x => x.Name == fr.Name);
                    if (0 > fieldIndex || fieldIndex >= it.Fields.Count)
                        throw new Exception ($"Couldn't find field {fr.Name} in {it.FullName}");
                    return it.Fields[fieldIndex];
                }
                else {
                    var tr = ImportTypeRef (fr.DeclaringType);
                    var td = tr.Resolve ();
                    var ifr = mod.ImportReference (td.Fields.First (x => x.Name == fr.Name));
                    return ifr;
                }
            }

            MethodReference ImportMethodRef (MethodReference mr)
            {
                //Console.WriteLine ("IMPORT " + mr);
                var t = ImportTypeRef (mr.DeclaringType);
                var irt = ImportTypeRef (mr.ReturnType);
                var iparams = mr.Parameters.Select (x => ImportTypeRef (x.ParameterType)).ToArray ();
                var imr = ImportMethod (t, irt, mr.Name, iparams);
                return imr;
            }

            TypeDefinition ImportTypeDef (string @namespace, string name)
            {
                var itd = mod.Types.FirstOrDefault (x => x.Name == name);
                if (itd == null)
                    throw new Exception ($"Couldn't find type {@namespace}.{name}");
                return itd;
            }

            TypeReference ImportTypeRef (TypeReference tr)
            {
                //Console.WriteLine ("ITR " + tr.FullName);
                if (tr == null)
                    return null;
                if (tr.IsPointer) {
                    return ImportTypeRef (tr.GetElementType ()).MakePointerType ();
                }
                else if (tr.IsArray) {
                    return ImportTypeRef (tr.GetElementType ()).MakeArrayType ();
                }
                else if (tr.IsGenericParameter) {
                    var et = ImportTypeRef (tr.DeclaringType).Resolve ();
                    var igp = new GenericParameter (tr.Name, et);
                    return igp;
                }
                else if (tr.IsNested) {
                    var td = ImportTypeRef (tr.DeclaringType).Resolve ();
                    var nt = td.NestedTypes.FirstOrDefault (x => x.Name == tr.Name);
                    if (nt == null) {
                        throw new Exception ($"Cannot find imported nested type {tr.Name} in {td}");
                    }
                    return nt;
                }
                else if (tr.IsByReference) {
                    return ImportTypeRef (tr.GetElementType ()).MakeByReferenceType ();
                }
                else if (tr.IsGenericInstance && tr is GenericInstanceType git) {
                    var ips = git.GenericArguments.Select (ImportTypeRef).ToArray ();
                    var itr = ImportTypeRef (git.ElementType);
                    var igit = itr.MakeGenericInstanceType (ips);
                    return igit;
                }
                else if (itypes.TryGetValue (tr.FullName, out var itt)) {
                    return itt;
                }
                else {
                    var name = tr.Name;
                    var itd = mod.Types.FirstOrDefault (x => x.Name == name);
                    if (itd != null)
                        return itd;

                    if (tr.Namespace.StartsWith ("System", StringComparison.Ordinal)) {
                        var itr = Import (tr.FullName);
                        return itr;
                    }
                    else {
                        throw new NotSupportedException ($"Cannot import type reference {tr}");
                    }
                }
            }
        }

        void EmitSyscalls ()
        {
            syscalls.Emit ();

            foreach (var iskv in syscalls.Calls) {
                var symbol = iskv.Key;
                if (externalMethodDefs.ContainsKey (symbol)) {
                    iskv.Value.DeclaringType.Methods.Remove (iskv.Value);
                    continue;
                }

                externalMethodDefs[symbol] = new DefinedFunction ("Syscall") {
                    Symbol = symbol,
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
                    // Create global data parameter
                    //
                    if (options.Reentrant) {
                        var globals = new ParameterDefinition ("globals", ParameterAttributes.None, GetGlobalDataPointerType ());
                        md.Parameters.Add (globals);
                    }

                    //
                    // Create parameters
                    //
                    var dbgVars = Array.Empty<object> ();                    
                    if (dbgMeth.TryGetValue (Symbol.Variables, out var o) && o is MetaSymbol) {
                        if (m.Metadata.TryGetValue ((Symbol)o, out o)) {
                            dbgVars = ((IEnumerable<object>)o).ToArray ();
                        }
                    }
                    else if (dbgMeth.TryGetValue (Symbol.RetainedNodes, out var ro) && ro is MetaSymbol) {
                        if (m.Metadata.TryGetValue ((Symbol)ro, out ro) && ro is IEnumerable<object> roenum) {
                            var q = from vref in roenum.OfType<MetaSymbol> ()
                                    where m.Metadata.ContainsKey (vref)
                                    let d = m.Metadata[vref] as SymbolTable<object>
                                    where d != null
                                    where d.ContainsKey (Symbol.Arg)
                                    let arg = int.Parse (d[Symbol.Arg].ToString ())
                                    orderby arg
                                    select vref;
                            dbgVars = q.ToArray ();
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
                        if (fp.ParameterType is VarArgsType) {
                            p.CustomAttributes.Add (new CustomAttribute (sysParamsAttrCtor));
                            p.Name = "arguments";
                        }
                        md.Parameters.Add (p);
                        paramSyms[fp.Symbol] = p;
                    }

                    declaringType.Methods.Add (md);
                    //Console.WriteLine ("EMIT " + md);

                    var def = new DefinedFunction ("Definition") {
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
                        // Create global data parameter
                        //
                        if (options.Reentrant) {
                            var globals = new ParameterDefinition ("globals", ParameterAttributes.None, GetGlobalDataPointerType ());
                            md.Parameters.Add (globals);
                        }

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

                        externalMethodDefs[sym] = new DefinedFunction ("Declaration") {
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
                        else if (node.Structure != null) {
                            td = mod.GetType (namesp + "." + node.Name);
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
                        ErrorMessage (m.IRModule?.SourceFilename, $"Failed to compile function `{IR.MangledName.Demangle (m.Symbol)}` ({m.Symbol}): {ex.Message}", ex);
                        CompileFailedFunction (m, ex);
                    }
                }
                else {
                    var paramTypes = string.Join (", ", m.ILDefinition.Parameters.Select (x => x.ParameterType.ToString()));
                    ErrorMessage (m.IRModule?.SourceFilename, $"Undefined function `{m.ILDefinition.ReturnType} {IR.MangledName.Demangle (m.Symbol)}({paramTypes})` ({m.Symbol}) from {m.Origin}");
                    CompileMissingFunction (m);
                }
            }
        }

        public int GetFunctionToken (DefinedFunction ff)
        {
            var key = ff.ILDefinition;
            if (!functionTokens.TryGetValue (key, out var token)) {
                // No token of 0
                token = functionTokens.Count + 1;
                functionTokens[key] = token;
            }
            return token;
        }

        void EmitLoadFunction ()
        {
            var md = LoadFunction;

            mod.GetType (namespac + ".Globals").Methods.Add (md);

            md.Parameters.Add (new ParameterDefinition ("token", ParameterAttributes.None, sysBytePtr));
            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Conv_I4));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Conv_I));

            var targets = new Mono.Cecil.Cil.Instruction[functionTokens.Count];
            var code = new List<Mono.Cecil.Cil.Instruction> (functionTokens.Count * 2);
            foreach (var kv in functionTokens.OrderBy (x => x.Value)) {
                var index = kv.Value - 1;
                var ldftn = il.Create (OpCodes.Ldftn, kv.Key);
                targets[index] = ldftn;
                code.Add (ldftn);
                code.Add (il.Create (OpCodes.Ret));
            }

            il.Append (il.Create (OpCodes.Switch, targets));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ret));

            foreach (var c in code) {
                il.Append (c);
            }

            body.Optimize ();
            md.Body = body;
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
                        // Console.WriteLine ($"REM {s.Key}, {s.Value}, {s.Value?.DeclaringType}");
                        s.Value.DeclaringType?.Methods.Remove (s.Value);
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

        readonly SymbolTable<bool> addedDebugInfoToStruct = new SymbolTable<bool> ();

        void AddDebugInfoToStruct (Symbol symbol, SymbolTable<object> debugInfo, Module module)
        {
            if (addedDebugInfoToStruct.ContainsKey (symbol))
                return;
            var td = globalStructs[symbol].Item2;
            if (debugInfo.TryGetValue (Symbol.BaseType, out var o) && o is MetaSymbol) {
                if (module.Metadata.TryGetValue ((Symbol)o, out o) && o is SymbolTable<object> typedefDbg) {
                    if (typedefDbg.TryGetValue (Symbol.Name, out o) && o is string) {
                        td.Name = o.ToString ();
                    }
                    var structDbg = typedefDbg;
                    while (structDbg.TryGetValue (Symbol.BaseType, out o)
                           && o is MetaSymbol bt
                           && module.Metadata.TryGetValue (bt, out o)
                           && o is SymbolTable<object> next) {
                        structDbg = next;
                    }
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
                                addedDebugInfoToStruct[symbol] = true;
                            }
                        }
                    }
                }
            }
        }

        TypeReference GetParameterType (LType irType, Module module, SymbolTable<object> debugInfo)
        {
            bool? unsigned = null;

            AddDebugInfoToStruct (irType, module, debugInfo);

            return GetClrType (irType, module: module, unsigned: unsigned);
        }

        public void AddDebugInfoToStruct (LType irType, Module module, SymbolTable<object> debugInfo)
        {
            if (debugInfo != null) {
                if (irType is Types.PointerType pt && pt.ElementType is NamedType nt && nt.Resolve (module) is LiteralStructureType st) {
                    AddDebugInfoToStruct (nt.Symbol, debugInfo, module);
                }
            }
        }

        public static int RoundUpIntBits (int bits)
        {
            if (bits > 32)
                return 64;
            if (bits > 16)
                return 32;
            if (bits > 8)
                return 16;
            return 8;
        }

        public TypeReference GetClrType (LType irType, Module module, bool? unsigned = null)
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
                    if (unsigned.HasValue) {
                        if (unsigned.Value) {
                            switch (RoundUpIntBits (intt.Bits)) {
                                case 1:
                                    return sysBoolean;
                                case 8:
                                    return sysByte;
                                case 16:
                                    return sysUInt16;
                                case 32:
                                    return sysUInt32;
                                case 64:
                                    return sysUInt64;
                                default:
                                    throw new NotSupportedException ($"{intt.Bits}-bit unsigned integers not supported");
                            }
                        }
                        else {
                            switch (RoundUpIntBits (intt.Bits)) {
                                case 1:
                                    return sysBoolean;
                                case 8:
                                    return sysSByte;
                                case 16:
                                    return sysInt16;
                                case 32:
                                    return sysInt32;
                                case 64:
                                    return sysInt64;
                                default:
                                    throw new NotSupportedException ($"{intt.Bits}-bit signed integers not supported");
                            }
                        }
                    }
                    else {
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
            if (st.IsPacked)
                tname = "Packed" + tname;

            var ns = namespac + ".AnonymousTypes";
            var (td, elementsClrTypes) = CreateStructType (ns, tname, st.IsPacked, resolvedElements, module);

            var r = new AnonymousStruct {
                ElementClrTypes = elementsClrTypes.ToArray (),
                ClrType = td,
                ElementFields = td.Fields.Select (x => (FieldReference)x).ToArray (),
            };

            mod.Types.Add (td);
            astructTypes[resolvedElements] = r;

            return r;
        }

        (TypeDefinition, List<TypeReference>) CreateStructType (string ns, string tname, bool isPacked, LType[] resolvedElements, Module module)
        {
            var tattrs = TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public;
            var td = new TypeDefinition (ns, tname, tattrs, sysVal);
            td.IsExplicitLayout = true;
            var n = resolvedElements.Length;
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
                    size = (int)resolvedElements[i].GetByteSize (module: module);
                    clrType = GetClrType (resolvedElements[i], module: module);
                }
                elementsClrTypes.Add (clrType);
                string pre = GetFieldPrefix (resolvedElements[i]);
                var f = new FieldDefinition (pre + i, FieldAttributes.Public, clrType);
                if (!isPacked)
                    offset = module.Align (offset, resolvedElements[i], (int)size);
                f.Offset = offset;
                td.Fields.Add (f);
                offset += size;
            }
            td.ClassSize = offset;
            td.PackingSize = isPacked ? (short)1 : (short)module.PointerByteSize;
            return (td, elementsClrTypes);
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
        ""tfm"": ""net6.0"",
        ""framework"": {
            ""name"": ""Microsoft.NETCore.App"",
            ""version"": ""6.0.0""
        }
    }
}
";
                var rtjsonPath = Path.ChangeExtension (path, ".runtimeconfig.json");
                File.WriteAllText (rtjsonPath, text);
            }
        }
    }

    public class CompilationOptions
    {
        public readonly Module[] Modules;
        public readonly string AssemblyName;
        public readonly bool SafeMemory;
        public readonly bool Reentrant;

        public CompilationOptions (IEnumerable<Module> modules, string assemblyName, bool safeMemory = false, bool reentrant = false)
        {
            Modules = modules.ToArray ();
            AssemblyName = assemblyName;
            SafeMemory = safeMemory;
            Reentrant = reentrant;
        }
    }
}
