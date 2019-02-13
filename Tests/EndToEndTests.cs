using System.Linq;
using NUnit.Framework;
using Repil;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices;

namespace Tests
{
    [TestFixture]
    public unsafe class EndToEndTests : TestsBase
    {
        // clang -g -O3 -S -emit-llvm -fpic *.c

        [Test]
        public void SuiteSparse ()
        {
            var asmFileName = "SuiteSparse.dll";
            var irmods =
                new[] {
                    "SuiteSparse.klu_defaults.ll",
                    "SuiteSparse.klu_analyze.ll",
                }
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

            var funcs = asm.GetType ("SuiteSparse.Functions");
            Assert.NotNull (funcs);
            var defs = funcs.GetMethod ("klu_defaults");
            Assert.NotNull (defs);
            var rnull = defs.Invoke (null, new object[] { IntPtr.Zero });
            Assert.AreEqual (0, rnull);

            var commont = asm.GetType ("SuiteSparse.klu_common_struct");
            Assert.NotNull (commont);
            var common = Activator.CreateInstance (commont);
            Assert.NotNull (common);
            var h = GCHandle.Alloc (common, GCHandleType.Pinned);
            var rcommon = defs.Invoke (null, new object[] { h.AddrOfPinnedObject() });
            h.Free ();
            Assert.AreEqual (1, rcommon);
            var tol = commont.GetField ("F0").GetValue (common);
            Assert.AreEqual (0.001, tol);
        }
    }
}
