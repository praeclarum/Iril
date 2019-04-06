using System;

namespace Iril.IR
{
    public class Assignment
    {
        public readonly LocalSymbol Result;
        public readonly Instruction Instruction;
        public readonly SymbolTable<MetaSymbol> Metadata;

        public Assignment(Instruction instruction, SymbolTable<MetaSymbol> metadata = null)
        {
            Result = LocalSymbol.None;
            Instruction = instruction ?? throw new ArgumentNullException(nameof(instruction));
            Metadata = metadata ?? new SymbolTable<MetaSymbol>();
        }

        public Assignment(LocalSymbol result, Instruction instruction, SymbolTable<MetaSymbol> metadata = null)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
            Instruction = instruction ?? throw new ArgumentNullException(nameof(instruction));
            Metadata = metadata ?? new SymbolTable<MetaSymbol>();
        }

        public bool HasResult => Result != LocalSymbol.None;

        public MetaSymbol DebugSymbol => Metadata.TryGetValue(MetaSymbol.Dbg, out var d) ? d : MetaSymbol.None;
        public bool HasDebugSymbol => DebugSymbol != MetaSymbol.None;

        public override string ToString()
        {
            if (!HasResult)
                return Instruction.ToString();
            return $"{Result} = {Instruction}";
        }
    }
}
