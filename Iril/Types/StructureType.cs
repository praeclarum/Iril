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

        public override long GetByteSize (Module module) => module.PointerByteSize;

        public override int GetAlignment (Module module) => module.PointerByteSize;

        public override bool StructurallyEquals (LType other) =>
            other is OpaqueStructureType;

        public override int GetStructuralHashCode () =>
            1012;
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

        public override int GetAlignment (Module module) => Elements[0].GetAlignment (module);

        public override bool StructurallyEquals (LType other) =>
            other is LiteralStructureType a
            && ElementsEqual (a.Elements);

        bool ElementsEqual (LType[] parameterTypes)
        {
            if (Elements.Length != parameterTypes.Length)
                return false;
            for (var i = 0; i < Elements.Length; i++) {
                if (!Elements[i].StructurallyEquals (parameterTypes[i]))
                    return false;
            }
            return true;
        }

        public override int GetStructuralHashCode () =>
            1123
            + GetElementsHash ();

        int GetElementsHash ()
        {
            var h = Elements.Length.GetHashCode ();
            for (var i = 0; i < Elements.Length; i++) {
                h += Elements[i].GetStructuralHashCode ();
            }
            return h;
        }
    }

    public class PackedStructureType : LiteralStructureType
    {
        public PackedStructureType (IEnumerable<LType> elements) : base (elements)
        {
        }
    }
}
