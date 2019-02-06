using System;
using Repil.Types;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Repil.IR
{
    public class FunctionDefinition
    {
        public readonly GlobalSymbol Symbol;
        public readonly LType ReturnType;
        public readonly Parameter[] Parameters;
        public readonly Assignment[] Instructions;

        public FunctionDefinition (GlobalSymbol symbol, LType returnType, IEnumerable<Parameter> parameters, IEnumerable<Assignment> instructions)
        {
            Symbol = symbol;
            ReturnType = returnType;
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
        NonNull = 1 << 0,
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
}
