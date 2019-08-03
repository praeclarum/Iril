using System;

namespace Iril.Types
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

        public override int GetAlignment (Module module) => ElementType.GetAlignment (module);

        public override string ToString () => $"[{Length} x {ElementType}]";

        public override bool StructurallyEquals (LType other) =>
            other is ArrayType a
            && Length == a.Length
            && ElementType.StructurallyEquals (a.ElementType);

        public override int GetStructuralHashCode () =>
            234
            + Length.GetHashCode ()
            + ElementType.GetStructuralHashCode ();
    }
}
