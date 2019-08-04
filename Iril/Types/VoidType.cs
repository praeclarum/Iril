using System;
namespace Iril.Types
{
    public class VoidType : LType
    {
        public static readonly VoidType Void = new VoidType ();

        VoidType ()
        {
        }

        public override string ToString () => "void";

        public override long GetByteSize (Module module) => module.PointerByteSize;

        public override int GetAlignment (Module module) => module.PointerByteSize;

        public override bool StructurallyEquals (LType other) =>
            other is VoidType;

        public override int GetStructuralHashCode () =>
            901;
    }
}
