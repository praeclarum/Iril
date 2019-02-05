using System;
using System.Numerics;
namespace Repil.IR
{
    public abstract class Constant : Value
    {
    }

    public abstract class SimpleConstant : Constant
    {
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
    }

    public class IntegerConstant : SimpleConstant
    {
        public readonly BigInteger Value;

        public IntegerConstant (BigInteger value)
        {
            Value = value;
        }
    }

    public class FloatConstant : SimpleConstant
    {
        public readonly double Value;

        public FloatConstant (double value)
        {
            Value = value;
        }
    }

    public class NullConstant : SimpleConstant
    {
        public static readonly NullConstant Null = new NullConstant ();

        NullConstant ()
        {
        }
    }
}
