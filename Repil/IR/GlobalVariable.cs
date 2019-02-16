using System;
using Repil.Types;

namespace Repil.IR
{
    public class GlobalVariable
    {
        public readonly GlobalSymbol Symbol;
        public readonly bool IsConstant;
        public readonly LType Type;
        public readonly Constant Initializer;

        public GlobalVariable (GlobalSymbol symbol, bool isConstant, LType type, Constant initializer, bool isPrivate)
        {
            Symbol = symbol;
            IsConstant = isConstant;
            Type = type ?? throw new ArgumentNullException (nameof (type));
            Initializer = initializer;
        }
    }
}
