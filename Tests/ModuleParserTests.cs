using NUnit.Framework;
using System.IO;

using Iril;

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
            Assert.Greater (mod.FunctionDefinitions.Count, 0);
        }

        [Test]
        public void KluDefaults ()
        {
            Parse ("SuiteSparse.klu_defaults.ll");
        }

        [Test]
        public void KluAnalyze ()
        {
            Parse ("SuiteSparse.klu_analyze.ll");
        }

        [Test]
        public void KluMemory ()
        {
            Parse ("SuiteSparse.klu_memory.ll");
        }

        [Test]
        public void SuiteSparseConfig ()
        {
            Parse ("SuiteSparse.SuiteSparse_config.ll");
        }

        [Test]
        public void Colamd ()
        {
            Parse ("SuiteSparse.colamd.ll");
        }

        [Test]
        public void Klu ()
        {
            Parse ("SuiteSparse.klu.ll");
        }

    }
}
