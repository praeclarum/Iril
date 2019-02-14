using System;
using System.Collections.Generic;
using System.Linq;

namespace Repil.Types
{
    public abstract class StructureType : LType
    {
    }

    public class OpaqueStructureType : StructureType
    {
        public override long GetByteSize (Module module) => 0;
    }

    public class LiteralStructureType : StructureType
    {
        public readonly LType[] Elements;

        public LiteralStructureType (IEnumerable<LType> elements)
        {
            Elements = elements.ToArray ();
        }

        public override string ToString () =>
            $"{{{string.Join(", ", (object[])Elements)}}}";

        public override long GetByteSize (Module module) => Elements.Sum (x => x.GetByteSize (module));
    }

    public class PackedStructureType : LiteralStructureType
    {
        public PackedStructureType (LType[] elements) : base (elements)
        {
        }
    }
}
