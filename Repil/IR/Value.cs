using System;
namespace Repil.IR
{
    public abstract class Value
    {
    }

    public class LocalValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LocalValue (LocalSymbol symbol)
        {
            Symbol = symbol;
        }
    }
}
