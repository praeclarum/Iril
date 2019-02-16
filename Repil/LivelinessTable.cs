using System;
using System.Collections.Generic;
using System.Linq;

using Repil.IR;

namespace Repil
{
    class LivelinessTable
    {
        readonly DefinedFunction function;

        class BlockInfo
        {
            public Block Block;
            public HashSet<LocalSymbol> References;
            public HashSet<LocalSymbol> Definitions;
            public HashSet<LocalSymbol> Nexts;

            public LocalSymbol Symbol => Block.Symbol;

            public readonly SymbolTable<bool> AliveCache =
                new SymbolTable<bool> ();
        }

        readonly SymbolTable<BlockInfo> infos = new SymbolTable<BlockInfo> ();

        public LivelinessTable(DefinedFunction function)
        {
            this.function = function;
            BuildUsageInfo ();
        }

        void BuildUsageInfo ()
        {
            var f = function.IRDefinition;

            foreach (var b in f.Blocks) {
                var i = new BlockInfo {
                    Block = b,
                    References = new HashSet<LocalSymbol> (
                        b.Assignments.Select (x => x.Instruction)
                        .Concat (new IR.Instruction[] { b.Terminator })
                        .SelectMany (x => x.ReferencedLocals.Distinct ())
                        .Distinct ()),
                    Definitions = new HashSet<LocalSymbol> (
                        b.Assignments.Where (x => x.Result != LocalSymbol.None)
                        .Select (x => x.Result)),
                    Nexts = new HashSet<LocalSymbol> (b.Terminator.NextLabelSymbols),
                };
                infos[b.Symbol] = i;
            }
        }

        bool IsAliveInBlock (LocalSymbol variable, BlockInfo bi)
        {
            if (bi.AliveCache.TryGetValue (variable, out var isAlive))
                return isAlive;

            //
            // Easy case, this block uses or defines the variable
            //
            isAlive = bi.References.Contains (variable) || bi.Definitions.Contains (variable);
            if (isAlive) {
                // Ensure the reference is made so that deep searches get saved
                bi.AliveCache[variable] = true;
                return true;
            }

            //
            // Find a path with no definition of then variable.
            //
            var visitedBlocks = new HashSet<LocalSymbol> ();
            visitedBlocks.Add (bi.Symbol);
            isAlive = DetermineIsAliveInBlock (variable, bi, visitedBlocks);
            bi.AliveCache[variable] = isAlive;
            return isAlive;
        }

        bool DetermineIsAliveInBlock (LocalSymbol variable, BlockInfo block, HashSet<LocalSymbol> visitedBlocks)
        {
            if (visitedBlocks.Contains (block.Symbol))
                return block.AliveCache.TryGetValue (variable, out var a) && a;
            visitedBlocks.Add (block.Symbol);

            //
            // Easy case, this block declares the variable
            //
            if (block.Definitions.Contains (variable))
                return false;

            //
            // Easy case, this block uses the variable
            //
            if (block.References.Contains (variable))
                return true;

            //
            // Hard case, do a depth first walk
            //
            foreach (var b in block.Nexts) {
                var nbi = infos[b];
                if (DetermineIsAliveInBlock (variable, nbi, visitedBlocks))
                    return true;
            }

            return false;
        }

        public bool VariablesInterfere (LocalSymbol symbol, IEnumerable<LocalSymbol> otherSymbols)
        {
            foreach (var ikv in infos) {
                var block = ikv.Value;
                var symAlive = IsAliveInBlock (symbol, block);
                //Console.WriteLine ($"b={block.Symbol}, v={symbol} => {symAlive}");
                if (symAlive) {
                    foreach (var otherSymbol in otherSymbols) {
                        if (IsAliveInBlock (otherSymbol, block))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
