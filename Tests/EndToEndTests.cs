using System.Linq;
using NUnit.Framework;
using Repil;
using System.IO;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class EndToEndTests : TestsBase
    {
        [Test]
        public void SuiteSparse ()
        {
            var rp = typeof (object).Assembly.Location;
            var irmods =
                new[] { "SuiteSparse.klu_defaults.ll" }
                .Select (x => Repil.Module.Parse (GetCode (x)));
            var compilation = new Compilation (
                irmods,
                assemblyName: "SuiteSparse.dll",
                systemAssemblyPath: rp);

            var o = new MemoryStream ();
            compilation.WriteAssembly (o);
            Assert.Greater (o.Length, 1024);

            var asm = Assembly.Load (o.ToArray ());

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);
        }
    }
}
