using System;
using Repil.Types;

namespace Repil.IR
{
    public class Instruction
    {
        public Instruction ()
        {
        }
    }

    public class IcmpInstruction : Instruction
    {
        public readonly LocalSymbol Result;
        public readonly IcmpCondition Condition;
        public readonly LType Type;
        public readonly Value Op1;
        public readonly Value Op2;

        public IcmpInstruction (LocalSymbol result, IcmpCondition condition, LType type, Value op1, Value op2)
        {
            this.Result = result;
            this.Condition = condition;
            this.Type = type;
            this.Op1 = op1;
            this.Op2 = op2;
        }
    }

    public enum IcmpCondition
    {
        Equal,
        NotEqual,
        UnsignedGreaterThan,
        UnsignedGreaterThanOrEqual,
        UnsignedLessThan,
        UnsignedLessThanOrEqual,
        SignedGreaterThan,
        SignedGreaterThanOrEqual,
        SignedLessThan,
        SignedLessThanOrEqual,
    }
}
