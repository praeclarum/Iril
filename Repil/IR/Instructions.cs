using System;
using Repil.Types;

namespace Repil.IR
{
    public abstract class Instruction
    {
    }

    public abstract class TerminalInstruction
    {
    }

    public abstract class AssignInstruction
    {
        public readonly LocalSymbol Result;

        protected AssignInstruction (LocalSymbol result)
        {
            Result = result;
        }
    }

    public class BitcastInstruction : AssignInstruction
    {
        public readonly LType InputType;
        public readonly Value Value;
        public readonly LType OutputType;

        public BitcastInstruction (LocalSymbol result, LType inputType, Value value, LType outputType)
            : base (result)
        {
            InputType = inputType;
            Value = value;
            OutputType = outputType;
        }
    }

    public abstract class BrInstruction : TerminalInstruction
    {
    }

    public class ConditionalBrInstruction : BrInstruction
    {
        public readonly Value Condition;
        public readonly LabelValue IfTrue;
        public readonly LabelValue IfFalse;

        public ConditionalBrInstruction (Value condition, LabelValue ifTrue, LabelValue ifFalse)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }
    }

    public class UnconditionalBrInstruction : BrInstruction
    {
        public readonly LabelValue Destination;

        public UnconditionalBrInstruction (LabelValue destination)
        {
            Destination = destination;
        }
    }

    public class IcmpInstruction : AssignInstruction
    {
        public readonly IcmpCondition Condition;
        public readonly LType Type;
        public readonly Value Op1;
        public readonly Value Op2;

        public IcmpInstruction (LocalSymbol result, IcmpCondition condition, LType type, Value op1, Value op2)
            : base (result)
        {
            Condition = condition;
            Type = type;
            Op1 = op1;
            Op2 = op2;
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
