using System;

namespace Iril.Types
{
    public class IntegerType : SingleValueType
    {
        public static readonly IntegerType I1 = new IntegerType (1);
        public static readonly IntegerType I8 = new IntegerType (8);
        public static readonly IntegerType I16 = new IntegerType (16);
        public static readonly IntegerType I31 = new IntegerType (31);
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

        public override long GetByteSize (Module module) => Bits < 8 ? 1 : Bits / 8;

        public override int GetAlignment (Module module) => Bits < 8 ? 1 : Bits / 8;

        public static IntegerType Parse (string text, int startIndex, int length)
        {
            var i = startIndex + 1;
            var e = startIndex + length;

            switch (length) {
                case 2:
                    switch (text[i]) {
                        case '1':
                            return I1;
                        case '8':
                            return I8;
                    }
                    break;
                case 3:
                    switch (text[i]) {
                        case '1' when text[i + 1] == '6':
                            return I16;
                        case '3' when text[i + 1] == '1':
                            return I31;
                        case '3' when text[i + 1] == '2':
                            return I32;
                        case '6' when text[i + 1] == '4':
                            return I64;
                    }
                    break;
            }
            var bitText = text.Substring (i, e - i);
            var bits = int.Parse (bitText);
            return new IntegerType (bits);
        }

        public override bool StructurallyEquals (LType other) =>
            other is IntegerType a
            && Bits == a.Bits;

        public override int GetStructuralHashCode () =>
            345
            + Bits.GetHashCode ();
    }
}
