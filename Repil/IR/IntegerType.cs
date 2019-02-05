using System;
namespace Repil.IR
{
    public class IntegerType : Type
    {
        public static IntegerType I8 = new IntegerType (8);
        public static IntegerType I16 = new IntegerType (16);
        public static IntegerType I32 = new IntegerType (32);
        public static IntegerType I64 = new IntegerType (64);

        public readonly int Bits;

        public IntegerType (int bits)
        {
            Bits = bits;
        }
    }
}
