using System;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public class Module
    {
        public readonly ModulePart[] Parts;

        public Module (IEnumerable<ModulePart> parts)
        {
            Parts = parts.ToArray ();
        }

        public static Module Parse (string llvm)
        {
            var parser = new Parser ();
            var lex = new Lexer (llvm);
            return (Module)parser.yyparse (lex, null);
        }
    }

    public partial class Parser
    {
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
