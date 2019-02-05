using System.Collections.Generic;
using System;

namespace Repil.IR
{
    public class Lexer : Repil.IR.yyParser.yyInput
    {
        readonly string code;
        int p;
        int tok;
        object val;

        static readonly Dictionary<Symbol, int> keywords = new Dictionary<Symbol, int> () {
            { Symbol.Intern ("source_filename"), Token.SOURCE_FILENAME }
        };

        public Lexer (string llvm)
        {
            this.code = llvm;
        }

        public bool advance ()
        {
            var s = code;
            var n = s.Length;

            while (p < n && (char.IsWhiteSpace (s[p]) || s[p] == ';')) {
                if (s[p] == ';') {
                    while (p < n && (s[p] != '\n'))
                        p++;
                }
                p++;
            }

            if (p >= n) {
                tok = -1;
                val = null;
                return false;
            }

            switch (s[p]) {
                case '=':
                    tok = '=';
                    val = null;
                    p++;
                    break;
                case '"': {
                        var ep = p + 1;
                        while (ep < n && (s[ep] != '\"'))
                            ep++;
                        tok = Token.STRING;
                        if (ep < n) {
                            val = s.Substring (p + 1, ep - p - 1);
                            p = ep + 1;
                        }
                        else {
                            val = s.Substring (p + 1);
                            p = n;
                        }
                    }
                    break;
                case var ch when char.IsLetter (ch) && char.IsLower (ch): {
                        var ep = p + 1;
                        while (ep < n && (char.IsLetter (s[ep]) || s[ep] == '_'))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        val = sym;
                        if (keywords.TryGetValue (sym, out var t)) {
                            tok = t;
                        }
                        else {
                            throw new NotSupportedException (sym.ToString ());
                        }
                        p = ep;
                    }
                    break;
                default:
                    throw new NotSupportedException (s[p].ToString ());
            }

            return true;
        }

        public int token () => tok;

        public object value () => val;
    }
}
