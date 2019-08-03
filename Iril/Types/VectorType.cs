using System;
using System.Numerics;
namespace Iril.Types
{
    public class VectorType : AggregateType
    {
        public readonly int Length;
        public readonly LType ElementType;

        public VectorType (int length, LType elementType)
        {
            Length = length;
            ElementType = elementType;
        }

        public override string ToString ()
        {
            return $"<{Length} x {ElementType}>";
        }

        public override long GetByteSize (Module module) => Length * ElementType.GetByteSize (module);

        public override int GetAlignment (Module module) => (int)GetByteSize (module);

        public override bool StructurallyEquals (LType other) =>
            other is VectorType a
            && Length == a.Length
            && ElementType.StructurallyEquals (a.ElementType);

        public override int GetStructuralHashCode () =>
            890
            + Length.GetHashCode ()
            + ElementType.GetStructuralHashCode ();
    }
}
