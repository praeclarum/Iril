using System;
namespace Repil.Types
{
    public class VoidType : LType
    {
        public static readonly VoidType Void = new VoidType ();

        VoidType ()
        {
        }

        public override string ToString () => "void";
    }
}
