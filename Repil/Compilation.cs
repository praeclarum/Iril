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
            for (var i = 0; i < f.Parameters.Length; i++) {
                var fp = f.Parameters[i];
                var pname = "p" + i;
                var pt = GetClrType (fp.Type);
                var p = new ParameterDefinition (pname, ParameterAttributes.None, pt);
                md.Parameters.Add (p);
            }

            var body = new MethodBody (md);
            var il = body.GetILProcessor ();

            //
            // Create phi locals
            //
            var phiLocals = new SymbolTable<VariableDefinition> ();
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (a.Result != null && a.Instruction is IR.PhiInstruction phi) {
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
            // Emit the instructions
            //
            foreach (var b in f.Blocks) {
                foreach (var a in b.Assignments) {
                    if (!ShouldInline (a.Result)) {
                        CompileInstruction (a);
                    }
                }
            }

            md.Body = body;
            body.Optimize ();

            return md;

            bool ShouldInline (LocalSymbol symbol)
            {
                return symbol != null && localCounts.ContainsKey (symbol) && localCounts[symbol] == 1;
            }

            VariableDefinition GetPhiLocal (IR.Assignment assignment)
            {
                return phiLocals[assignment.Result];
            }

            void CompileInstruction (IR.Assignment assignment)
            {
                switch (assignment.Instruction) {
                    case IR.PhiInstruction phi:
                        il.Append (il.Create (OpCodes.Ldloc, GetPhiLocal (assignment)));
                        break;
                    case IR.RetInstruction ret:
                        CompileValue (ret.Value);
                        il.Append (il.Create (OpCodes.Ret));
                        break;
                }
            }

            void CompileValue (IR.TypedValue value)
            {
                switch (value.Value) {
                    case IR.LocalValue local:
                        if (locals.TryGetValue (local.Symbol, out var vd)) {
                            il.Append (il.Create (OpCodes.Ldloc, vd));
                        }
                        else {
                            CompileInstruction (f.GetAssignment (local));
                        }
                        break;
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
