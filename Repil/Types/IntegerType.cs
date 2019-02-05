using System;

namespace Repil.Types
{
    public class IntegerType : SingleValueType
    {
        public static readonly IntegerType I8 = new IntegerType (8);
        public static readonly IntegerType I16 = new IntegerType (16);
        public static readonly IntegerType I32 = new IntegerType (32);
        public static readonly IntegerType I64 = new IntegerType (64);

        public readonly Symbol Symbol;
        public readonly int Bits;

        public IntegerType (int bits)
        {
            Symbol = Symbol.Intern ("i" + bits);
            Bits = bits;
        }

        public override string ToString () => Symbol.ToString ();
    }
}
