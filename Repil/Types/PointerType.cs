using System;

namespace Repil.Types
{
    public class PointerType : LType
    {
        public readonly LType ElementType;
        public readonly int AddressSpace;

        public static readonly PointerType PI8 = new PointerType (IntegerType.I8, 0);

        public PointerType (LType elementType, int addressSpace)
        {
            ElementType = elementType;
            AddressSpace = addressSpace;
        }

        public override string ToString () => $"{ElementType}*";
    }
}
