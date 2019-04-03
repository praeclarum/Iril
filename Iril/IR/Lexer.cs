using System.Collections.Generic;
using System;
using System.Numerics;
using System.Globalization;

namespace Iril.IR
{
    public class Lexer : Iril.IR.yyParser.yyInput
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
        class MultiCharToken
        {
        }
        static readonly MultiCharToken multiCharToken = new MultiCharToken ();

        static readonly Dictionary<Symbol, int> keywords = new Dictionary<Symbol, int> () {
            { Symbol.Intern ("source_filename"), Token.SOURCE_FILENAME },
            { Symbol.Intern ("target"), Token.TARGET },
            { Symbol.Intern ("datalayout"), Token.DATALAYOUT },
            { Symbol.Intern ("triple"), Token.TRIPLE },
            { Symbol.Intern ("type"), Token.TYPE },
            { Symbol.Intern ("void"), Token.VOID },
            { Symbol.Intern ("half"), Token.HALF },
            { Symbol.Intern ("float"), Token.FLOAT },
            { Symbol.Intern ("double"), Token.DOUBLE },
            { Symbol.Intern ("true"), Token.TRUE },
            { Symbol.Intern ("false"), Token.FALSE },
            { Symbol.Intern ("x"), Token.X },
            { Symbol.Intern ("declare"), Token.DECLARE },
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
            { Symbol.Intern ("nonnull"), Token.NONNULL },
            { Symbol.Intern ("undef"), Token.UNDEF },
            { Symbol.Intern ("br"), Token.BR },
            { Symbol.Intern ("label"), Token.LABEL },
            { Symbol.Intern ("bitcast"), Token.BITCAST },
            { Symbol.Intern ("to"), Token.TO },
            { Symbol.Intern ("store"), Token.STORE },
            { Symbol.Intern ("align"), Token.ALIGN },
            { Symbol.Intern ("getelementptr"), Token.GETELEMENTPTR },
            { Symbol.Intern ("inbounds"), Token.INBOUNDS },
            { Symbol.Intern ("call"), Token.CALL },
            { Symbol.Intern ("phi"), Token.PHI },
            { Symbol.Intern ("ret"), Token.RET },
            { Symbol.Intern ("nocapture"), Token.NOCAPTURE },
            { Symbol.Intern ("writeonly"), Token.WRITEONLY },
            { Symbol.Intern ("readonly"), Token.READONLY },
            { Symbol.Intern ("attributes"), Token.ATTRIBUTES },
            { Symbol.Intern ("norecurse"), Token.NORECURSE },
            { Symbol.Intern ("nounwind"), Token.NOUNWIND },
            { Symbol.Intern ("readnone"), Token.READNONE },
            { Symbol.Intern ("speculatable"), Token.SPECULATABLE },
            { Symbol.Intern ("ssp"), Token.SSP },
            { Symbol.Intern ("uwtable"), Token.UWTABLE },
            { Symbol.Intern ("argmemonly"), Token.ARGMEMONLY },
            { Symbol.Intern ("alloca"), Token.ALLOCA },
            { Symbol.Intern ("load"), Token.LOAD },
            { Symbol.Intern ("tail"), Token.TAIL },
            { Symbol.Intern ("switch"), Token.SWITCH },
            { Symbol.Intern ("trunc"), Token.TRUNC },
            { Symbol.Intern ("add"), Token.ADD },
            { Symbol.Intern ("nsw"), Token.NSW },
            { Symbol.Intern ("nuw"), Token.NUW },
            { Symbol.Intern ("sext"), Token.SEXT },
            { Symbol.Intern ("zext"), Token.ZEXT },
            { Symbol.Intern ("mul"), Token.MUL },
            { Symbol.Intern ("fadd"), Token.FADD },
            { Symbol.Intern ("fmul"), Token.FMUL },
            { Symbol.Intern ("and"), Token.AND },
            { Symbol.Intern ("lshr"), Token.LSHR },
            { Symbol.Intern ("ashr"), Token.ASHR },
            { Symbol.Intern ("exact"), Token.EXACT },
            { Symbol.Intern ("sub"), Token.SUB },
            { Symbol.Intern ("select"), Token.SELECT },
            { Symbol.Intern ("or"), Token.OR },
            { Symbol.Intern ("insertelement"), Token.INSERTELEMENT },
            { Symbol.Intern ("shufflevector"), Token.SHUFFLEVECTOR },
            { Symbol.Intern ("extractelement"), Token.EXTRACTELEMENT },
            { Symbol.Intern ("shl"), Token.SHL },
            { Symbol.Intern ("sdiv"), Token.SDIV },
            { Symbol.Intern ("srem"), Token.SREM },
            { Symbol.Intern ("fdiv"), Token.FDIV },
            { Symbol.Intern ("fsub"), Token.FSUB },
            { Symbol.Intern ("sitofp"), Token.SITOFP },
            { Symbol.Intern ("uitofp"), Token.UITOFP },
            { Symbol.Intern ("xor"), Token.XOR },
            { Symbol.Intern ("fcmp"), Token.FCMP },
            { Symbol.Intern ("oeq"), Token.OEQ },
            { Symbol.Intern ("one"), Token.ONE },
            { Symbol.Intern ("ogt"), Token.OGT },
            { Symbol.Intern ("oge"), Token.OGE },
            { Symbol.Intern ("olt"), Token.OLT },
            { Symbol.Intern ("ole"), Token.OLE },
            { Symbol.Intern ("ord"), Token.ORD },
            { Symbol.Intern ("ueq"), Token.UEQ },
            { Symbol.Intern ("une"), Token.UNE },
            { Symbol.Intern ("uno"), Token.UNO },
            { Symbol.Intern ("fptosi"), Token.FPTOSI },
            { Symbol.Intern ("fptoui"), Token.FPTOUI },
            { Symbol.Intern ("fptrunc"), Token.FPTRUNC },
            { Symbol.Intern ("distinct"), Token.DISTINCT },
            { Symbol.Intern ("noalias"), Token.NOALIAS },
            { Symbol.Intern ("metadata"), Token.METADATA },
            { Symbol.Intern ("global"), Token.GLOBAL },
            { Symbol.Intern ("constant"), Token.CONSTANT },
            { Symbol.Intern ("zeroinitializer"), Token.ZEROINITIALIZER },
            { Symbol.Intern ("private"), Token.PRIVATE },
            { Symbol.Intern ("internal"), Token.INTERNAL },
            { Symbol.Intern ("external"), Token.EXTERNAL },
            { Symbol.Intern ("ptrtoint"), Token.PTRTOINT },
            { Symbol.Intern ("inttoptr"), Token.INTTOPTR },
            { Symbol.Intern ("urem"), Token.UREM },
            { Symbol.Intern ("udiv"), Token.UDIV },
            { Symbol.Intern ("fastcc"), Token.FASTCC },
            { Symbol.Intern ("opaque"), Token.OPAQUE },
            { Symbol.Intern ("x86_fp80"), Token.X86_FP80 },
            { Symbol.Intern ("fpext"), Token.FPEXT },
            { Symbol.Intern ("signext"), Token.SIGNEXT },
            { Symbol.Intern ("zeroext"), Token.ZEROEXT },
            { Symbol.Intern ("volatile"), Token.VOLATILE },
            { Symbol.Intern ("returned"), Token.RETURNED },
            { Symbol.Intern ("fence"), Token.FENCE },
            { Symbol.Intern ("seq_cst"), Token.SEQ_CST },
            { Symbol.Intern ("unreachable"), Token.UNREACHABLE },
            { Symbol.Intern ("dso_local"), Token.DSO_LOCAL },
            { Symbol.Intern ("dso_preemptable"), Token.DSO_PREEMPTABLE },
            { Symbol.Intern ("linkonce"), Token.LINKONCE },
            { Symbol.Intern ("linkonce_odr"), Token.LINKONCE_ODR },
            { Symbol.Intern ("weak"), Token.WEAK },
            { Symbol.Intern ("dereferenceable"), Token.DEREFERENCEABLE },
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
                while (min >= 0 && s[min] != '\n' && (lastP - min) < 122)
                    min--;
                var max = lastP + 1;
                while (max < n && s[max] != '\n' && (max - min) < 132)
                    max++;
                var line = s.Substring (min + 1, max - min - 1);
                var arrow = new String ('~', lastP - min - 1) + "^";
                return line + "\n" + arrow;
            }
        }

        char NextTokenChar ()
        {
            var s = code;
            var n = s.Length;
            var np = p;
            while (np < n && char.IsWhiteSpace (s[np])) {
                np++;
            }
            return (np < n) ? s[np] : (char)0;
        }

        public bool advance ()
        {
            var s = code;
            var n = s.Length;

            //Console.WriteLine (Surrounding);

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

                        if (tok == Token.META_SYMBOL) {
                            if (NextTokenChar () == '=')
                                tok = Token.META_SYMBOL_DEF;
                        }
                    }
                    break;
                case ',' when p + 3 < n && s[p + 2] == '!' && IsNamedStart (s[p + 3]):
                case '!' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '@' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '%' when p + 1 < n && IsNamedStart (s[p + 1]):
                case '#' when p + 1 < n && IsNamedStart (s[p + 1]): {
                        if (s[p] == ',')
                            p += 2;
                        var ep = p + 1;
                        while (ep < n && (char.IsLetterOrDigit (s[ep]) || s[ep] == '_' || s[ep] == '.' || s[ep] == '-'))
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = s[p] == '@' ? Token.GLOBAL_SYMBOL :
                            (s[p] == '%' ? Token.LOCAL_SYMBOL :
                            (s[p] == '!' ? Token.META_SYMBOL : Token.ATTRIBUTE_GROUP_REF));
                        val = sym;
                        p = ep;

                        if (tok == Token.META_SYMBOL) {
                            if (NextTokenChar () == '=')
                                tok = Token.META_SYMBOL_DEF;
                        }
                    }
                    break;
                case 'c' when p + 1 < n && s[p + 1] == '\"':
                case '!' when p + 1 < n && s[p + 1] == '\"':
                case '@' when p + 1 < n && s[p + 1] == '\"':
                case '%' when p + 1 < n && s[p + 1] == '\"':
                case '#' when p + 1 < n && s[p + 1] == '\"': {
                        var ep = p + 2;
                        while (ep < n && s[ep] != '\"')
                            ep++;
                        if (ep < n)
                            ep++;
                        var sym = Symbol.Intern (s, p, ep - p);
                        tok = s[p] == '@' ? Token.GLOBAL_SYMBOL :
                            (s[p] == '%' ? Token.LOCAL_SYMBOL :
                            (s[p] == '!' ? Token.META_SYMBOL :
                            (s[p] == '#' ? Token.ATTRIBUTE_GROUP_REF :
                            Token.CONSTANT_BYTES)));
                        val = sym;
                        p = ep;

                        if (tok == Token.META_SYMBOL) {
                            if (NextTokenChar () == '=')
                                tok = Token.META_SYMBOL_DEF;
                        }
                    }
                    break;
                case 'i' when p + 1 < n && char.IsDigit (s[p + 1]): {
                        var ep = p + 1;
                        while (ep < n && char.IsDigit (s[ep]))
                            ep++;
                        val = Types.IntegerType.Parse (s, p, ep - p);
                        p = ep;
                        tok = Token.INTEGER_TYPE;
                    }
                    break;
                case var ch when char.IsLetter (ch): {
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
                            tok = Token.SYMBOL;
                        }
                    }
                    break;
                case '0' when p + 2 < n && s[p + 1] == 'x': {
                        p += 2;
                        if (IsFloatHexStart (s[p]))
                            p++;

                        var ep = p;
                        while (ep < n && IsHex (s[ep])) {
                            ep++;
                        }
                        var ss = s.Substring (p, ep - p);
                        p = ep;
                        val = BigInteger.Parse (ss, NumberStyles.HexNumber);
                        tok = Token.HEX_INTEGER;
                    }
                    break;
                case var ch when char.IsDigit (ch) || ch == '-': {
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
                case '.' when p + 2 < n && s[p + 1] == '.' && s[p + 2] == '.':
                    tok = Token.ELLIPSIS;
                    val = multiCharToken;
                    p += 3;
                    break;
                case '=':
                case ',':
                case '+':
                case '*':
                case '/':
                case '{':
                case '}':
                case '(':
                case ')':
                case '<':
                case '>':
                case '[':
                case ']':
                case '!':
                case ':':
                case '|':
                    tok = s[p];
                    val = singleCharToken;
                    p++;
                    break;
                default:
                    throw new InvalidOperationException ($"Unexpected '{s[p++]}'");
            }

            return true;
        }

        bool IsFloatHexStart (char c)
        {
            switch (c) {
                case 'K':
                case 'M':
                case 'L':
                case 'H':
                    return true;
            }
            return false;
        }

        bool IsHex (char c)
        {
            switch (c) {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return true;
            }
            return false;
        }

        bool IsNamedStart (char c)
        {
            return char.IsLetterOrDigit (c) || c == '_' || c == '.' || c == '-';
        }

        public int token () => tok;

        public object value () => val;
    }
}
