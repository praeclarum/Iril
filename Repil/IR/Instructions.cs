using System;
using Repil.Types;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public abstract class Instruction
    {
        public abstract IEnumerable<LocalSymbol> ReferencedLocals { get; }
        public abstract LType ResultType { get; }
    }

    public abstract class TerminatorInstruction : Instruction
    {
        public override LType ResultType => VoidType.Void;
    }

    public class AllocaInstruction : Instruction
    {
        public readonly LType Type;
        public readonly int Align;

        public AllocaInstruction (LType type, int align)
        {
            Type = type;
            Align = align;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();
        public override LType ResultType => Type;
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Input.ReferencedLocals;
        public override LType ResultType => OutputType;
    }

    public abstract class BrInstruction : TerminatorInstruction
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Condition.ReferencedLocals;
    }

    public class UnconditionalBrInstruction : BrInstruction
    {
        public readonly LabelValue Destination;

        public UnconditionalBrInstruction (LabelValue destination)
        {
            Destination = destination;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();
    }

    public class CallInstruction : Instruction
    {
        public readonly LType ReturnType;
        public readonly Value Pointer;
        public readonly Argument[] Arguments;
        public readonly bool Tail;

        public CallInstruction (LType returnType, Value pointer, IEnumerable<Argument> arguments, bool tail)
        {
            ReturnType = returnType;
            Pointer = pointer;
            Arguments = arguments.ToArray ();
            Tail = tail;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals {
            get {
                foreach (var a in Arguments) {
                    foreach (var l in a.Value.ReferencedLocals) {
                        yield return l;
                    }
                }
            }
        }

        public override LType ResultType => ReturnType;
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Pointer.ReferencedLocals;
        public override LType ResultType => Type;
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Op1.ReferencedLocals.Concat (Op2.ReferencedLocals);
        public override LType ResultType => IntegerType.I1;
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

    public class LoadInstruction : Instruction
    {
        public readonly LType Type;
        public readonly TypedValue Pointer;

        public LoadInstruction (LType type, TypedValue pointer)
        {
            Type = type;
            Pointer = pointer;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Pointer.ReferencedLocals;
        public override LType ResultType => Type;
    }

    public class PhiInstruction : Instruction
    {
        public readonly LType Type;
        public readonly PhiValue[] Values;

        public PhiInstruction (LType type, IEnumerable<PhiValue> values)
        {
            Type = type;
            Values = values.ToArray ();
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals {
            get {
                foreach (var v in Values) {
                    foreach (var l in v.Value.ReferencedLocals) {
                        yield return l;
                    }
                }
            }
        }

        public override LType ResultType => Type;
    }

    public class PhiValue
    {
        public readonly Value Value;
        public readonly Value Label;

        public PhiValue (Value value, Value label)
        {
            Value = value;
            Label = label;
        }
    }

    public class RetInstruction : TerminatorInstruction
    {
        public readonly TypedValue Value;

        public RetInstruction (TypedValue value)
        {
            Value = value;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals.Concat (Pointer.ReferencedLocals);
        public override LType ResultType => VoidType.Void;
    }

    public class SwitchInstruction : TerminatorInstruction
    {
        public readonly TypedValue Value;
        public readonly LabelValue DefaultLabel;
        public readonly SwitchCase[] Cases;

        public SwitchInstruction (TypedValue value, LabelValue pointer, IEnumerable<SwitchCase> cases)
        {
            Value = value;
            DefaultLabel = pointer;
            Cases = cases.ToArray ();
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals.Concat (DefaultLabel.ReferencedLocals);
        public override LType ResultType => VoidType.Void;
    }

    public class SwitchCase
    {
        public readonly TypedValue Value;
        public readonly LabelValue Label;

        public SwitchCase (TypedValue value, LabelValue label)
        {
            Value = value;
            Label = label;
        }
    }
}
