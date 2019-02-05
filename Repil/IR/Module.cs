using System;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public class Module
    {
        /// <summary>
        /// The original module identifier
        /// </summary>
        public string SourceFilename = "";

        /// <summary>
        /// How data is to be laid out in memory
        /// </summary>
        public string TargetDatalayout = "";

        /// <summary>
        /// A series of identifiers delimited by the minus sign character
        /// </summary>
        public string TargetTriple = "";

        public static Module Parse (string llvm)
        {
            var module = new Module ();
            var parser = new Parser (module);
            var lex = new Lexer (llvm);
            parser.yyparse (lex, null);
            return module;
        }
    }

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
    }
}
