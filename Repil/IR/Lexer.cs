
namespace Repil.IR
{
    public class Lexer : Repil.IR.yyParser.yyInput
    {
        private string llvm;

        public Lexer(string llvm)
        {
            this.llvm = llvm;
        }

        public bool advance ()
        {
            throw new System.NotImplementedException ();
        }

        public int token ()
        {
            throw new System.NotImplementedException ();
        }

        public object value ()
        {
            throw new System.NotImplementedException ();
        }
    }
}
