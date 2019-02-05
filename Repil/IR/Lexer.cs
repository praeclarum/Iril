using System.Collections.Generic;
using System;

namespace Repil.IR
{
    public class Lexer : Repil.IR.yyParser.yyInput
    {
        readonly string code;
        int p;
        int lastP;
        int tok;
        object val;

        static readonly Dictionary<Symbol, int> keywords = new Dictionary<Symbol, int> () {
            { Symbol.Intern ("source_filename"), Token.SOURCE_FILENAME },
            { Symbol.Intern ("target"), Token.TARGET },
            { Symbol.Intern ("datalayout"), Token.DATALAYOUT },
            { Symbol.Intern ("triple"), Token.TRIPLE },
            { Symbol.Intern ("type"), Token.TYPE },
            { Symbol.Intern ("half"), Token.HALF },
            { Symbol.Intern ("float"), Token.FLOAT },
            { Symbol.Intern ("double"), Token.DOUBLE },
            { Symbol.Intern ("i8"), Token.I8 },
            { Symbol.Intern ("i16"), Token.I16 },
            { Symbol.Intern ("i32"), Token.I32 },
            { Symbol.Intern ("i64"), Token.I64 },
            { Symbol.Intern ("define"), Token.DEFINE },
            { Symbol.Intern ("unnamed_addr"), Token.UNNAMED_ADDR },
            { Symbol.Intern ("local_unnamed_addr"), Token.LOCAL_UNNAMED_ADDR },
        };

        public Lexer (string llvm)
        {
            this.code = llvm;
        }

        public string Surrounding {
            get {
                var s = code;
                var n = s.Length;
                var min = lastP;
                while (min >= 0 && s[min] != '\n')
                    min--;
                var max = lastP;
                while (max < n && s[max] != '\n')
                    max++;
                var line = s.Substring (min + 1, max - min - 1);
                var arrow = new String ('~', lastP - min) + "^";
                return line + "\n" + arrow;
            }
        }

        public bool advance ()
        {
            var s = code;
            var n = s.Length;

            lastP = p;

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
                case '#' when p + 1 < n && (char.IsDigit (s[p + 1])): {
                        var ep = p + 1;
                        while (ep < n && (char.IsDigit (s[ep])))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = Token.ATTRIBUTE_GROUP_REF;
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
