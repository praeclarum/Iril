﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Iril.IR
{
    public class Block
    {
        public readonly LocalSymbol Symbol;
        public readonly Assignment[] Assignments;
        public readonly Assignment TerminatorAssignment;
        public IEnumerable<Assignment> AllAssignments => Assignments.Concat (new[] { TerminatorAssignment });
        public TerminatorInstruction Terminator => (TerminatorInstruction)TerminatorAssignment.Instruction;

        public Instruction FirstInstruction => Assignments.Length > 0 ? Assignments[0].Instruction : Terminator;
        public Assignment FirstNonPhiAssignment => AllAssignments.FirstOrDefault (x => !(x.Instruction is PhiInstruction));

        public Block(LocalSymbol symbol, IEnumerable<Assignment> assignments, Assignment terminator)
        {
            if (assignments == null)
            {
                throw new ArgumentNullException(nameof(assignments));
            }
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Assignments = assignments.ToArray();
            TerminatorAssignment = terminator ?? throw new ArgumentNullException(nameof(terminator));
        }

        public override string ToString()
        {
            return $"{Symbol} = {{ {Assignments.Length} assignments }}";
        }

        public Block WithSymbol(LocalSymbol newSymbol)
        {
            return new Block(newSymbol, Assignments, TerminatorAssignment);
        }

        public IEnumerable<LocalSymbol> PhiPredecessors
        {
            get
            {
                var q = from a in Assignments
                        let phi = a.Instruction as PhiInstruction
                        where phi != null
                        from v in phi.Values
                        let label = v.Label as LocalValue
                        where label != null
                        select label.Symbol;
                return q.Distinct();
            }
        }

        public Assignment FindAssignment (Symbol symbol)
        {
            foreach (var a in Assignments) {
                if (a.HasResult && a.Result == symbol)
                    return a;
            }
            return null;
        }
    }
}
