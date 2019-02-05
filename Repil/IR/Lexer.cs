using System.Collections.Generic;
using System;
using System.Numerics;

namespace Repil.IR
{
    public class Lexer : Repil.IR.yyParser.yyInput
    {
        readonly string code;
        int p;
        int lastP;
        int tok;
        object val = singleCharToken;

        class SingleCharToken
        {
        }
        static readonly SingleCharToken singleCharToken = new SingleCharToken ();

        static readonly Dictionary<Symbol, int> keywords = new Dictionary<Symbol, int> () {
            { Symbol.Intern ("source_filename"), Token.SOURCE_FILENAME },
            { Symbol.Intern ("target"), Token.TARGET },
            { Symbol.Intern ("datalayout"), Token.DATALAYOUT },
            { Symbol.Intern ("triple"), Token.TRIPLE },
            { Symbol.Intern ("type"), Token.TYPE },
            { Symbol.Intern ("half"), Token.HALF },
            { Symbol.Intern ("float"), Token.FLOAT },
            { Symbol.Intern ("double"), Token.DOUBLE },
            { Symbol.Intern ("x"), Token.X },
            { Symbol.Intern ("i1"), Token.I1 },
            { Symbol.Intern ("i8"), Token.I8 },
            { Symbol.Intern ("i16"), Token.I16 },
            { Symbol.Intern ("i32"), Token.I32 },
            { Symbol.Intern ("i64"), Token.I64 },
            { Symbol.Intern ("define"), Token.DEFINE },
            { Symbol.Intern ("unnamed_addr"), Token.UNNAMED_ADDR },
            { Symbol.Intern ("local_unnamed_addr"), Token.LOCAL_UNNAMED_ADDR },
            { Symbol.Intern ("icmp"), Token.ICMP },
            { Symbol.Intern ("eq"), Token.EQ },
            { Symbol.Intern ("ne"), Token.NE },
            { Symbol.Intern ("ugt"), Token.UGT },
            { Symbol.Intern ("uge"), Token.UGE },
            { Symbol.Intern ("ult"), Token.ULT },
            { Symbol.Intern ("ule"), Token.ULE },
            { Symbol.Intern ("sgt"), Token.SGT },
            { Symbol.Intern ("sge"), Token.SGE },
            { Symbol.Intern ("slt"), Token.SLT },
            { Symbol.Intern ("sle"), Token.SLE },
            { Symbol.Intern ("null"), Token.NULL },
            { Symbol.Intern ("br"), Token.BR },
            { Symbol.Intern ("label"), Token.LABEL },
            { Symbol.Intern ("bitcast"), Token.BITCAST },
            { Symbol.Intern ("to"), Token.TO },
            { Symbol.Intern ("store"), Token.STORE },
            { Symbol.Intern ("align"), Token.ALIGN },
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
                var max = lastP + 1;
                while (max < n && s[max] != '\n')
                    max++;
                var line = s.Substring (min + 1, max - min - 1);
                var arrow = new String ('~', lastP - min - 1) + "^";
                return line + "\n" + arrow;
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

            lastP = p;

            if (p >= n) {
                tok = -1;
                val = singleCharToken;
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
                case '<':
                case '>':
                    tok = s[p];
                    val = singleCharToken;
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
                case '!' when p + 1 < n && char.IsDigit (s[p + 1]):
                case '@' when p + 1 < n && char.IsDigit (s[p + 1]):
                case '%' when p + 1 < n && char.IsDigit (s[p + 1]):
                case '#' when p + 1 < n && char.IsDigit (s[p + 1]): {
                        var ep = p + 1;
                        while (ep < n && (char.IsDigit (s[ep])))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = s[p] == '@' ? Token.GLOBAL_SYMBOL :
                            (s[p] == '%' ? Token.LOCAL_SYMBOL :
                            (s[p] == '!' ? Token.META_SYMBOL : Token.ATTRIBUTE_GROUP_REF));
                        val = sym;
                        p = ep;
                    }
                    break;
                case '!' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '@' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '%' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '#' when p + 1 < n && IsNamedStart (s[p + 1]): {
                        var ep = p + 1;
                        while (ep < n && (char.IsLetterOrDigit (s[ep]) || s[ep] == '_' || s[ep] == '.' || s[ep] == '-'))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = s[p] == '@' ? Token.GLOBAL_SYMBOL :
                            (s[p] == '%' ? Token.LOCAL_SYMBOL :
                            (s[p] == '!' ? Token.META_SYMBOL : Token.ATTRIBUTE_GROUP_REF));
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
                        p = ep;
                        if (keywords.TryGetValue (sym, out var t)) {
                            tok = t;
                        }
                        else {
                            throw new InvalidOperationException ($"Unknown keyword '{sym}'");
                        }
                    }
                    break;
                case var ch when char.IsDigit (ch): {
                        var ep = p + 1;
                        var isfloat = false;
                        while (ep < n && (char.IsDigit (s[ep]) || s[ep] == '.' || s[ep] == 'e' || s[ep] == '-' || s[ep] == '+')) {
                            isfloat = isfloat || (s[ep] == '.' || s[ep] == 'e' || s[ep] == '-' || s[ep] == '+');
                            ep++;
                        }
                        var ss = s.Substring (p, ep - p);
                        p = ep;
                        if (isfloat) {
                            val = double.Parse (ss);
                            tok = Token.FLOAT_LITERAL;
                        }
                        else {
                            val = BigInteger.Parse (ss);
                            tok = Token.INTEGER;
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException ($"Unexpected '{s[p++]}'");
            }

            return true;
        }

        bool IsNamedStart (char c)
        {
            return char.IsLetterOrDigit (c) || c == '_' || c == '.' || c == '-';
        }

        public int token () => tok;

        public object value () => val;
    }
}
