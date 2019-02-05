using NUnit.Framework;
using System.IO;

using Repil.IR;

namespace Tests
{
    public class ModuleParserTests
    {
        public void Parse (string name)
        {
            var asm = GetType ().Assembly;
            var names = asm.GetManifestResourceNames ();
            string code;
            using (var s = asm.GetManifestResourceStream ("Tests.Inputs." + name)) {
                using (var r = new StreamReader (s)) {
                    code = r.ReadToEnd ();
                }
            }

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
