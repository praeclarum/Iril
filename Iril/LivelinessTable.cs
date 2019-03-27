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

            public HashSet<LocalSymbol> LiveIn;
            public HashSet<LocalSymbol> LiveOut;
            public HashSet<LocalSymbol> RemainingNexts;

            public LocalSymbol Symbol => Block.Symbol;

            public readonly SymbolTable<bool> AliveCache =
                new SymbolTable<bool> ();

            public bool IsAlive (LocalSymbol symbol)
            {
                return LiveIn.Contains (symbol) || LiveOut.Contains (symbol);
            }
        }

        readonly List<BlockInfo> infoList = new List<BlockInfo> ();
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
                        .Where (x => !(x is PhiInstruction))
                        .Concat (new IR.Instruction[] { b.Terminator })
                        .SelectMany (x => x.ReferencedLocals.Distinct ())
                        .Distinct ()),
                    Definitions = new HashSet<LocalSymbol> (
                        b.Assignments.Where (x => !(x.Instruction is PhiInstruction) && x.HasResult)
                        .Select (x => x.Result)),
                    PhiDefinitions = new HashSet<LocalSymbol> (
                        b.Assignments.Where (x => x.Instruction is PhiInstruction && x.HasResult)
                        .Select (x => x.Result)),
                    Nexts = new HashSet<LocalSymbol> (b.Terminator.NextLabelSymbols),
                    LiveIn = new HashSet<LocalSymbol> (),
                    LiveOut = new HashSet<LocalSymbol> (),
                    RemainingNexts = new HashSet<LocalSymbol> (b.Terminator.NextLabelSymbols),
                };
                infoList.Add (i);
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
                                    i.LiveOut.Add (val.Symbol);
                                }
                            }
                        }
                    }
                }
            }

            BuildLivenessBitvector ();
        }

        void BuildLivenessBitvector ()
        {
            //Console.WriteLine (function.Symbol);
            var rem = new List<BlockInfo> (infoList);
            rem.Reverse ();

            while (rem.Count > 0) {

                var torem = rem.FirstOrDefault (x => x.RemainingNexts.Count == 0);
                if (torem == null) {
                    //Console.WriteLine ("UH OH RECURSIION");
                    torem = rem.OrderBy (x => x.RemainingNexts.Count).First ();
                }
                //Console.WriteLine ($"torem {torem.Symbol}");
                rem.Remove (torem);

                var instructions = torem.Block.Assignments.Concat (new[] { new Assignment (torem.Block.Terminator) }).Reverse ();
                var liveIn = new HashSet<LocalSymbol> (torem.LiveOut);
                foreach (var a in instructions) {
                    if (a.Instruction is PhiInstruction phi) {
                        if (a.HasResult) {
                            liveIn.Add (a.Result);
                        }
                    }
                    else {
                        foreach (var r in a.Instruction.ReferencedLocals) {
                            liveIn.Add (r);
                        }
                        if (a.HasResult) {
                            liveIn.Add (a.Result);
                        }
                    }
                }
                torem.LiveIn = liveIn;

                foreach (var b in rem) {
                    if (b.RemainingNexts.Contains (torem.Symbol)) {
                        foreach (var v in liveIn) {
                            b.LiveOut.Add (v);
                        }
                        b.RemainingNexts.Remove (torem.Symbol);
                    }
                }
            }

            //foreach (var i in infos) {
            //    Console.WriteLine ($"{i.Key}: in: [{string.Join (",", i.Value.LiveIn)}], out: [{string.Join (",", i.Value.LiveOut)}]");
            //}
            //Console.WriteLine ();
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

        public bool VariablesInterfere2 (LocalSymbol symbol, IEnumerable<LocalSymbol> otherSymbols)
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

        public bool VariablesInterfere (LocalSymbol symbol, IEnumerable<LocalSymbol> otherSymbols)
        {
            foreach (var b in infoList) {
                if (!b.IsAlive (symbol))
                    continue;
                foreach (var o in otherSymbols) {
                    if (b.IsAlive (o))
                        return true;
                }
            }
            return false;
        }
    }
}
