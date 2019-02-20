using System;
using System.Collections.Generic;
using System.Linq;
using Repil.Types;

namespace Repil.IR
{
    public abstract class Value
    {
        public virtual IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();
    }

    public class LabelValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LabelValue (LocalSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override string ToString () => $"label {Symbol}";
    }

    public class LocalValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LocalValue (LocalSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals {
            get {
                yield return Symbol;
            }
        }

        public override string ToString () => Symbol.ToString ();
    }

    public class MetaValue : Value
    {
        public readonly MetaSymbol Symbol;

        public MetaValue (MetaSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();

        public override string ToString () => Symbol.ToString ();
    }

    public class GlobalValue : Value
    {
        public readonly GlobalSymbol Symbol;

        public GlobalValue (GlobalSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override string ToString () => Symbol.ToString ();
    }

    public class VectorValue : Value
    {
        public readonly TypedValue[] Values;

        public VectorValue (IEnumerable<TypedValue> values)
        {
            if (values == null) {
                throw new ArgumentNullException (nameof (values));
            }

            Values = values.ToArray ();
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals {
            get {
                foreach (var v in Values) {
                    foreach (var l in v.ReferencedLocals) {
                        yield return l;
                    }
                }
            }
        }
    }

    public class TypedValue
    {
        public readonly LType Type;
        public readonly Value Value;

        public TypedValue (LType type, Value value)
        {
            Type = type ?? throw new ArgumentNullException (nameof (type));
            Value = value ?? throw new ArgumentNullException (nameof (value));
        }

        public IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;

        public override string ToString () => $"{Type} {Value}";
    }

    public class GetElementPointerValue : Value
    {
        public readonly LType Type;
        public readonly TypedValue Pointer;
        public readonly TypedValue[] Indices;

        public GetElementPointerValue (LType type, TypedValue pointer, IEnumerable<TypedValue> indices)
        {
            Type = type;
            Pointer = pointer;
            Indices = indices.ToArray ();
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals =>
            Pointer.ReferencedLocals.Concat (Indices.SelectMany (x => x.Value.ReferencedLocals));
    }

    public class IntToPointerValue : Value
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        public IntToPointerValue (TypedValue value, LType type)
        {
            Value = value ?? throw new ArgumentNullException (nameof (value));
            Type = type ?? throw new ArgumentNullException (nameof (type));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
    }

    public class VoidValue : Value
    {
        public static readonly VoidValue Void = new VoidValue ();
    }
}
