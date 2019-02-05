using System;
namespace Repil.IR
{
    public class FloatType : Type
    {
        public static FloatType Double = new FloatType (64);

        public readonly int Bits;

        public FloatType (int bits)
        {
            Bits = bits;
        }
    }
}
