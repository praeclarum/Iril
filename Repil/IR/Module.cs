using System;

namespace Repil.IR
{
    public class Module
    {
        public Module ()
        {
        }

        public static Module Parse (string llvm)
        {
            var parser = new Parser ();
            var lex = new Lexer (llvm);
            return (Module)parser.yyparse (lex, null);
        }
    }
}
