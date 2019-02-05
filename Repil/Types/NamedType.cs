using System;

namespace Repil.Types
{
    public class NamedType : LType
    {
        public readonly Symbol Symbol;

        public NamedType (Symbol symbol)
        {
            Symbol = symbol;
        }
    }
}
