using System;
using System.Numerics;
using System.Collections.Generic;
using Iril.Types;
using System.Linq;
using System.Globalization;

namespace Iril.IR
{
    public abstract class Constant : Value
    {
        public abstract int Int32Value { get; }

        public override int GetInt32Value (Module module) => Int32Value;
    }

    public abstract class SimpleConstant : Constant
    {
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();

        public override bool IsIdempotent (FunctionDefinition function) => true;
    }

    public abstract class ComplexConstant : Constant
    {
    }

    public class TypedConstant
    {
        public readonly LType Type;
        public readonly Constant Constant;

        public TypedConstant (LType type, Constant constant)
        {
            Type = type;
            Constant = constant ?? throw new ArgumentNullException (nameof (constant));
        }

        public override string ToString () => $"{Type} {Constant}";
    }

    public class BooleanConstant : SimpleConstant
    {
        public static readonly BooleanConstant True = new BooleanConstant (true);
        public static readonly BooleanConstant False = new BooleanConstant (false);

        public readonly bool IsTrue;

        BooleanConstant (bool isTrue)
        {
            IsTrue = isTrue;
        }

        public override int Int32Value => IsTrue ? 1 : 0;

        public override string ToString () => IsTrue ? "true" : "false";
    }

    public class BytesConstant : Constant
    {
        public readonly Symbol Bytes;

        public BytesConstant (Symbol bytes)
        {
            Bytes = bytes ?? throw new ArgumentNullException (nameof (bytes));
        }

        public override int Int32Value => 0;

        public override string ToString () => $"{Bytes}";

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();
    }

    public class IntegerConstant : SimpleConstant
    {
        public static readonly IntegerConstant Zero = new IntegerConstant (BigInteger.Zero);
        public static readonly IntegerConstant One = new IntegerConstant (BigInteger.One);
        public static readonly IntegerConstant B1 = new IntegerConstant (BigInteger.One);
        public static readonly IntegerConstant B11       = new IntegerConstant (0b11);
        public static readonly IntegerConstant B111      = new IntegerConstant (0b111);
        public static readonly IntegerConstant B1111     = new IntegerConstant (0b1111);
        public static readonly IntegerConstant B11111    = new IntegerConstant (0b11111);
        public static readonly IntegerConstant B111111   = new IntegerConstant (0b111111);
        public static readonly IntegerConstant B1111111  = new IntegerConstant (0b1111111);
        public static readonly IntegerConstant B11111111 = new IntegerConstant (0b11111111);

        public readonly BigInteger Value;

        public IntegerConstant (BigInteger value)
        {
            Value = value;
        }

        public override int Int32Value => (int)Value;

        public override string ToString () => Value.ToString ();

        public static IntegerConstant MaskBits (int bits)
        {
            switch (bits) {
                case 0:
                    return Zero;
                case 1:
                    return One;
                case 2:
                    return B11;
                case 3:
                    return B111;
                case 4:
                    return B1111;
                case 5:
                    return B11111;
                case 6:
                    return B111111;
                case 7:
                    return B1111111;
                case 8:
                    return B11111111;
                default: { var m = (BigInteger.One << bits) - 1;
                    return new IntegerConstant (m);
                    }
            }
        }
    }

    public class HexIntegerConstant : SimpleConstant
    {
        public readonly BigInteger Value;

        public HexIntegerConstant (BigInteger value)
        {
            Value = value;
        }

        public override int Int32Value => (int)Value;

        public override string ToString () => $"0x{Value:X}";
    }

    public class FloatConstant : SimpleConstant
    {
        public readonly double Value;

        public FloatConstant (double value)
        {
            Value = value;
        }

        public override int Int32Value => (int)Math.Round (Value);

        public override string ToString () => Value.ToString (System.Globalization.CultureInfo.InvariantCulture);
    }

    public class NullConstant : SimpleConstant
    {
        public static readonly NullConstant Null = new NullConstant ();

        NullConstant ()
        {
        }

        public override int Int32Value => 0;

        public override string ToString () => "null";
    }

    public class StructureConstant : ComplexConstant
    {
        public readonly TypedValue[] Elements;

        public StructureConstant (IEnumerable<TypedValue> elements)
        {
            if (elements == null) {
                throw new ArgumentNullException (nameof (elements));
            }

            Elements = elements.ToArray ();
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Elements.SelectMany (x => x.Value.ReferencedLocals);
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Elements.SelectMany (x => x.Value.ReferencedGlobals);

        public override int Int32Value => 0;
    }



    public class UndefinedConstant : SimpleConstant
    {
        public static UndefinedConstant Undefined = new UndefinedConstant ();

        public override string ToString () => "undef";

        public override int Int32Value => 0;
    }

    public class ZeroConstant : SimpleConstant
    {
        public static ZeroConstant Zero = new ZeroConstant ();

        public override string ToString () => "zeroinitializer";

        public override int Int32Value => 0;
    }
}
