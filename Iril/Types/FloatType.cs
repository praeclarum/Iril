using System;

namespace Iril.Types
{
    public class FloatType : LType
    {
        public static readonly FloatType Half = new FloatType ("half", 16);
        public static readonly FloatType Float = new FloatType ("float", 32);
        public static readonly FloatType Double = new FloatType ("double", 64);
        public static readonly FloatType FP128 = new FloatType ("fp128", 128);
        public static readonly FloatType X86_FP80 = new FloatType ("x86_fp80", 80);
        public static readonly FloatType PPC_FP128 = new FloatType ("ppc_fp128", 128);

        public readonly Symbol Symbol;
        public readonly int Bits;

        FloatType (string name, int bits)
        {
            Symbol = Symbol.Intern (name);
            Bits = bits;
        }

        public override string ToString () => Symbol.ToString ();

        public override long GetByteSize (Module module) => Bits == 32 ? 4L : 8L;

        public override int GetAlignment (Module module) => Bits / 8;

        public override bool StructurallyEquals (LType other) =>
            other is FloatType a
            && Bits == a.Bits;

        public override int GetStructuralHashCode () =>
            456
            + Bits.GetHashCode ();
    }
}
