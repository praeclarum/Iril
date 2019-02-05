using System;
namespace Repil.IR
{
    public class SourceFilename : ModulePart
    {
        public readonly string Name;

        public SourceFilename (string name)
        {
            Name = name;
        }
    }
}
