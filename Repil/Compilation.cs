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
        TypeReference sysObj;
        TypeReference sysVal;
        TypeReference sysByte;
        TypeReference sysInt16;
        TypeReference sysInt32;
        TypeReference sysInt64;
        TypeReference sysSingle;
        TypeReference sysDouble;

        public Compilation (IEnumerable<Module> documents, string assemblyName, string systemAssemblyPath)
        {
            Modules = documents.ToArray ();
            AssemblyName = assemblyName;
            SystemAssemblyPath = systemAssemblyPath;
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

        void FindSystemTypes ()
        {
            sysAsm = AssemblyDefinition.ReadAssembly (SystemAssemblyPath);
            sysObj = mod.ImportReference (sysAsm.MainModule.GetType ("System.Object"));
            sysVal = mod.ImportReference (sysAsm.MainModule.GetType ("System.ValueType"));
            sysByte = mod.ImportReference (sysAsm.MainModule.GetType ("System.Byte"));
            sysInt16 = mod.ImportReference (sysAsm.MainModule.GetType ("System.Int16"));
            sysInt32 = mod.ImportReference (sysAsm.MainModule.GetType ("System.Int32"));
            sysInt64 = mod.ImportReference (sysAsm.MainModule.GetType ("System.Int64"));
            sysSingle = mod.ImportReference (sysAsm.MainModule.GetType ("System.Single"));
            sysDouble = mod.ImportReference (sysAsm.MainModule.GetType ("System.Double"));
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
                    var f = iskv.Value;
                    var tname = iskv.Key.Text.Substring (1).Split ('.').Last ();

                    var md = new MethodDefinition (tname, MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static, GetClrType (f.ReturnType));

                    for (var i = 0; i < f.Parameters.Length; i++) {
                        var fp = f.Parameters[i];
                        var pname = "p" + i;
                        //var pt = GetClrType (fp.Type);
                        var pt = sysInt32.MakePointerType ();
                        var p = new ParameterDefinition (pname, ParameterAttributes.None, pt);
                        md.Parameters.Add (p);
                    }

                    var b = new MethodBody (md);
                    var il = b.GetILProcessor ();

                    if (f.ReturnType != VoidType.Void) {
                        il.Append (CecilInstruction.Create (OpCodes.Ldc_I4, 42));
                    }
                    il.Append (CecilInstruction.Create (OpCodes.Ret));
                    md.Body = b;

                    funcstd.Methods.Add (md);
                }
            }
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
                default:
                    throw new NotSupportedException ($"{e} not supported");
            }
        }

        public void WriteAssembly (Stream output)
        {
            asm.Write (output);
        }
    }
}
