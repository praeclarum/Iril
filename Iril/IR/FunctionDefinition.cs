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
                foreach (var a in b.AllAssignments) {
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
                foreach (var a in b.AllAssignments) {
                    if (ReferenceEquals (a.Result, local.Symbol))
                        return a;
                }
            }
            throw new KeyNotFoundException ($"Undeclared local {local} in `{Symbol}`");
        }

        public Assignment FindAssignment (LocalValue local)
        {
            foreach (var b in Blocks) {
                foreach (var a in b.AllAssignments) {
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
        StructureReturn = 1 << 9,
        NoAlias         = 1 << 10,
        Align8          = 1 << 11,
        Byval           = 1 << 12,
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
