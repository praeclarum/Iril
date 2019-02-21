using System;
using System.Collections.Generic;
using System.Linq;

using Repil.IR;
using Repil.Types;

namespace Repil
{
    /// <summary>
    /// https://llvm.org/docs/LangRef.html
    /// clang -O3 -S -emit-llvm -fpic *.c
    /// </summary>
    public class Module
    {
        public Symbol Symbol { get; private set; }

        /// <summary>
        /// The original module identifier
        /// </summary>
        public string SourceFilename = "";

        /// <summary>
        /// How data is to be laid out in memory
        /// </summary>
        public string TargetDatalayout = "";

        public long PointerByteSize = 8;

        /// <summary>
        /// A series of identifiers delimited by the minus sign character
        /// </summary>
        public string TargetTriple = "";

        public SymbolTable<StructureType> IdentifiedStructures = new SymbolTable<StructureType> ();

        public SymbolTable<FunctionDefinition> FunctionDefinitions = new SymbolTable<FunctionDefinition> ();

        public SymbolTable<FunctionDeclaration> FunctionDeclarations = new SymbolTable<FunctionDeclaration> ();

        public SymbolTable<AttributeGroup> AttributeGroups = new SymbolTable<AttributeGroup> ();

        public SymbolTable<GlobalVariable> GlobalVariables = new SymbolTable<GlobalVariable> ();

        public SymbolTable<object> Metadata = new SymbolTable<object> ();

        public static Module Parse (string llvm, string filename = "")
        {
            var module = new Module ();
            module.Symbol = GetIdentifier (filename);
            var parser = new Parser (module);
            var lex = new Lexer (llvm);
            try {
                parser.yyparse (lex, null);
                if (!string.IsNullOrEmpty (module.SourceFilename))
                    module.Symbol = GetIdentifier (module.SourceFilename);
            }
            catch (Exception ex) {
                var m = $"{ex.Message}\n{lex.Surrounding}";
                throw new Exception (m, ex);
            }
            return module;
        }

        static Symbol GetIdentifier (string rawName)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension (rawName ?? "");
            var lastName = name.Split (new[] { '.', ' ', '\t', '\r', '\n', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault ();
            return lastName ?? "Module";
        }
    }
}

namespace Repil.IR
{
    public partial class Parser
    {
        Module module;

        public Parser (Module module)
        {
            this.module = module;
        }

        static List<T> NewList<T>(T firstItem)
        {
            return new List<T> (1) { firstItem };
        }
        static List<T> ListAdd<T> (object list, T item)
        {
            var l = (List<T>)list;
            l.Add (item);
            return l;
        }
        static SymbolTable<T> NewSyms<T> (object firstSymbol, T firstItem)
        {
            var s = new SymbolTable<T> ();
            s.Add ((Symbol)firstSymbol, firstItem);
            return s;
        }
        static SymbolTable<T> SymsAdd<T> (object syms, object symbol, T item)
        {
            var s = (SymbolTable<T>)syms;
            s[(Symbol)symbol] = item;
            return s;
        }
    }
}
