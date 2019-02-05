using System;
using System.Numerics;
using System.Collections.Generic;
using Repil.Types;
using System.Linq;

namespace Repil.IR
{
    public abstract class Constant : Value
    {
    }

    public abstract class SimpleConstant : Constant
    {
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

    public class VectorConstant : ComplexConstant
    {
        public readonly TypedConstant[] Constants;

        public VectorConstant (IEnumerable<TypedConstant> constants)
        {
            if (constants == null) {
                throw new ArgumentNullException (nameof (constants));
            }

            Constants = constants.ToArray ();
        }
    }
}
