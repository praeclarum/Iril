using System;

namespace Repil.Types
{
    public abstract class StructureType : LType
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
