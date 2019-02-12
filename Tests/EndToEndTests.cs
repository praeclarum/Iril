using System.Linq;
using NUnit.Framework;
using Repil;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class EndToEndTests : TestsBase
    {
        [Test]
        public void SuiteSparse ()
        {
            var asmFileName = "SuiteSparse.dll";
            var irmods =
                new[] { "SuiteSparse.klu_defaults.ll" }
                .Select (x => Repil.Module.Parse (GetCode (x)));
            var compilation = new Compilation (
                irmods,
                assemblyName: asmFileName);

            var asmStream = new MemoryStream ();
            compilation.WriteAssembly (asmStream);
            Assert.Greater (asmStream.Length, 1024);

            var asmBytes = asmStream.ToArray ();
            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            File.WriteAllBytes (asmPath, asmBytes);
            var disProc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "ikdasm",
                    Arguments = $"\"{asmPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            var disAsmB = new StringBuilder ();
            disProc.Start ();
            while (!disProc.StandardOutput.EndOfStream) {
                string line = disProc.StandardOutput.ReadLine ();
                disAsmB.AppendLine (line);
            }
            var disAsm = disAsmB.ToString ();
            System.Console.WriteLine (disAsm);
            System.Console.WriteLine (asmPath);

            var asm = Assembly.Load (asmBytes);

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);
        }
    }
}
