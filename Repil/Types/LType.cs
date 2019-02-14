using System;

namespace Repil.Types
{
    public abstract class LType
    {
        public virtual LType Resolve (Module module) => this;

        public abstract long GetByteSize (Module module);
    }
}
