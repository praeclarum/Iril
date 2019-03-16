using System;
using Iril.Types;

namespace Iril.IR
{
    public class GlobalVariable
    {
        public readonly GlobalSymbol Symbol;
        public readonly LType Type;
        public readonly Value Initializer;
        public readonly bool IsPrivate;
        public readonly bool IsExternal;
        public readonly bool IsConstant;

        public GlobalVariable (GlobalSymbol symbol, LType type, Value initializer, bool isPrivate, bool isExternal, bool isConstant)
        {
            Symbol = symbol;
            Type = type ?? throw new ArgumentNullException (nameof (type));
            Initializer = initializer;
            IsPrivate = isPrivate;
            IsExternal = isExternal;
            IsConstant = isConstant;
        }
    }
}
