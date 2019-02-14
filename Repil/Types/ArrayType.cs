using System;

namespace Repil.Types
{
    public class ArrayType : AggregateType
    {
        public readonly long Length;
        public readonly LType ElementType;

        public ArrayType (long length, LType elementType)
        {
            Length = length;
            ElementType = elementType;
        }

        public override long GetByteSize (Module module) => Length * ElementType.GetByteSize (module);
    }
}
