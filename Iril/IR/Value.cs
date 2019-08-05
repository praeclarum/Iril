using System;
using System.Collections.Generic;
using System.Linq;
using Iril.Types;

namespace Iril.IR
{
    public abstract class Value
    {
        public virtual IEnumerable<LocalSymbol> ReferencedLocals => Enumerable.Empty<LocalSymbol> ();
        public abstract IEnumerable<GlobalSymbol> ReferencedGlobals { get; }

        public virtual int GetInt32Value (Module module) => 0;

        public virtual bool IsIdempotent (FunctionDefinition function) => false;
    }

    public class ArrayConstant : Value
    {
        public readonly TypedValue[] Elements;

        public ArrayConstant (IEnumerable<TypedValue> constants)
        {
            if (constants == null) {
                throw new ArgumentNullException (nameof (constants));
            }

            Elements = constants.ToArray ();
        }

        public override string ToString () => $"[{string.Join (", ", (object[])Elements)}]";

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Elements.SelectMany (x => x.Value.ReferencedGlobals);
    }

    public class BitcastValue : ConversionValue
    {
        public BitcastValue (TypedValue value, LType type)
            : base (value, type)
        {
        }
    }

    public abstract class ConversionValue : Value
    {
        public readonly TypedValue Value;
        public readonly LType Type;

        protected ConversionValue (TypedValue value, LType type)
        {
            Value = value;
            Type = type;
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => Value.ReferencedLocals;
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Value.ReferencedGlobals;

        public override bool IsIdempotent (FunctionDefinition function) => Value.Value.IsIdempotent (function);
    }

    public class LabelValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LabelValue (LocalSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override string ToString () => $"label {Symbol}";

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();
    }

    public class LocalValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LocalValue (LocalSymbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override IEnumerable<LocalSymbol> ReferencedLocals => new LocalSymbol[] { Symbol };
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();

        public override bool IsIdempotent (FunctionDefinition function)
        {
            return !function.Phis.ContainsKey (Symbol);
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
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();

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

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => new GlobalSymbol[] { Symbol };
    }

    public class PtrtointValue : ConversionValue
    {
        public PtrtointValue (TypedValue value, LType type) : base (value, type)
        {
        }
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

        public override IEnumerable<LocalSymbol> ReferencedLocals => Values.SelectMany (x => x.ReferencedLocals);
        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Values.SelectMany (x => x.ReferencedGlobals);
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
        public IEnumerable<GlobalSymbol> ReferencedGlobals => Value.ReferencedGlobals;

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
        public override IEnumerable<GlobalSymbol> ReferencedGlobals =>
            Pointer.ReferencedGlobals.Concat (Indices.SelectMany (x => x.Value.ReferencedGlobals));
    }

    public class IntToPointerValue : ConversionValue
    {
        public IntToPointerValue (TypedValue value, LType type)
            : base (value, type)
        {
        }
    }

    public class VectorConstant : Value
    {
        public readonly TypedValue[] Constants;

        public VectorConstant (IEnumerable<TypedValue> constants)
        {
            if (constants == null) {
                throw new ArgumentNullException (nameof (constants));
            }

            Constants = constants.ToArray ();
        }

        public override string ToString () => $"<{string.Join (", ", (object[])Constants)}>";

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Constants.SelectMany (x => x.Value.ReferencedGlobals);
    }

    public class VoidValue : Value
    {
        public static readonly VoidValue Void = new VoidValue ();

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();
    }

    public class InlineAssemblyValue : Value
    {
        public readonly string Assembly;
        public readonly string Constraints;

        public InlineAssemblyValue (string assembly, string constraints)
        {
            Assembly = assembly ?? throw new ArgumentNullException (nameof (assembly));
            Constraints = constraints ?? throw new ArgumentNullException (nameof (constraints));
        }

        public override IEnumerable<GlobalSymbol> ReferencedGlobals => Enumerable.Empty<GlobalSymbol> ();
    }
}
