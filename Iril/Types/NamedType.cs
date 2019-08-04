using System;

namespace Iril.Types
{
    public class NamedType : LType
    {
        public readonly Symbol Symbol;

        public NamedType (Symbol symbol)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
        }

        public override string ToString () => Symbol.ToString ();

        public override LType Resolve (Module module)
        {
            if (module.IdentifiedStructures.TryGetValue (Symbol, out var s))
                return s;
            return this;
        }

        public override long GetByteSize (Module module) => Resolve (module).GetByteSize (module);

        public override int GetAlignment (Module module) => Resolve (module).GetAlignment (module);

        public override bool StructurallyEquals (LType other) =>
            other is NamedType a
            && Symbol == a.Symbol;

        public override int GetStructuralHashCode () =>
            678
            + Symbol.GetHashCode ();
    }
}
