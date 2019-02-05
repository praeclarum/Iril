using System;

namespace Repil.IR
{
    public abstract class StructureType : Type
    {
    }

    public class OpaqueStructureType : StructureType
    {
    }

    public class LiteralStructureType : StructureType
    {
    }

    public class PackedStructureType : LiteralStructureType
    {
    }
}
