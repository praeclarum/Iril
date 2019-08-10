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
        public static readonly LiteralStructureType Empty = new LiteralStructureType (false, Enumerable.Empty<LType> ());

        public readonly bool IsPacked;
        public readonly LType[] Elements;

        public LiteralStructureType (bool isPacked, IEnumerable<LType> elements)
        {
            IsPacked = isPacked;
            Elements = elements.ToArray ();
        }

        public override string ToString () =>
            $"{{{string.Join(", ", (object[])Elements)}}}";

        public override long GetByteSize (Module module)
        {
            var offset = 0;
            for (var i = 0; i < Elements.Length; i++) {
                var type = Elements[i];
                var size = type.GetByteSize (module);
                offset = module.Align (offset, type, (int)size);
                offset += (int)size;
            }
            return offset;
        }

        public override int GetAlignment (Module module) => Elements[0].GetAlignment (module);

        public override bool StructurallyEquals (LType other) =>
            other is LiteralStructureType a
            && IsPacked == a.IsPacked
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
            + IsPacked.GetHashCode ()
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
        public PackedStructureType (IEnumerable<LType> elements) : base (true, elements)
        {
        }
    }
}
