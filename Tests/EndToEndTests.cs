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
            var irmods = new[] {
                "SuiteSparse.colamd.ll",
                "SuiteSparse.SuiteSparse_config.ll",
                "SuiteSparse.klu.ll",
                "SuiteSparse.klu_analyze.ll",
                "SuiteSparse.klu_analyze_given.ll",
                "SuiteSparse.klu_memory.ll",
                "SuiteSparse.klu_defaults.ll",
                }
                .Select (x => Repil.Module.Parse (GetCode (x)));
            var compilation = new Compilation (
                irmods,
                assemblyName: asmFileName);

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            try { File.Delete (asmPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

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
            //System.Console.WriteLine (disAsm);
            System.Console.WriteLine (asmPath);

            var asm = Assembly.Load (File.ReadAllBytes (asmPath));

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);

            var funcs = asm.GetType ("SuiteSparse.Functions");
            Assert.NotNull (funcs);
            var defs = funcs.GetMethod ("klu_defaults");
            Assert.NotNull (defs);
            var rnull = defs.Invoke (null, new object[] { IntPtr.Zero });
            Assert.AreEqual (0, rnull);

            var testMemory = true;
            if (testMemory) {

                funcs.GetMethod ("SuiteSparse_start").Invoke (null, Array.Empty<object> ());
                var ssmalloc = Pointer.Unbox (funcs.GetMethod ("SuiteSparse_malloc").Invoke (null, new object[] { 2L, 16L }));
                Assert.NotZero ((int)ssmalloc);
                int ok = 1;
                var ssrealloc = Pointer.Unbox (funcs.GetMethod ("SuiteSparse_realloc").Invoke (null, new object[] { 4L, 2L, 16L, Pointer.Box (ssmalloc, typeof (byte*)), Pointer.Box (&ok, typeof (int*)) }));
                Assert.NotZero ((long)ssrealloc);
                var ssfree = Pointer.Unbox (funcs.GetMethod ("SuiteSparse_free").Invoke (null, new object[] { Pointer.Box (ssrealloc, typeof (byte*)) }));
                Assert.AreEqual (0L, (long)ssfree);

                var commont = asm.GetType ("SuiteSparse.klu_common");
                Assert.NotNull (commont);
                var common = Activator.CreateInstance (commont);
                Assert.NotNull (common);
                var h = GCHandle.Alloc (common, GCHandleType.Pinned);
                var rcommon = defs.Invoke (null, new object[] { h.AddrOfPinnedObject () });
                h.Free ();
                Assert.AreEqual (1, rcommon);
                var tol = commont.GetField ("tol").GetValue (common);
                Assert.AreEqual (0.001, tol);
            }
        }
    }
}
