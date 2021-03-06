﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            public HashSet<LocalSymbol> Definitions;
            public Bitvector Def;
            public Bitvector Use;
            public Bitvector PhiIns;
            public Bitvector LiveIn;
            public Bitvector LiveOut;
            public HashSet<LocalSymbol> Nexts;

            public LocalSymbol Symbol => Block.Symbol;
            public override string ToString () => Block.ToString ();

            public readonly SymbolTable<bool> AliveCache =
                new SymbolTable<bool> ();
        }

        readonly SymbolTable<BlockInfo> infos = new SymbolTable<BlockInfo> ();
        readonly List<BlockInfo> blocks = new List<BlockInfo> ();
        readonly SymbolTable<BlockInfo> blockIndex = new SymbolTable<BlockInfo> ();
        readonly Symbols symbols = new Symbols ();

        public LivelinessTable(DefinedFunction function)
        {
            this.function = function;
            FindSymbols ();
            CreateBlocks ();
        }

        void FindSymbols ()
        {
            foreach (var b in function.IRDefinition.Blocks) {
                foreach (var a in b.AllAssignments) {
                    if (a.HasResult)
                        symbols.GetIndex (a.Result);
                    foreach (var s in a.Instruction.ReferencedLocals)
                        symbols.GetIndex (s);
                }
            }
        }

        void CreateBlocks ()
        {
            var f = function.IRDefinition;
            //Debug.WriteLine ($"{f.Symbol} --------------------------");

            foreach (var b in f.Blocks) {
                var i = new BlockInfo {
                    Block = b,
                    Definitions = new HashSet<LocalSymbol> (),
                    Nexts = new HashSet<LocalSymbol> (b.Terminator.NextLabelSymbols),
                    Def = symbols.NewBitvector (),
                    Use = symbols.NewBitvector (),
                    PhiIns = symbols.NewBitvector (),
                    LiveIn = symbols.NewBitvector (),
                    LiveOut = symbols.NewBitvector (),
                };
                blocks.Add (i);
                blockIndex[b.Symbol] = i;

                //
                // Find references and definitions
                //
                foreach (var a in b.Assignments) {
                    if (a.Instruction is PhiInstruction phi) {
                        foreach (var v in phi.Values) {
                            if (v.Value is LocalValue loc) {
                                i.PhiIns.Set (loc.Symbol);
                            }
                        }
                    }
                }
                foreach (var a in b.AllAssignments) {
                    if (a.HasResult) {
                        i.Definitions.Add (a.Result);
                        i.Def.Set (a.Result);
                    }

                    foreach (var r in a.Instruction.ReferencedLocals) {
                        if (!i.Definitions.Contains (r)) {
                            i.Use.Set (r);
                        }
                    }
                }

                //Console.WriteLine ($"{i.Symbol}: {i.DefUse}");

                //
                // Add phi assignments to ensure they're alive
                //
                foreach (var a in b.Assignments) {
                    if (a.Instruction is PhiInstruction) {
                        i.LiveIn.Set (a.Result);
                    }
                }
            }

            DetermineLiveliness ();
        }

        void DetermineLiveliness ()
        {
            //
            // Depth-first sort the blocks
            // From "Principles of Compiler Design", 13.3 Depth First Search, Figure 13.8, Page 451
            //
            var dfn = new SymbolTable<int> ();
            var orderIndex = blocks.Count - 1;
            var depth = new BlockInfo[blocks.Count];
            void Search(BlockInfo b, HashSet<LocalSymbol> visited)
            {
                visited.Add (b.Symbol);
                foreach (var s in b.Nexts) {
                    if (!visited.Contains (s)) {
                        if (blockIndex.TryGetValue (s, out var sb)) {
                            Search (blockIndex[s], visited);
                        }
                        else {
                            throw new Exception ($"Undefined block {s} in {function.Symbol} ({string.Join(", ", blockIndex.Keys)})");
                        }
                    }
                }
                dfn[b.Symbol] = orderIndex;
                depth[orderIndex] = b;
                orderIndex--;
            }

            //
            // From "Principles of Compiler Design", 14.4 Backward Flow Problems, Algorithm 14.6, Page 490
            //
            var n = blocks.Count;
            if (n > 0) {
                Search (blocks[0], new HashSet<LocalSymbol> ());
            }

            var v = symbols.NewBitvector ();

            bool changed = true;
            while (changed) {
                changed = false;

                for (var i = n - 1; i >= 0; i--) {

                    var ni = depth[i];

                    // OUT[ni] := Union of IN[successor]                    
                    v.Clear ();
                    foreach (var s in ni.Nexts) {
                        v.Union (blockIndex[s].LiveIn);
                    }
                    if (!ni.LiveOut.Equals (v)) {
                        ni.LiveOut.Copy (v);
                        changed = true;
                    }

                    // IN[ni]  := OUT[ni] - DEF[ni] U USE[ni]
                    v.Copy (ni.LiveOut);
                    v.Subtract (ni.Def);
                    v.Union (ni.Use);
                    v.Union (ni.PhiIns);
                    if (!ni.LiveIn.Equals (v)) {
                        ni.LiveIn.Copy (v);
                        changed = true;
                    }
                }
            }

            //for (var i = 0; i < n; i++) {
            //    var ni = depth[i];
            //    Debug.WriteLine ($" IN[{ni.Symbol}]: {ni.LiveIn}");
            //    Debug.WriteLine ($"OUT[{ni.Symbol}]: {ni.LiveOut}");
            //}
        }

        class Symbols
        {
            public readonly SymbolTable<int> BitIndex = new SymbolTable<int> ();
            public int GetIndex (Symbol s)
            {
                if (BitIndex.TryGetValue (s, out var i))
                    return i;
                i = BitIndex.Count;
                BitIndex[s] = i;
                return i;
            }
            public Bitvector NewBitvector ()
            {
                var n = (BitIndex.Count + 63) / 64;
                var b = new Bitvector { Symbols = this, Bits = new ulong[n] };
                return b;
            }
        }

        class Bitvector
        {
            public Symbols Symbols;
            public ulong[] Bits;
            public void Union (Bitvector other)
            {
                for (var i = 0; i < Bits.Length; i++) {
                    Bits[i] = Bits[i] | other.Bits[i];
                }
            }
            public void Subtract (Bitvector other)
            {
                for (var i = 0; i < Bits.Length; i++) {
                    Bits[i] = Bits[i] & ~other.Bits[i];
                }
            }
            public void Copy (Bitvector other)
            {
                for (var i = 0; i < Bits.Length; i++) {
                    Bits[i] = other.Bits[i];
                }
            }
            public void Set (Symbol s) {
                var bi = Symbols.BitIndex[s];
                var i = bi / 64;
                var j = bi % 64;
                var v = 1uL << j;
                Bits[i] = Bits[i] | v;
            }
            public bool this[Symbol s] {
                get {
                    var bi = Symbols.BitIndex[s];
                    var i = bi / 64;
                    var j = bi % 64;
                    var v = 1uL << j;
                    return (Bits[i] & v) != 0uL;
                }
                set {
                    var bi = Symbols.BitIndex[s];
                    var i = bi / 64;
                    var j = bi % 64;
                    var v = 1uL << j;
                    if (value) {
                        Bits[i] = Bits[i] | v;
                    }
                    else {
                        Bits[i] = Bits[i] & ~v;
                    }
                }
            }
            public void Clear ()
            {
                for (var i = 0; i < Bits.Length; i++) {
                    Bits[i] = 0uL;
                }
            }
            public override bool Equals (object obj)
            {
                if (!(obj is Bitvector o))
                    return false;
                for (var i = 0; i < Bits.Length; i++) {
                    if (Bits[i] != o.Bits[i]) {
                        return false;
                    }
                }
                return true;
            }
            public override int GetHashCode ()
            {
                var h = 0;
                for (var i = 0; i < Bits.Length; i++) {
                    unchecked {
                        h += Bits[i].GetHashCode ();
                    }
                }
                return h;
            }
            public override string ToString ()
            {
                var r = new List<Symbol> ();
                foreach (var kv in Symbols.BitIndex) {
                    var bi = kv.Value;
                    var i = bi / 64;
                    var j = bi % 64;
                    var v = 1uL << j;
                    if ((Bits[i] & v) != 0uL) {
                        r.Add (kv.Key);
                    }
                }
                return string.Join (", ", r);
            }
        }

        bool IsAliveInBlock (LocalSymbol variable, BlockInfo bi)
        {
            if (bi.AliveCache.TryGetValue (variable, out var isAlive))
                return isAlive;

            isAlive = bi.LiveIn[variable] || bi.Def[variable];
            bi.AliveCache.Add (variable, isAlive);

            return isAlive;
        }        

        public bool VariablesInterfere (LocalSymbol symbol, IEnumerable<LocalSymbol> otherSymbols)
        {
            foreach (var block in blocks) {
                var symAlive = IsAliveInBlock (symbol, block);
                //Console.WriteLine ($"b={block.Symbol}, v={symbol} => {symAlive}");
                if (symAlive) {
                    foreach (var otherSymbol in otherSymbols) {
                        if (IsAliveInBlock (otherSymbol, block)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
