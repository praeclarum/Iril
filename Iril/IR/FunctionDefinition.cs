using System;
using Iril.Types;
using System.Collections.Generic;
using System.Linq;

namespace Iril.IR
{
    public class FunctionDefinition
    {
        public readonly LType ReturnType;
        public readonly GlobalSymbol Symbol;
        public readonly Parameter[] Parameters;
        public readonly Block[] Blocks;
        public readonly SymbolTable<MetaSymbol> MetaRefs;
        public readonly bool IsExternal;
        public readonly SymbolTable<Assignment> Phis;

        public FunctionDefinition (LType returnType, GlobalSymbol symbol, IEnumerable<Parameter> parameters, IEnumerable<Block> blocks, bool isExternal, SymbolTable<MetaSymbol> metaRefs = null)
        {
            ReturnType = returnType;
            Symbol = symbol;
            IsExternal = isExternal;

            var implicitLocalCounter = 0;

            var ps = new List<Parameter> ();
            foreach (var p in parameters) {
                if (p.Symbol == LocalSymbol.None) {
                    var s = (LocalSymbol)Iril.Symbol.Intern ('%', implicitLocalCounter);
                    ps.Add (p.WithSymbol (s));
                    implicitLocalCounter++;
                }
            }

            var bs = new List<Block> ();
            foreach (var b in blocks) {
                if (b.Symbol == LocalSymbol.None) {
                    var s = (LocalSymbol)Iril.Symbol.Intern ('%', implicitLocalCounter);
                    bs.Add (b.WithSymbol (s));
                    implicitLocalCounter++;
                }
                foreach (var a in b.Assignments) {
                    if (a.HasResult && a.Result.HasNumericValue) {
                        implicitLocalCounter = a.Result.NumericValue + 1;
                    }
                }
            }

            Parameters = ps.ToArray ();
            Blocks = bs.ToArray ();
            MetaRefs = metaRefs ?? new SymbolTable<MetaSymbol> ();

            Phis = new SymbolTable<Assignment> ();
            foreach (var b in blocks) {
                foreach (var a in b.Assignments) {
                    if (a.HasResult && a.Instruction is PhiInstruction) {
                        Phis[a.Result] = a;
                    }
                }
            }
        }

        public override string ToString () =>
            $"{ReturnType} ({String.Join(", ", (object[])Parameters)}) {{ }}";

        public Assignment GetAssignment (LocalValue local)
        {
            foreach (var b in Blocks) {
                foreach (var a in b.Assignments) {
                    if (ReferenceEquals (a.Result, local.Symbol))
                        return a;
                }
            }
            throw new KeyNotFoundException ($"{local}");
        }

        public Assignment FindAssignment (LocalValue local)
        {
            foreach (var b in Blocks) {
                foreach (var a in b.Assignments) {
                    if (ReferenceEquals (a.Result, local.Symbol))
                        return a;
                }
            }
            return null;
        }
    }

    public class Parameter
    {
        public readonly LocalSymbol Symbol;
        public readonly LType ParameterType;

        public Parameter (LocalSymbol symbol, LType type)
        {
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
            ParameterType = type ?? throw new ArgumentNullException (nameof (type));
        }

        public override string ToString () =>
            $"{ParameterType}";

        public Parameter WithSymbol (LocalSymbol symbol)
        {
            return new Parameter (symbol, ParameterType);
        }
    }

    [Flags]
    public enum ParameterAttributes
    {
        NonNull         = 1 << 0,
        NoCapture       = 1 << 1,
        WriteOnly       = 1 << 2,
        ReadOnly        = 1 << 3,
        ReadNone        = 1 << 4,
        SignExtend      = 1 << 5,
        ZeroExtend      = 1 << 6,
        Returned        = 1 << 7,
        Dereferenceable = 1 << 8,
    }

    public class Block
    {
        public readonly LocalSymbol Symbol;
        public readonly Assignment[] Assignments;
        public readonly TerminatorInstruction Terminator;

        public Block (LocalSymbol symbol, IEnumerable<Assignment> assignments, TerminatorInstruction terminator)
        {
            if (assignments == null) {
                throw new ArgumentNullException (nameof (assignments));
            }
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
            Assignments = assignments.ToArray ();
            Terminator = terminator ?? throw new ArgumentNullException (nameof (terminator));
        }

        public override string ToString ()
        {
            return $"{Symbol} = {{ {Assignments.Length} assignments }}";
        }

        public Block WithSymbol (LocalSymbol newSymbol)
        {
            return new Block (newSymbol, Assignments, Terminator);
        }

        public IEnumerable<LocalSymbol> PhiPredecessors {
            get {
                var q = from a in Assignments
                        let phi = a.Instruction as PhiInstruction
                        where phi != null
                        from v in phi.Values
                        let label = v.Label as LocalValue
                        where label != null
                        select label.Symbol;
                return q.Distinct ();
            }
        }
    }

    public class Assignment
    {
        public readonly LocalSymbol Result;
        public readonly Instruction Instruction;
        public readonly SymbolTable<MetaSymbol> Metadata;

        public Assignment (Instruction instruction, SymbolTable<MetaSymbol> metadata = null)
        {
            Result = LocalSymbol.None;
            Instruction = instruction ?? throw new ArgumentNullException (nameof (instruction));
            Metadata = metadata ?? new SymbolTable<MetaSymbol> ();
        }

        public Assignment (LocalSymbol result, Instruction instruction, SymbolTable<MetaSymbol> metadata = null)
        {
            Result = result ?? throw new ArgumentNullException (nameof (result));
            Instruction = instruction ?? throw new ArgumentNullException (nameof (instruction));
            Metadata = metadata ?? new SymbolTable<MetaSymbol> ();
        }

        public bool HasResult => Result != LocalSymbol.None;

        public MetaSymbol DebugSymbol => Metadata.TryGetValue (MetaSymbol.Dbg, out var d) ? d : MetaSymbol.None;
        public bool HasDebugSymbol => DebugSymbol != MetaSymbol.None;

        public override string ToString ()
        {
            if (!HasResult)
                return Instruction.ToString ();
            return $"{Result} = {Instruction}";
        }
    }

    public class FunctionDeclaration
    {
        public readonly LType ReturnType;
        public readonly GlobalSymbol Symbol;
        public readonly Parameter[] Parameters;

        public FunctionDeclaration (LType returnType, GlobalSymbol symbol, IEnumerable<Parameter> parameters)
        {
            if (parameters == null) {
                throw new ArgumentNullException (nameof (parameters));
            }

            ReturnType = returnType ?? throw new ArgumentNullException (nameof (returnType));
            Symbol = symbol ?? throw new ArgumentNullException (nameof (symbol));
            Parameters = parameters.ToArray ();
        }

        public override string ToString () =>
            $"{ReturnType} ({String.Join (", ", (object[])Parameters)})";
    }
}
