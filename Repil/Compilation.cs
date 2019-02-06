using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Repil.Types;

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
        readonly SymbolTable<(StructureType, TypeDefinition)> structs =
            new SymbolTable<(StructureType, TypeDefinition)> ();

        AssemblyDefinition sysAsm;
        TypeReference sysObj;

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
        }

        void FindSystemTypes ()
        {
            sysAsm = AssemblyDefinition.ReadAssembly (SystemAssemblyPath);
            sysObj = mod.ImportReference (sysAsm.MainModule.GetType ("System.Object"));
        }

        void CompileStructures ()
        {
            foreach (var m in Modules) {
                foreach (var i in m.IdentifiedStructures) {
                    if (i.Value is LiteralStructureType l) {

                        var name = i.Key.Text.Substring (1).Split ('.').Last ();

                        var td = new TypeDefinition (namespac, name, TypeAttributes.SequentialLayout, sysObj);

                        mod.Types.Add (td);

                    }
                }
            }
        }

        public void WriteAssembly (Stream output)
        {
            asm.Write (output);
        }
    }
}
