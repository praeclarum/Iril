using System;
using Repil.Types;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public abstract class Instruction
    {
        public abstract IEnumerable<LocalSymbol> ReferencedLocals { get; }
        public virtual bool IsIdempotent => false;
        public abstract LType ResultType (Module module);
    }

    public abstract class TerminatorInstruction : Instruction
    {
        public abstract IEnumerable<LocalSymbol> NextLabelSymbols { get; }

        public override LType ResultType (Module module) => VoidType.Void;
    }

    public class AshrInstruction : BinaryInstruction
    {
        public AshrInstruction (LType type, Value op1, Value op2, bool exact)
            : base (type, op1, op2)
        {
        }
    }

    public abstract class BinaryInstruction : Instruction
    {
        public readonly LType Type;
        public readonly Value Op1;
        public readonly Value Op2;

        protected BinaryInstruction (LType type, Value op1, Value op2)
        {
            Type = type;
            Op1 = op1;
            Op2 = op2;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Op1.ReferencedLocals.Concat (Op2.ReferencedLocals);
        public override LType ResultType (Module module) => Type;
        public override bool IsIdempotent => true;
    }

    public class AddInstruction : BinaryInstruction
    {
        public AddInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
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
        public override LType ResultType (Module module) => new PointerType (Type, 0);
    }

    public class AndInstruction : BinaryInstruction
    {
        public AndInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
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
        public override LType ResultType (Module module) => OutputType;
        public override bool IsIdempotent => true;
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

        public override IEnumerable<LocalSymbol> NextLabelSymbols => new[] { IfTrue.Symbol, IfFalse.Symbol };
    }

    public class UnconditionalBrInstruction : BrInstruction
    {
        public readonly LabelValue Destination;

        public UnconditionalBrInstruction (LabelValue destination)
        {
            Destination = destination;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();

        public override IEnumerable<LocalSymbol> NextLabelSymbols => new[] { Destination.Symbol };
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

        public override LType ResultType (Module module) => ReturnType;

        public override string ToString ()
        {
            return $"{ReturnType} {Pointer}({String.Join (", ", (object[])Arguments)})";
        }

        public override bool IsIdempotent {
            get {
                if (Pointer is GlobalValue g) {
                    switch (g.Symbol.Text) {
                        case "@llvm.dbg.declare":
                        case "@llvm.dbg.value":
                        case "@llvm.fabs.f64":
                        case "@llvm.sqrt.f64":
                            return true;
                    }
                }
                return false;
            }
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

        public override string ToString ()
        {
            return $"{Type} {Value}";
        }
    }

    public class DivInstruction : BinaryInstruction
    {
        public DivInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class ExtractElementInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly TypedValue Index;

        public ExtractElementInstruction (TypedValue value, TypedValue index)
        {
            Value = value ?? throw new ArgumentNullException (nameof (value));
            Index = index ?? throw new ArgumentNullException (nameof (index));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals =>
            Value.ReferencedLocals.Concat (Index.ReferencedLocals);

        public override LType ResultType (Module module) => ((VectorType)Value.Type).ElementType;

        public override bool IsIdempotent => true;
    }

    public class FaddInstruction : BinaryInstruction
    {
        public FaddInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class FcmpInstruction : Instruction
    {
        public readonly FcmpCondition Condition;
        public readonly LType Type;
        public readonly Value Op1;
        public readonly Value Op2;

        public FcmpInstruction (FcmpCondition condition, LType type, Value op1, Value op2)
        {
            Condition = condition;
            Type = type;
            Op1 = op1;
            Op2 = op2;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Op1.ReferencedLocals.Concat (Op2.ReferencedLocals);
        public override LType ResultType (Module module) => IntegerType.I1;
        public override bool IsIdempotent => true;
    }

    public enum FcmpCondition
    {
        False,
        True,
        Ordered,
        OrderedEqual,
        OrderedNotEqual,
        OrderedGreaterThan,
        OrderedGreaterThanOrEqual,
        OrderedLessThan,
        OrderedLessThanOrEqual,
        Unordered,
        UnorderedEqual,
        UnorderedNotEqual,
        UnorderedGreaterThan,
        UnorderedGreaterThanOrEqual,
        UnorderedLessThan,
        UnorderedLessThanOrEqual,
    }

    public class FdivInstruction : BinaryInstruction
    {
        public FdivInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class FenceInstruction : Instruction
    {
        public override IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();

        public override LType ResultType (Module module) => VoidType.Void;
    }

    public class FmulInstruction : BinaryInstruction
    {
        public FmulInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class FpextInstruction : ConversionInstruction
    {
        public FpextInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class FptosiInstruction : ConversionInstruction
    {
        public FptosiInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class FptouiInstruction : ConversionInstruction
    {
        public FptouiInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class FptruncInstruction : ConversionInstruction
    {
        public FptruncInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class FsubInstruction : BinaryInstruction
    {
        public FsubInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class GetElementPointerInstruction : Instruction
    {
        public readonly LType Type;
        public readonly TypedValue Pointer;
        public readonly TypedValue[] Indices;

        public GetElementPointerInstruction (LType type, TypedValue pointer, IEnumerable<TypedValue> indices)
        {
            Type = type;
            Pointer = pointer;
            Indices = indices.ToArray ();
        }

        public override string ToString () => $"&{Pointer}[{string.Join (", ", (object[])Indices)}]";
        public override IEnumerable<LocalSymbol> ReferencedLocals =>
            Pointer.ReferencedLocals.Concat (Indices.SelectMany (x => x.Value.ReferencedLocals));
        public override LType ResultType (Module module)
        {
            var t = Type.Resolve (module);
            foreach (var i in Indices.Skip (1)) {
                if (t is ArrayType art) {
                    t = art.ElementType.Resolve (module);
                }
                else if (t is LiteralStructureType s) {
                    if (i.Value is Constant c) {
                        var e = s.Elements[c.Int32Value];
                        t = e.Resolve (module);
                    }
                    else {
                        throw new Exception ($"Cannot get element {i.Value} at compile time");
                    }
                }
                else {
                    throw new Exception ("Cannot get element type of " + t);
                }
            }
            return new PointerType (t, 0);
        }
        public override bool IsIdempotent => true;
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
        public override LType ResultType (Module module) => IntegerType.I1;
        public override bool IsIdempotent => true;
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

    public class InsertElementInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly TypedValue Element;
        public readonly TypedValue Index;

        public InsertElementInstruction (TypedValue value, TypedValue element, TypedValue index)
        {
            Value = value ?? throw new ArgumentNullException (nameof (value));
            Element = element ?? throw new ArgumentNullException (nameof (element));
            Index = index ?? throw new ArgumentNullException (nameof (index));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals =>
            Value.ReferencedLocals.Concat (Element.ReferencedLocals).Concat (Index.ReferencedLocals);

        public override LType ResultType (Module module) => Value.Type;
    }

    public class InttoptrInstruction : ConversionInstruction
    {
        public InttoptrInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class LoadInstruction : Instruction
    {
        public readonly LType Type;
        public readonly TypedValue Pointer;
        public readonly bool IsVolatile;

        public LoadInstruction (LType type, TypedValue pointer, bool isVolatile)
        {
            Type = type;
            Pointer = pointer;
            IsVolatile = isVolatile;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Pointer.ReferencedLocals;
        public override LType ResultType (Module module) => Type;
    }

    public class LshrInstruction : BinaryInstruction
    {
        public LshrInstruction (LType type, Value op1, Value op2, bool exact)
            : base (type, op1, op2)
        {
        }
    }

    public class MultiplyInstruction : BinaryInstruction
    {
        public MultiplyInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class OrInstruction : BinaryInstruction
    {
        public OrInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
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

        public override LType ResultType (Module module) => Type;
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

    public class PtrtointInstruction : ConversionInstruction
    {
        public PtrtointInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class UdivInstruction : BinaryInstruction
    {
        public UdivInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class UremInstruction : BinaryInstruction
    {
        public UremInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
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

        public override IEnumerable<LocalSymbol> NextLabelSymbols => Enumerable.Empty<LocalSymbol> ();
    }

    public class SdivInstruction : BinaryInstruction
    {
        public SdivInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class SelectInstruction : Instruction
    {
        public readonly LType Type;
        public readonly Value Condition;
        public readonly TypedValue Value1;
        public readonly TypedValue Value2;

        public SelectInstruction (LType type, Value condition, TypedValue value1, TypedValue value2)
        {
            Type = type;
            Condition = condition;
            Value1 = value1;
            Value2 = value2;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Condition.ReferencedLocals.Concat (Value1.ReferencedLocals).Concat (Value2.ReferencedLocals);
        public override LType ResultType (Module module) => Value1.Type;
        public override bool IsIdempotent => true;
    }

    public class SextInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        public SextInstruction (TypedValue value, LType type)
        {
            Value = value;
            Type = type;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
        public override LType ResultType (Module module) => Type;
    }

    public class ShlInstruction : BinaryInstruction
    {
        public ShlInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class ShuffleVectorInstruction : Instruction
    {
        public readonly TypedValue Value1;
        public readonly TypedValue Value2;
        public readonly TypedValue Mask;

        public readonly VectorType Type;

        public ShuffleVectorInstruction (TypedValue value1, TypedValue value2, TypedValue mask)
        {
            Value1 = value1 ?? throw new ArgumentNullException (nameof (value1));
            Value2 = value2 ?? throw new ArgumentNullException (nameof (value2));
            Mask = mask ?? throw new ArgumentNullException (nameof (mask));

            Type = new VectorType (((VectorType)Mask.Type).Length, ((VectorType)Value1.Type).ElementType);
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals =>
            Value1.ReferencedLocals.Concat (Value2.ReferencedLocals);

        public override LType ResultType (Module module) => Type;

        public override bool IsIdempotent => true;
    }

    public class SitofpInstruction : ConversionInstruction
    {
        public SitofpInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class SremInstruction : BinaryInstruction
    {
        public SremInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class StoreInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly TypedValue Pointer;
        public readonly bool IsVolatile;

        public StoreInstruction (TypedValue value, TypedValue pointer, bool isVolatile)
        {
            Value = value;
            Pointer = pointer;
            IsVolatile = isVolatile;
        }

        public override string ToString () => $"{Pointer} <- {Value}";
        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals.Concat (Pointer.ReferencedLocals);
        public override LType ResultType (Module module) => VoidType.Void;
    }

    public class SubInstruction : BinaryInstruction
    {
        public SubInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
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
        public override LType ResultType (Module module) => VoidType.Void;
        public override IEnumerable<LocalSymbol> NextLabelSymbols =>
            Cases.Select (x => x.Label.Symbol).Concat (new[] { DefaultLabel.Symbol });
    }

    public class SwitchCase
    {
        public readonly TypedConstant Value;
        public readonly LabelValue Label;

        public SwitchCase (TypedConstant value, LabelValue label)
        {
            Value = value;
            Label = label;
        }
    }

    public class TruncInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        public TruncInstruction (TypedValue value, LType type)
        {
            Value = value;
            Type = type;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
        public override LType ResultType (Module module) => Type;
    }

    public abstract class ConversionInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        protected ConversionInstruction (TypedValue value, LType type)
        {
            Value = value;
            Type = type;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
        public override LType ResultType (Module module) => Type;
        public override bool IsIdempotent => true;
    }

    public class UitofpInstruction : ConversionInstruction
    {
        public UitofpInstruction (TypedValue input, LType outputType)
            : base (input, outputType)
        {
        }
    }

    public class XorInstruction : BinaryInstruction
    {
        public XorInstruction (LType type, Value op1, Value op2)
            : base (type, op1, op2)
        {
        }
    }

    public class ZextInstruction : Instruction
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        public ZextInstruction (TypedValue value, LType type)
        {
            Value = value;
            Type = type;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
        public override LType ResultType (Module module) => Type;
    }
}
