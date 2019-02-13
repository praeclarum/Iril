using System;
using System.Numerics;
namespace Repil.Types
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
    }
}
