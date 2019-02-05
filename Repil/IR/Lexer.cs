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
            { Symbol.Intern ("source_filename"), Token.SOURCE_FILENAME },
            { Symbol.Intern ("target"), Token.TARGET },
            { Symbol.Intern ("datalayout"), Token.DATALAYOUT },
            { Symbol.Intern ("triple"), Token.TRIPLE },
            { Symbol.Intern ("type"), Token.TYPE },
            { Symbol.Intern ("double"), Token.DOUBLE },
            { Symbol.Intern ("i8"), Token.I8 },
            { Symbol.Intern ("i16"), Token.I16 },
            { Symbol.Intern ("i32"), Token.I32 },
            { Symbol.Intern ("i64"), Token.I64 },
        };

        public Lexer (string llvm)
        {
            this.code = llvm;
        }

        public string Surrounding {
            get {
                var min = Math.Max (0, p - 10);
                var max = Math.Min (code.Length - 1, p + 10);
                return code.Substring (min, max - min);
            }
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
                case ',':
                case '+':
                case '-':
                case '*':
                case '/':
                case '{':
                case '}':
                case '(':
                case ')':
                    tok = s[p];
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
                case '@':
                case '%' when p + 1 < n && (char.IsLetter (s[p + 1]) || s[p + 1] == '_' || s[p + 1] == '.' || s[p + 1] == '-'): {
                        var ep = p + 1;
                        while (ep < n && (char.IsLetterOrDigit (s[ep]) || s[ep] == '_' || s[ep] == '.' || s[ep] == '-'))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = s[p] == '@' ? Token.GLOBAL_SYMBOL : Token.LOCAL_SYMBOL;
                        val = sym;
                        p = ep;
                    }
                    break;
                case var ch when char.IsLetter (ch) && char.IsLower (ch): {
                        var ep = p + 1;
                        while (ep < n && (char.IsLetterOrDigit (s[ep]) || s[ep] == '_'))
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
