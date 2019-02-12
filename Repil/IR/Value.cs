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
    }
}
