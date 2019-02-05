using System;
namespace Repil.IR
{
    public abstract class Value
    {
    }

    public class LabelValue : Value
    {
        public readonly LocalSymbol Symbol;

        public LabelValue (LocalSymbol symbol)
        {
            Symbol = symbol;
        }
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
