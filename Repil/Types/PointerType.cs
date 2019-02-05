using System;

namespace Repil.Types
{
    public class PointerType : LType
    {
        public readonly LType TargetType;
        public readonly int AddressSpace;

        public static readonly PointerType PI8 = new PointerType (IntegerType.I8, 0);

        public PointerType (LType target, int addressSpace)
        {
            TargetType = target;
            AddressSpace = addressSpace;
        }
    }
}
