using NUnit.Framework;
using System.IO;

using Repil;

namespace Tests
{
    [TestFixture]
    public class ModuleParserTests : TestsBase
    {
        public void Parse (string name)
        {
            var code = GetCode (name);

            var mod = Module.Parse (code);
            Assert.NotNull (mod);
            Assert.Greater (mod.SourceFilename.Length, 2);
            Assert.Greater (mod.TargetTriple.Length, 2);
            Assert.Greater (mod.TargetDatalayout.Length, 2);
        }

        [Test]
        public void KluDefaults ()
        {
            Parse ("SuiteSparse.klu_defaults.ll");
        }
    }
}
