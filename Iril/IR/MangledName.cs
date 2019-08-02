using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Iril.IR
{
    // https://itanium-cxx-abi.github.io/cxx-abi/abi.html#mangle.unscoped-name
    // https://demangler.com

    public class MangledName
    {
        public readonly Symbol Symbol;
        public readonly string[] Ancestry;
        public readonly string Identifier;
        public readonly string Demangled;

        readonly List<object> subs = new List<object> ();

        public MangledName (Symbol symbol, bool prefixWithTypeKind = false)
        {
            Symbol = symbol;
            var text = Symbol.Text;

            if (text.StartsWith ("@_Z", StringComparison.Ordinal)) {
                try {
                    var encoding = ParseEncoding (3);
                    Demangled = encoding.ToString ();
                    Identifier = SanitizeIdentifier (encoding.Identifier);
                    Ancestry = encoding.Ancestry.Select (SanitizeIdentifier).ToArray ();
                }
                catch (Exception ex) {
                    Debug.WriteLine (ex.ToString ());
                    var skip = text[0] == '@' || text[0] == '%';
                    Demangled = skip ? text.Substring (1) : text;
                    Identifier = SanitizeIdentifier (Demangled);
                    Ancestry = Array.Empty<string> ();
                }
                //Console.WriteLine (text);
                //Console.WriteLine ($"  DE: {encoding}");
                //Console.WriteLine ($"  AN: {string.Join (" >> ", Ancestry)}");
                //Console.WriteLine ($"  ID: {Identifier}");
            }
            else if (text.StartsWith ("%struct.", StringComparison.Ordinal) ||
                     text.StartsWith ("%class.", StringComparison.Ordinal) ||
                     text.StartsWith ("%union.", StringComparison.Ordinal) ||
                     text.StartsWith ("%\"struct.", StringComparison.Ordinal) ||
                     text.StartsWith ("%\"class.", StringComparison.Ordinal) ||
                     text.StartsWith ("%\"union.", StringComparison.Ordinal)) {
                var d = text.IndexOf ('.');
                var rrem = text[text.Length - 1] == '\"' ? 1 : 0;
                var itext = text.Substring (d + 1, text.Length - d - 1 - rrem);
                var cparts = itext.Split (new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(SanitizeIdentifier).ToArray ();
                Demangled = itext;
                Identifier = cparts[cparts.Length-1];
                if (prefixWithTypeKind) {
                    var ttext = text.Substring (1 + rrem, d - 1 - rrem);
                    Identifier = ttext + Identifier;
                }
                Ancestry = cparts.Take(cparts.Length - 1).ToArray ();
                //Console.WriteLine (text);
                //Console.WriteLine ($"  AN: {string.Join (" >> ", Ancestry)}");
                //Console.WriteLine ($"  ID: {Identifier}");
            }
            else {
                var skip = text[0] == '@' || text[0] == '%';
                Demangled = skip ? text.Substring (1) : text;
                Identifier = SanitizeIdentifier (Demangled);
                Ancestry = Array.Empty<string> ();
            }
        }

        public static string Demangle (Symbol symbol)
        {
            var n = new MangledName (symbol);
            return n.Demangled;
        }

        public static string SanitizeIdentifier (string text)
        {
            var b = new System.Text.StringBuilder ();
            foreach (var c in text) {
                if (char.IsDigit (c)) {
                    if (b.Length == 0)
                        b.Append ('_');
                    b.Append (c);
                }
                else if (char.IsLetter (c)) {
                    b.Append (c);
                }
                else {
                    b.Append ('_');
                }
            }
            return b.ToString ();
        }

        void SyntaxError (string message, int p)
        {
            throw new Exception (message + ": " + Symbol.Text.Substring (p));
        }

        Encoding ParseEncoding (int p)
        {
            var t = Symbol.Text;
            var ch = t[p];
            if (ch == 'T') {
                switch (t[p + 1]) {
                    case 'V':
                        p += 2;
                        return new VirtualTable (ParseType (ref p));
                    case 'T':
                        p += 2;
                        return new VttStructure (ParseType (ref p));
                    case 'I':
                        p += 2;
                        return new TypeInfoStructure (ParseType (ref p));
                    case 'S':
                        p += 2;
                        return new TypeInfoName (ParseType (ref p));
                }
            }
            return new NamedEncoding { Name = ParseName (ref p) };
        }

        TypeName ParseType (ref int p)
        {
            var t = Symbol.Text;

            ParseCVQualifiers (ref p);

            switch (t[p]) {
                case 'v':
                    p++;
                    return TypeName.Void;
                case 'w':
                    p++;
                    return TypeName.Wchar;
                case 'b':
                    p++;
                    return TypeName.Bool;
                case 'c':
                    p++;
                    return TypeName.Char;
                case 'a':
                    p++;
                    return TypeName.SChar;
                case 'h':
                    p++;
                    return TypeName.UChar;
                case 's':
                    p++;
                    return TypeName.Short;
                case 't':
                    p++;
                    return TypeName.UShort;
                case 'i':
                    p++;
                    return TypeName.Int;
                case 'j':
                    p++;
                    return TypeName.UInt;
                case 'l':
                    p++;
                    return TypeName.Long;
                case 'm':
                    p++;
                    return TypeName.ULong;
                case 'P': {
                        p++;
                        var et = ParseType (ref p);
                        return new PointerType { ElementType = et };
                    }
                case 'R': {
                        p++;
                        var et = ParseType (ref p);
                        return new LValueRefType { ElementType = et };
                    }
                case 'O': {
                        p++;
                        var et = ParseType (ref p);
                        return new RValueRefType { ElementType = et };
                    }
                case 'S' when p + 1 < t.Length && t[p + 1] != 't': {
                        p++;
                        return (TypeName)ParseSubstitution (ref p);
                    }
                default:                    
                    return new NamedType { Name = ParseName (ref p) };
            }

            throw new Exception ();
        }

        Name ParseName (ref int p)
        {
            var t = Symbol.Text;
            var ch = t[p];

            switch (ch) {
                case 'N':
                    p++;
                    return ParseNestedName (ref p);
                case 'S' when p + 1 < t.Length && t[p + 1] == 't':
                    p += 2;
                    return new NestedName { Names = { Name.Std, ParseUnqualifiedName (ref p) } };
                default:
                    return ParseUnqualifiedName (ref p);
            }
        }

        Name ParseNestedName (ref int p)
        {
            var q = ParseCVQualifiers (ref p);
            var t = Symbol.Text;
            var nn = new NestedName { Qualifiers = q };
            while (t[p] != 'E') {
                if (t[p] == 'I') {
                    p++;
                    nn.Names.Add (ParseTemplateArgs (ref p));
                }
                else if (t[p] == 'S' && p + 1 < t.Length && t[p + 1] != 't') {
                    p++;
                    nn.Names.Add ((Name)ParseSubstitution (ref p));
                }
                else if (t[p] == 'L') {
                    // IDK
                    p++;
                }
                else {
                    nn.Names.Add (ParseUnqualifiedName (ref p));
                    subs.Add (nn.Names.Last ());
                }
            }
            p++;
            return nn;
        }

        [Flags]
        enum CVQualifiers
        {
            Const = 1 << 1,
        }

        CVQualifiers ParseCVQualifiers (ref int p)
        {
            CVQualifiers q = 0;
            var t = Symbol.Text;
            if (t[p] == 'r') {
                p++;
            }
            if (t[p] == 'V') {
                p++;
            }
            if (t[p] == 'K') {
                p++;
                q |= CVQualifiers.Const;
            }
            return q;
        }

        Name ParseUnqualifiedName (ref int p)
        {
            var t = Symbol.Text;
            var ch = t[p];

            var op = OperatorName.TryGet (t, ref p);
            if (op != null) {
                return op;
            }

            if (ch == 'C' || ch == 'D') {
                var k = t.Substring (p, 2);
                var ident = k;
                switch (k) {
                    case "C1":
                        ident = "ctor";
                        break;
                    case "C2":
                        ident = "ctor_base";
                        break;
                    case "C3":
                        ident = "ctor_alloc";
                        break;
                    case "D0":
                        ident = "dtor_free";
                        break;
                    case "D1":
                        ident = "dtor";
                        break;
                    case "D2":
                        ident = "dtor_base";
                        break;
                }
                var n = new CtorName { Kind = k, Ident = ident };
                p += 2;
                return n;
            }

            Name prefix = null;
            if (ch == 'S' && p + 1 < t.Length && t[p + 1] == 't') {
                p += 2;
                prefix = Name.Std;
            }

            var e = p;
            while (char.IsDigit (t[e]) && e + 1 < t.Length)
                e++;
            if (e == p) {
                SyntaxError ("Bad Source Name", p);
                return null;
            }
            var length = int.Parse (t.Substring (p, e - p));
            var text = t.Substring (e, length);
            p = e + length;

            var un = new UnqualifiedName { Text = text };
            if (prefix == null)
                return un;

            return new NestedName { Names = { prefix, un } };
        }

        TemplateArgsName ParseTemplateArgs (ref int p)
        {
            var t = Symbol.Text;
            var r = new TemplateArgsName ();
            while (t[p] != 'E') {
                var type = ParseTemplateArg (ref p);
                r.Args.Add (type);
                subs.Add (type);
            }
            p++;
            return r;
        }

        TypeName ParseTemplateArg (ref int p)
        {
            var t = Symbol.Text;

            if (t[p] == 'J') {
                p++;
                var tr = new TemplateArgsType ();
                while (t[p] != 'E') {
                    var a = ParseTemplateArg (ref p);
                    tr.Args.Add (a);
                }
                p++;
                return tr;
            }

            var r = TryParseExpression (ref p);
            if (r != null)
                return r;

            return ParseType (ref p);
        }

        TypeName TryParseExpression (ref int p)
        {
            var t = Symbol.Text;
            switch (t[p]) {
                case 'L':
                    p++;
                    return ParsePrimaryExpression (ref p);
                default:
                    return null;
            }
        }

        TypeName ParsePrimaryExpression (ref int p)
        {
            var t = Symbol.Text;

            var typ = ParseType (ref p);

            if (char.IsDigit (t[p])) {
                var e = p;
                while (e < t.Length && char.IsDigit (t[e])) {
                    e++;
                }
                var value = int.Parse (t.Substring (p, e - p));
                typ = new ValueExprType { Type = typ, Value = value };
                p = e;
            }

            while (p < t.Length && t[p] != 'E') p++;
            p++;

            return typ;
        }

        object ParseSubstitution (ref int p)
        {
            var t = Symbol.Text;
            var e = p;
            while (t[e] != '_') {
                e++;
            }
            var index = -1;
            if (e > p) {
                var v = t.Substring (p, e - p);
                index = int.Parse (v);
            }
            index++;
            p = e + 1;
            if (index < subs.Count) {
                return subs[index];
            }
            return new UnqualifiedName { Text = "S" + index };
        }

        abstract class Name
        {
            public static readonly Name Std = new UnqualifiedName { Text = "std" };
            public abstract string Identifier { get; }
            public abstract string[] Ancestry { get; }
        }

        class CtorName : Name
        {
            public string Kind;
            public string Ident;
            public override string ToString () => Ident;
            public override string Identifier => Ident;
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class OperatorName : Name
        {
            public readonly string Name;
            public readonly string Ident;
            public readonly string Text;
            public override string ToString () => "operator " + Text;
            public override string Identifier => Ident;
            public override string[] Ancestry => Array.Empty<string> ();
            public OperatorName ()
            {
            }
            public OperatorName (string name, string ident, string text)
            {
                Name = name;
                Text = text;
                Ident = ident;
            }

            static readonly Dictionary<string, OperatorName> ops = new Dictionary<string, OperatorName> ();
            static OperatorName ()
            {
                ops["nw"] = new OperatorName ("nw", "new", "new");
                ops["na"] = new OperatorName ("na", "new_array", "new[]");
                ops["dl"] = new OperatorName ("dl", "delete", "delete");
                ops["da"] = new OperatorName ("da", "delete_array", "delete[]");
                ops["ps"] = new OperatorName ("ps", "ps", "+");
                ops["ng"] = new OperatorName ("ng", "ng", "-");
                ops["ad"] = new OperatorName ("ad", "ad", "&");
                ops["de"] = new OperatorName ("de", "de", "*");
                ops["co"] = new OperatorName ("co", "co", "~");
                ops["pl"] = new OperatorName ("pl", "pl", "+");
                ops["mi"] = new OperatorName ("mi", "mi", "-");
                ops["ml"] = new OperatorName ("ml", "ml", "*");
                ops["dv"] = new OperatorName ("dv", "dv", "/");
                ops["rm"] = new OperatorName ("rm", "rm", "%");
                ops["an"] = new OperatorName ("an", "an", "&");
                ops["or"] = new OperatorName ("or", "or", "|");
                ops["eo"] = new OperatorName ("eo", "eo", "^");
                ops["aS"] = new OperatorName ("aS", "aS", "=");
                ops["pL"] = new OperatorName ("pL", "pL", "+=");
                ops["mI"] = new OperatorName ("mI", "mI", "-=");
                ops["mL"] = new OperatorName ("mL", "mL", "*=");
                ops["dV"] = new OperatorName ("dV", "dV", "/=");
                ops["rM"] = new OperatorName ("rM", "rM", "%=");
                ops["aN"] = new OperatorName ("aN", "aN", "&=");
                ops["oR"] = new OperatorName ("oR", "oR", "|=");
                ops["eO"] = new OperatorName ("eO", "eO", "^=");
                ops["ls"] = new OperatorName ("ls", "ls", "<<");
                ops["rs"] = new OperatorName ("rs", "rs", ">>");
                ops["lS"] = new OperatorName ("lS", "lS", "<<=");
                ops["rS"] = new OperatorName ("rS", "rS", ">>=");
                ops["eq"] = new OperatorName ("eq", "eq", "==");
                ops["ne"] = new OperatorName ("ne", "ne", "!=");
                ops["lt"] = new OperatorName ("lt", "lt", "<");
                ops["gt"] = new OperatorName ("gt", "gt", ">");
                ops["le"] = new OperatorName ("le", "le", "<=");
                ops["ge"] = new OperatorName ("ge", "ge", ">=");
                ops["ss"] = new OperatorName ("ss", "ss", "<=>");
                ops["nt"] = new OperatorName ("nt", "nt", "!");
                ops["aa"] = new OperatorName ("aa", "aa", "&&");
                ops["oo"] = new OperatorName ("oo", "oo", "||");
                ops["pp"] = new OperatorName ("pp", "pp", "++");
                ops["mm"] = new OperatorName ("mm", "mm", "--");
                ops["cm"] = new OperatorName ("cm", "cm", ",");
                ops["pm"] = new OperatorName ("pm", "pm", "->*");
                ops["pt"] = new OperatorName ("pt", "pt", "->");
                ops["cl"] = new OperatorName ("cl", "cl", "()");
                ops["ix"] = new OperatorName ("ix", "ix", "[]");
                ops["qu"] = new OperatorName ("qu", "qu", "?");
            }
            public static OperatorName TryGet (string text, ref int index)
            {
                if (text.Length - index < 2)
                    return null;

                if ('a' <= text[index] && text[index] <= 'z') {
                    var t = text.Substring (index, 2);
                    if (ops.TryGetValue (t, out var n)) {
                        index += t.Length;
                        return n;
                    }
                }
                return null;
            }
        }

        class UnqualifiedName : Name
        {
            public string Text;
            public override string ToString () => Text;
            public override string Identifier => Text;
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class NestedName : Name
        {
            public CVQualifiers Qualifiers;
            public List<Name> Names = new List<Name> ();
            public override string ToString ()
            {
                var s = new StringBuilder ();
                foreach (var n in Names) {
                    if (s.Length == 0) {
                    }
                    else {
                        if (n is TemplateArgsName) {
                        }
                        else {
                            s.Append ("::");
                        }
                    }
                    s.Append (n);
                }
                return s.ToString ();
            }
            List<(Name Name, Name TArgs)> GetParts()
            {
                var flat = new List<Name> ();
                var r = new List<(Name Name, Name TArgs)> ();
                foreach (var n in Names)
                    Flatten (n);
                for (var i = 0; i < flat.Count; i++) {
                    var n = flat[i];
                    Name targs = null;
                    if (i + 1 < flat.Count && flat[i+1] is TemplateArgsName) {
                        targs = flat[i + 1];
                        i++;
                    }
                    r.Add ((n, targs));
                }
                return r;

                void Flatten (Name n)
                {
                    if (n is NestedName nn) {
                        foreach (var cn in nn.Names)
                            Flatten (cn);
                    }
                    else {
                        flat.Add (n);
                    }
                }
            }
            public override string Identifier {
                get {
                    var parts = GetParts ();
                    var x = parts.Last ();
                    var n = x.Name.Identifier;
                    if (x.TArgs != null)
                        n += "_" + x.TArgs.Identifier;
                    if (Qualifiers.HasFlag (CVQualifiers.Const))
                        n += "_const";
                    return n;
                }
            }
            public override string[] Ancestry {
                get {
                    var parts = GetParts ();
                    var r = parts.Take (parts.Count - 1).Select (x => {
                        var n = x.Name.Identifier;
                        if (x.TArgs != null)
                            n += "_" + x.TArgs.Identifier;
                        return n;
                    }).ToArray ();
                    return r;
                }
            }
        }

        class TemplateArgsName : Name
        {
            public List<TypeName> Args = new List<TypeName> ();
            public override string ToString () => "<" + string.Join (", ", Args) + ">";
            public override string Identifier => string.Join ("_", Args.Select (x => x.Identifier));
            public override string[] Ancestry => Array.Empty<string> ();
        }

        abstract class TypeName
        {
            public abstract string Identifier { get; }
            public abstract string[] Ancestry { get; }

            public static readonly TypeName Void = new BuiltinType { Name = "void" };
            public static readonly TypeName Wchar = new BuiltinType { Name = "wchar_t" };
            public static readonly TypeName Bool = new BuiltinType { Name = "bool" };
            public static readonly TypeName Char = new BuiltinType { Name = "char" };
            public static readonly TypeName SChar = new BuiltinType { Name = "signed char" };
            public static readonly TypeName UChar = new BuiltinType { Name = "unsigned char" };
            public static readonly TypeName Short = new BuiltinType { Name = "short" };
            public static readonly TypeName UShort = new BuiltinType { Name = "unsigned short" };
            public static readonly TypeName Int = new BuiltinType { Name = "int" };
            public static readonly TypeName UInt = new BuiltinType { Name = "unsigned int" };
            public static readonly TypeName Long = new BuiltinType { Name = "long" };
            public static readonly TypeName ULong = new BuiltinType { Name = "unsigned long" };
        }

        class ValueExprType : TypeName
        {
            public TypeName Type;
            public int Value;
            public override string ToString () => Value.ToString ();
            public override string Identifier => Value.ToString ();
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class BuiltinType : TypeName
        {
            public string Name;
            public override string ToString () => Name;
            public override string Identifier => Name;
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class NamedType : TypeName
        {
            public Name Name;
            public override string ToString () => Name.ToString ();
            public override string Identifier => Name.Identifier;
            public override string[] Ancestry => Name.Ancestry;
        }

        class PointerType : TypeName
        {
            public TypeName ElementType;
            public override string ToString () => $"{ElementType}*";
            public override string Identifier => $"{ElementType.Identifier}ptr";
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class LValueRefType : TypeName
        {
            public TypeName ElementType;
            public override string ToString () => $"{ElementType}&";
            public override string Identifier => $"{ElementType.Identifier}ref";
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class RValueRefType : TypeName
        {
            public TypeName ElementType;
            public override string ToString () => $"{ElementType}&&";
            public override string Identifier => $"{ElementType.Identifier}rref";
            public override string[] Ancestry => Array.Empty<string> ();
        }

        class TemplateArgsType : TypeName
        {
            public List<TypeName> Args = new List<TypeName> ();
            public override string ToString () => string.Join (", ", Args);
            public override string Identifier => string.Join ("_", Args.Select (x => x.Identifier));
            public override string[] Ancestry => Array.Empty<string> ();
        }

        abstract class Encoding
        {
            public abstract string Identifier { get; }
            public abstract string[] Ancestry { get; }
        }

        class NamedEncoding : Encoding
        {
            public Name Name;
            public override string ToString () => Name.ToString ();
            public override string Identifier => Name.Identifier;
            public override string[] Ancestry => Name.Ancestry;
        }

        abstract class SpecialName : Encoding
        {
            public readonly TypeName Type;

            public SpecialName (TypeName type)
            {
                Type = type ?? throw new ArgumentNullException (nameof (type));
            }
            public override string[] Ancestry => Type.Ancestry;
        }
        class VirtualTable : SpecialName
        {
            public VirtualTable (TypeName type) : base (type)
            {
            }
            public override string ToString () => $"@vtable for {Type}";
            public override string Identifier => $"{Type.Identifier}_vtable";
            
        }
        class VttStructure : SpecialName
        {
            public VttStructure (TypeName type) : base (type)
            {
            }
            public override string Identifier => $"{Type.Identifier}_vtt";
        }
        class TypeInfoStructure : SpecialName
        {
            public TypeInfoStructure (TypeName type) : base (type)
            {
            }
            public override string ToString () => $"@typeinfo for {Type}";
            public override string Identifier => $"{Type.Identifier}_typeinfo";
        }
        class TypeInfoName : SpecialName
        {
            public TypeInfoName (TypeName type) : base (type)
            {
            }
            public override string Identifier => $"{Type.Identifier}_typeinfoname";
        }
    }
}
