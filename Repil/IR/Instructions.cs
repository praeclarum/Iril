using System;
using Repil.Types;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public abstract class Instruction
    {
    }

    public abstract class TerminalInstruction : Instruction
    {
    }

    public class BitcastInstruction : Instruction
    {
        public readonly TypedValue Input;
        public readonly LType OutputType;

        public BitcastInstruction (TypedValue input, LType outputType)
        {
            Input = input;
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

    public class CallInstruction : Instruction
    {
        public readonly LType ReturnType;
        public readonly Value Pointer;
        public readonly Argument[] Arguments;

        public CallInstruction (LType returnType, Value pointer, IEnumerable<Argument> arguments)
        {
            ReturnType = returnType;
            Pointer = pointer;
            Arguments = arguments.ToArray ();
        }
    }

    public class Argument
    {
        public readonly LType Type;
        public readonly Value Value;
        public readonly ParameterAttributes Attributes;

        public Argument (LType type, Value value, ParameterAttributes attributes)
        {
            Type = type;
            Value = value;
            Attributes = attributes;
        }
    }

    public class GetElementPointerInstruction : Instruction
    {
        public readonly LType Type;
        public readonly TypedValue Pointer;
        public readonly int[] Indices;

        public GetElementPointerInstruction (LType type, TypedValue pointer, IEnumerable<int> indices)
        {
            Type = type;
            Pointer = pointer;
            Indices = indices.ToArray ();
        }
    }

    public class IcmpInstruction : Instruction
    {
        public readonly IcmpCondition Condition;
        public readonly LType Type;
        public readonly Value Op1;
        public readonly Value Op2;

        public IcmpInstruction (IcmpCondition condition, LType type, Value op1, Value op2)
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

    public class StoreInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly TypedValue Pointer;

        public StoreInstruction (TypedValue value, TypedValue pointer)
        {
            Value = value;
            Pointer = pointer;
        }
    }
}
