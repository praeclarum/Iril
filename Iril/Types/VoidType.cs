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

        public override long GetByteSize (Module module) => 0;
    }
}
