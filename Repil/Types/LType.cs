using System;

namespace Repil.Types
{
    public abstract class LType
    {
        public virtual LType Resolve (Module module) => this;

        public abstract long GetByteSize (Module module);

        public virtual IR.Value Zero => IR.IntegerConstant.Zero;
    }

    public class VarArgsType : LType
    {
        public static readonly VarArgsType VarArgs = new VarArgsType ();

        public override long GetByteSize (Module module) => 0L;
    }
}
