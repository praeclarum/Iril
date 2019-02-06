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
        public readonly Assignment[] Instructions;

        public FunctionDefinition (LType returnType, GlobalSymbol symbol, IEnumerable<Parameter> parameters, IEnumerable<Assignment> instructions)
        {
            ReturnType = returnType;
            Symbol = symbol;
            Parameters = parameters.ToArray ();
            Instructions = instructions.ToArray ();
        }
    }

    public class Parameter
    {
        public readonly LType Type;

        public Parameter (LType type)
        {
            Type = type;
        }
    }

    [Flags]
    public enum ParameterAttributes
    {
        NonNull   = 1 << 0,
        NoCapture = 1 << 1,
        WriteOnly = 1 << 2,
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
    }
}
