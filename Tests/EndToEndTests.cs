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
        [Test]
        public void SuiteSparse ()
        {
            var asmFileName = "SuiteSparse.dll";
            var irmods = new[] {
                "SuiteSparse.amd_1.ll",
                "SuiteSparse.amd_2.ll",
                "SuiteSparse.amd_aat.ll",
                "SuiteSparse.amd_control.ll",
                "SuiteSparse.amd_defaults.ll",
                "SuiteSparse.amd_dump.ll",
                "SuiteSparse.amd_global.ll",
                "SuiteSparse.amd_info.ll",
                "SuiteSparse.amd_order.ll",
                "SuiteSparse.amd_post_tree.ll",
                "SuiteSparse.amd_postorder.ll",
                "SuiteSparse.amd_preprocess.ll",
                "SuiteSparse.amd_valid.ll",
                "SuiteSparse.btf_maxtrans.ll",
                "SuiteSparse.btf_order.ll",
                "SuiteSparse.btf_strongcomp.ll",
                "SuiteSparse.colamd.ll",
                "SuiteSparse.klu.ll",
                "SuiteSparse.klu_analyze.ll",
                "SuiteSparse.klu_analyze_given.ll",
                "SuiteSparse.klu_defaults.ll",
                "SuiteSparse.klu_diagnostics.ll",
                "SuiteSparse.klu_dump.ll",
                "SuiteSparse.klu_extract.ll",
                "SuiteSparse.klu_factor.ll",
                "SuiteSparse.klu_free_numeric.ll",
                "SuiteSparse.klu_free_symbolic.ll",
                "SuiteSparse.klu_kernel.ll",
                "SuiteSparse.klu_memory.ll",
                "SuiteSparse.klu_refactor.ll",
                "SuiteSparse.klu_scale.ll",
                "SuiteSparse.klu_solve.ll",
                "SuiteSparse.klu_sort.ll",
                "SuiteSparse.klu_tsolve.ll",
                "SuiteSparse.SuiteSparse_config.ll",
                }
                .Select (x => Repil.Module.Parse (GetCode (x)));
            var compilation = new Compilation (
                irmods,
                assemblyName: asmFileName);

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            try { File.Delete (asmPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

            Assert.IsFalse (compilation.HasErrors, "Errors Found:\n    " + String.Join ("\n    ", compilation.Messages));

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
            var klu_defaults = funcs.GetMethod ("klu_defaults");
            var klu_analyze = funcs.GetMethod ("klu_analyze");
            var klu_factor = funcs.GetMethod ("klu_factor");
            var klu_solve = funcs.GetMethod ("klu_solve");
            Assert.NotNull (klu_defaults);
            Assert.NotNull (klu_analyze);
            Assert.NotNull (klu_factor);
            Assert.NotNull (klu_solve);
            var rnull = klu_defaults.Invoke (null, new object[] { IntPtr.Zero });
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
            }

            var commont = asm.GetType ("SuiteSparse.klu_common");
            Assert.NotNull (commont);
            var common = Activator.CreateInstance (commont);
            Assert.NotNull (common);
            var hcommon = GCHandle.Alloc (common, GCHandleType.Pinned);
            var rklu_defaults = klu_defaults.Invoke (null, new object[] { hcommon.AddrOfPinnedObject () });
            Assert.AreEqual (1, rklu_defaults);
            var tol = commont.GetField ("tol").GetValue (common);
            Assert.AreEqual (0.001, tol);

            var testSolve = false;
            if (testSolve) {
                var n = 5;
                var b = new[] { 8.0, 45.0, -3.0, 3.0, 19.0 };
                var ap = new[] { 0, 2, 5, 9, 10, 12 };
                var ai = new[] { 0, 1, 0, 2, 4, 1, 2, 3, 4, 2, 1, 4 };
                var ax = new double[] { 2.0, 3.0, 3.0, -1.0, 4.0, 4.0, -3.0, 1.0, 2.0, 2.0, 6.0, 1.0 };

                var hb = GCHandle.Alloc (b, GCHandleType.Pinned);
                var hap = GCHandle.Alloc (ap, GCHandleType.Pinned);
                var hai = GCHandle.Alloc (ai, GCHandleType.Pinned);
                var hax = GCHandle.Alloc (ax, GCHandleType.Pinned);

                var rklu_analyze = klu_analyze.Invoke (null, new object[] {
                    n,
                    hap.AddrOfPinnedObject (),
                    hai.AddrOfPinnedObject (),
                    hcommon.AddrOfPinnedObject (),
                });
                Assert.AreNotEqual (IntPtr.Zero, (IntPtr)Pointer.Unbox (rklu_analyze));

                hb.Free ();
                hap.Free ();
                hai.Free ();
                hax.Free ();
            }

            hcommon.Free ();
        }
    }
}
