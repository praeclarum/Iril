using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Iril.IR;

namespace Iril
{
    class LivelinessTable
    {
        readonly DefinedFunction function;

        class BlockInfo
        {
            public Block Block;
            public HashSet<LocalSymbol> References;
            public HashSet<LocalSymbol> Definitions;
            public HashSet<LocalSymbol> PhiDefinitions;
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
                        b.AllAssignments.Select (x => x.Instruction)
                        .Where (x => !(x is PhiInstruction))
                        .SelectMany (x => x.ReferencedLocals.Distinct ())
                        .Distinct ()),
                    Definitions = new HashSet<LocalSymbol> (
                        b.AllAssignments.Where (x => !(x.Instruction is PhiInstruction) && x.HasResult)
                        .Select (x => x.Result)),
                    PhiDefinitions = new HashSet<LocalSymbol> (
                        b.Assignments.Where (x => x.Instruction is PhiInstruction && x.HasResult)
                        .Select (x => x.Result)),
                    Nexts = new HashSet<LocalSymbol> (b.Terminator.NextLabelSymbols),
                };
                infos[b.Symbol] = i;

                //
                // Add phi assignments to ensure they're alive
                //
                foreach (var a in b.Assignments) {
                    if (a.Instruction is PhiInstruction) {
                        i.References.Add (a.Result);
                    }
                }

                //
                // Add phi referenced variables to liveliness
                //
                foreach (var ob in f.Blocks) {
                    if (ReferenceEquals (ob, b))
                        continue;

                    foreach (var a in ob.Assignments) {
                        if (a.Instruction is PhiInstruction phi) {
                            foreach (var v in phi.Values) {
                                if (v.Label is LocalValue l
                                    && l.Symbol == b.Symbol
                                    && v.Value is LocalValue val) {

                                    i.References.Add (a.Result);
                                    i.References.Add (val.Symbol);
                                }
                            }
                        }
                    }
                }
            }
        }

        bool IsAliveInBlock (LocalSymbol variable, BlockInfo bi)
        {
            bool isAlive;
            lock (bi) {
                if (bi.AliveCache.TryGetValue (variable, out isAlive))
                    return isAlive;
            }

            //
            // Easy case, this block uses or defines the variable
            //
            isAlive = bi.References.Contains (variable)
                      || bi.Definitions.Contains (variable)
                      || bi.PhiDefinitions.Contains (variable);
            if (isAlive) {
                // Ensure the reference is made so that deep searches get saved
                lock (bi) {
                    bi.AliveCache[variable] = true;
                }
                return true;
            }

            //
            // Find a path with no definition of then variable.
            //
            var visitedBlocks = new HashSet<LocalSymbol> ();
            isAlive = DetermineIsAliveInBlock (variable, bi, visitedBlocks);
            lock (bi) {
                bi.AliveCache[variable] = isAlive;
            }
            return isAlive;
        }

        bool DetermineIsAliveInBlock (LocalSymbol variable, BlockInfo block, HashSet<LocalSymbol> visitedBlocks)
        {
            if (visitedBlocks.Contains (block.Symbol)) {
                lock (block) {
                    if (block.AliveCache.TryGetValue (variable, out var a))
                        return a;
                }
                //throw new Exception ("Cycle");
                return false;
            }
            visitedBlocks.Add (block.Symbol);

            //
            // Easy case, this block declares this phi variable
            //
            if (block.PhiDefinitions.Contains (variable))
                return true;

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
            var interfere = false;
            Parallel.ForEach (infos, ikv => {
                if (interfere)
                    return;
                var block = ikv.Value;
                var symAlive = IsAliveInBlock (symbol, block);
                //Console.WriteLine ($"b={block.Symbol}, v={symbol} => {symAlive}");
                if (symAlive) {
                    foreach (var otherSymbol in otherSymbols) {
                        if (IsAliveInBlock (otherSymbol, block)) {
                            interfere = true;
                            return;
                        }
                    }
                }
            });
            return interfere;
        }
    }
}
