using System;

namespace Iril.Types
{
    public abstract class LType
    {
        public virtual LType Resolve (Module module) => this;

        public abstract long GetByteSize (Module module);

        public abstract bool StructurallyEquals (LType other);

        public abstract int GetStructuralHashCode ();

        public abstract int GetAlignment(Module module);
    }

    public class VarArgsType : LType
    {
        public static readonly VarArgsType VarArgs = new VarArgsType ();

        public override long GetByteSize (Module module) => 0L;

        public override string ToString () => "...";

        public override bool StructurallyEquals (LType other) => other is VarArgsType;

        public override int GetStructuralHashCode () => 123;

        public override int GetAlignment (Module module) => module.PointerByteSize;
    }
}
