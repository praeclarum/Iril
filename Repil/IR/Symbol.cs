using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace Repil.IR
{
    public class Symbol
    {
        public readonly uint Hash;
        public readonly string Text;

        public override string ToString () => Text;

        Symbol (uint hash, string text)
        {
            Hash = hash;
            Text = text;
        }

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
                    }
                    var symbol = new Symbol (hash, code.Substring (index, length));
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
}
