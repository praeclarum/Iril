using System;
using System.Collections.Generic;
using System.Linq;

namespace Iril.IR
{
    public class Block
    {
        public readonly LocalSymbol Symbol;
        public readonly Assignment[] Assignments;
        public readonly Assignment TerminatorAssignment;
        public TerminatorInstruction Terminator => (TerminatorInstruction)TerminatorAssignment.Instruction;

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
    }
}
