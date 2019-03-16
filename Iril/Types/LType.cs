using System;

namespace Iril.Types
{
    public abstract class LType
    {
        public virtual LType Resolve (Module module) => this;

        public abstract long GetByteSize (Module module);
    }

    public class VarArgsType : LType
    {
        public static readonly VarArgsType VarArgs = new VarArgsType ();

        public override long GetByteSize (Module module) => 0L;

        public override string ToString () => "...";
    }
}
