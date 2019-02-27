using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace Repil
{
    public class Symbol
    {
        public static readonly Symbol Name = Intern ("name");
        public static readonly Symbol Variables = Intern ("variables");
        public static readonly Symbol Type = Intern ("type");
        public static readonly Symbol Types = Intern ("types");
        public static readonly Symbol BaseType = Intern ("baseType");
        public static readonly Symbol Elements = Intern ("elements");
        public static readonly Symbol Line = Intern ("line");
        public static readonly Symbol Column = Intern ("column");
        public static readonly Symbol Scope = Intern ("scope");
        public static readonly Symbol File = Intern ("file");
        public static readonly Symbol Filename = Intern ("filename");
        public static readonly Symbol Directory = Intern ("directory");

        public readonly uint Hash;
        public readonly string Text;

        public override string ToString () => Text;

        protected Symbol (uint hash, string text)
        {
            Hash = hash;
            Text = text;
        }

        public static implicit operator Symbol (string text) => Intern (text);

        public override bool Equals (object obj) => ReferenceEquals (this, obj);

        public override int GetHashCode () { unchecked { return (int)Hash; } }

        bool Equals (string code, int index, int length)
        {
            if (length != Text.Length)
                return false;

            for (var i = 0; i < length; i++) {
                if (Text[i] != code[index + i])
                    return false;
            }

            return true;
        }

        public static bool operator == (Symbol x, Symbol y) => ReferenceEquals (x, y);
        public static bool operator != (Symbol x, Symbol y) => !ReferenceEquals (x, y);

        public static Symbol Intern (char prefix, int value)
        {
            return Intern (prefix + value.ToString ());
        }

        public static Symbol Intern (string symbol)
        {
            return SymbolTable.Get (symbol, 0, symbol.Length);
        }

        public static Symbol Intern (string code, int index, int length)
        {
            return SymbolTable.Get (code, index, length);
        }

        static class SymbolTable
        {
            class Entry
            {
                public Symbol Symbol;
                public Entry Next;
            }

            class Bucket
            {
                public Entry Head;
            }

            static Bucket[] buckets =
                Enumerable.Range (0, 0x1000).Select (x => new Bucket ()).ToArray ();

            public static Symbol Get (string code, int index, int length)
            {
                // http://www.cse.yorku.ca/~oz/hash.html
                uint hash = 5381;
                int end = index + length;
                for (var i = index; i < end; i++) {
                    var c = code[i];
                    hash = ((hash << 5) + hash) + c; /* hash * 33 + c */
                }

                var bucket = buckets[(hash & 0xFFF)];

                lock (bucket) {
                    var e = bucket.Head;
                    while (e != null) {
                        if (e.Symbol.Hash == hash && e.Symbol.Equals (code, index, length)) {
                            return e.Symbol;
                        }
                        e = e.Next;
                    }
                    var text = code.Substring (index, length);
                    var symbol =
                        text[0] == '@'
                        ? new GlobalSymbol (hash, text)
                        : (text[0] == '%'
                           ? new LocalSymbol (hash, text)
                           : (text[0] == '!'
                              ? new MetaSymbol (hash, text)
                              : new Symbol (hash, text)));
                    e = new Entry {
                        Next = bucket.Head,
                        Symbol = symbol,
                    };
                    bucket.Head = e;
                    return symbol;
                }
            }
        }
    }

    public class SymbolTable<T> : Dictionary<Symbol, T>
    {
    }

    public class GlobalSymbol : Symbol
    {
        public GlobalSymbol (uint hash, string text) : base (hash, text)
        {
        }
    }

    public class MetaSymbol : Symbol
    {
        public static readonly MetaSymbol Dbg = (MetaSymbol)Intern ("!dbg");
        public static readonly MetaSymbol None = (MetaSymbol)Intern ("!");

        public MetaSymbol (uint hash, string text) : base (hash, text)
        {
        }
    }

    public class LocalSymbol : Symbol
    {
        public static readonly LocalSymbol None = (LocalSymbol)Intern ("%");

        public LocalSymbol (uint hash, string text) : base (hash, text)
        {
        }

        public bool HasNumericValue => Text.Length > 1 && char.IsDigit (Text[1]);
        public int NumericValue => int.Parse (Text.Substring (1));
    }
}
