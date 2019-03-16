using System;
using System.Collections.Generic;
using System.Linq;

namespace Iril.Types
{
    public abstract class StructureType : LType
    {
    }

    public class OpaqueStructureType : StructureType
    {
        public static readonly OpaqueStructureType Opaque = new OpaqueStructureType ();

        public override long GetByteSize (Module module) => 0;
    }

    public class LiteralStructureType : StructureType
    {
        public static readonly LiteralStructureType Empty = new LiteralStructureType (Enumerable.Empty<LType> ());

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
        public PackedStructureType (IEnumerable<LType> elements) : base (elements)
        {
        }
    }
}
