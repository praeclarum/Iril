using System;
using Repil.Types;
using System.Collections.Generic;
using System.Linq;

namespace Repil.IR
{
    public class FunctionDefinition
    {
        public readonly LType ReturnType;
        public readonly GlobalSymbol Symbol;
        public readonly Parameter[] Parameters;
        public readonly Block[] Blocks;

        public FunctionDefinition (LType returnType, GlobalSymbol symbol, IEnumerable<Parameter> parameters, IEnumerable<Block> blocks)
        {
            ReturnType = returnType;
            Symbol = symbol;
            Parameters = parameters.ToArray ();
            Blocks = blocks.ToArray ();
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
            throw new KeyNotFoundException ();
        }
    }

    public class Parameter
    {
        public readonly LocalSymbol Symbol;
        public readonly LType Type;

        public Parameter (LocalSymbol symbol, LType type)
        {
            Symbol = symbol;
            Type = type;
        }

        public override string ToString () =>
            $"{Type}";
    }

    [Flags]
    public enum ParameterAttributes
    {
        NonNull   = 1 << 0,
        NoCapture = 1 << 1,
        WriteOnly = 1 << 2,
    }

    public class Block
    {
        public readonly Symbol Symbol;
        public readonly Assignment[] Assignments;

        public Block (Symbol symbol, IEnumerable<Assignment> assignments)
        {
            Symbol = symbol;
            Assignments = assignments.ToArray ();
        }
    }

    public class Assignment
    {
        public readonly LocalSymbol Result;
        public readonly Instruction Instruction;

        public Assignment (Instruction instruction)
        {
            Result = null;
            Instruction = instruction;
        }

        public Assignment (LocalSymbol result, Instruction instruction)
        {
            Result = result;
            Instruction = instruction;
        }
    }

    public class FunctionDeclaration
    {
        public readonly LType ReturnType;
        public readonly GlobalSymbol Symbol;
        public readonly Parameter[] Parameters;

        public FunctionDeclaration (LType returnType, GlobalSymbol symbol, IEnumerable<Parameter> parameters)
        {
            ReturnType = returnType;
            Symbol = symbol;
            Parameters = parameters.ToArray ();
        }

        public override string ToString () =>
            $"{ReturnType} ({String.Join (", ", (object[])Parameters)})";
    }
}
