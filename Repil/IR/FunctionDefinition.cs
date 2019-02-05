﻿using System;
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
        public readonly Instruction[] Instructions;

        public FunctionDefinition (GlobalSymbol symbol, LType returnType, IEnumerable<Parameter> parameters, IEnumerable<Instruction> instructions)
        {
            Symbol = symbol;
            ReturnType = returnType;
            Parameters = parameters.ToArray ();
            Instructions = instructions.ToArray ();
        }
    }

    public class Parameter
    {
        public Parameter ()
        {
        }
    }
}