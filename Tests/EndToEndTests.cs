﻿using System.Linq;
using NUnit.Framework;
using Iril;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public unsafe class EndToEndTests : TestsBase
    {
        [Test]
        public void Ddaskr ()
        {
            var asmFileName = "Ddaskr.dll";
            var irmods =
                GetZippedCode ("ddaskr.zip")
                .Select (x => Iril.Module.Parse (x.Code, x.Name));
            var compilation = new Compilation (new CompilationOptions (
                irmods,
                assemblyName: asmFileName));
            compilation.Compile ();

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            try { File.Delete (asmPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

            System.Console.WriteLine (asmPath);

            AssertNoErrors (compilation);
            CheckDisassemblyForErrors (asmPath);

            var asm = Assembly.Load (File.ReadAllBytes (asmPath));

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);
        }

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
                .Select (x => Iril.Module.Parse (GetCode (x)));
            var compilation = new Compilation (new CompilationOptions (
                irmods,
                assemblyName: asmFileName));
            compilation.Compile ();

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            var pdbPath = Path.ChangeExtension (asmPath, ".pdb");
            var mdbPath = asmPath + ".mdb";
            try { File.Delete (asmPath); }
            catch { }
            try { File.Delete (pdbPath); }
            catch { }
            try { File.Delete (mdbPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

            System.Console.WriteLine (asmPath);

            AssertNoErrors (compilation);
            CheckDisassemblyForErrors (asmPath);

            var asm = Assembly.Load (File.ReadAllBytes (asmPath));

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);

            var funcs = asm.GetType ("SuiteSparse.Globals");
            Assert.NotNull (funcs);
            var klu_defaults = funcs.GetMethod ("klu_defaults");
            var klu_analyze = funcs.GetMethod ("klu_analyze");
            var klu_factor = funcs.GetMethod ("klu_factor");
            var klu_tsolve = funcs.GetMethod ("klu_solve");
            Assert.NotNull (klu_defaults);
            Assert.NotNull (klu_analyze);
            Assert.NotNull (klu_factor);
            Assert.NotNull (klu_tsolve);
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

            var testSolve = true;
            if (testSolve) {
                var p = Problem.Easy;
                var n = p.N;
                var b = p.B;
                var ap = p.Ap;
                var ai = p.Ai;
                var ax = p.Ax;

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
                var psymbolic = Pointer.Unbox (rklu_analyze);
                Assert.AreNotEqual (IntPtr.Zero, (IntPtr)psymbolic);

                var rklu_factor = klu_factor.Invoke (null, new object[] {
                    hap.AddrOfPinnedObject (),
                    hai.AddrOfPinnedObject (),
                    hax.AddrOfPinnedObject (),
                    rklu_analyze,
                    hcommon.AddrOfPinnedObject (),
                });
                var pnumeric = Pointer.Unbox (rklu_factor);
                Assert.AreNotEqual (IntPtr.Zero, (IntPtr)pnumeric);

                var rklu_solve = klu_tsolve.Invoke (null, new object[] {
                    rklu_analyze,
                    rklu_factor,
                    n,
                    1,
                    hb.AddrOfPinnedObject (),
                    hcommon.AddrOfPinnedObject (),
                });
                Assert.AreEqual (1, rklu_solve);

                hb.Free ();
                hap.Free ();
                hai.Free ();
                hax.Free ();

                for (var i = 0; i < p.Solution.Length; i++) {
                    Assert.AreEqual (p.Solution[i], b[i], 1.0e-9);
                }
            }

            hcommon.Free ();
        }

        [Test, Ignore("The test input code is missing functions")]
        public void Sqlite ()
        {
            var asmFileName = "SQLite.dll";
            var irmods =
                GetZippedCode ("sqlite3.ll.zip")
                .Select (x => Iril.Module.Parse (x.Code, x.Name));
            var compilation = new Compilation (new CompilationOptions (
                irmods,
                assemblyName: asmFileName));
            compilation.Compile ();

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            try { File.Delete (asmPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

            System.Console.WriteLine (asmPath);

            AssertNoErrors (compilation);
            CheckDisassemblyForErrors (asmPath);

            var asm = Assembly.Load (File.ReadAllBytes (asmPath));

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);
        }

        public class Problem
        {
            public readonly int N;
            public readonly double[] A;
            public readonly double[] B;
            public readonly double[] Solution;
            public readonly int[] Ap;
            public readonly int[] Ai;
            public readonly double[] Ax;

            public static Problem Easy = new Problem (
                5,
                new[] { 0, 2, 5, 9, 10, 12 },
                new[] { 0, 1, 0, 2, 4, 1, 2, 3, 4, 2, 1, 4 },
                new double[] { 2.0, 3.0, 3.0, -1.0, 4.0, 4.0, -3.0, 1.0, 2.0, 2.0, 6.0, 1.0 },
                new double[] { 8.0, 45.0, -3.0, 3.0, 19.0 },
                new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 });
            public static Problem Rectifier = new Problem ("13,0,0,0,-1,-1,0,0,0,0,0,0,0,0,-0.0836797104155031,-3.99999999775614E-13,0,0,0,0,0,0,1,0,0,0,0,0,0.0836797104158957,-0.0836797104154957,0,1,0,0,0,0,0,0,0,0,0.0836797104159031,0,-3.99999999775614E-13,1,0,0,0,0,0,0,0,0,0,-3.99999999775614E-13,-0.0836797104154957,0.0836797104158957,0,0,1,0,0,0,0,0,0,0,0,0,0.001,0,0,0,0,0,0,-1,0,0,0,0,0,-0.001,0,0,0,0,0,0,0,0,-1,0,0,0,2,0,0,0,1,0,0,0,0,0,0,0,0,-2,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,-1,-1,0,0,1,0,0,0,0,0,0,0,0,0,0,-1,-1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,-1,-1,1,0,0,0,0,0,0,0,0,0,0,0,0,0.0524390476507283,0.0524390476722283,-0.0524390476722331,-0.0524390476507235,0,0,52.4344844522889,-52.4344844522889,0,0,0,27.8932945781642,-0.840114078822413,27.0531804993418,26.2130664205193,0.0178614551704348,-0.0178614551704348,0.0178614551702971,0.00835161125022193,-0.00835161125022193,-0.0178614551702971,0.0262130664205193,-0.0262130664205193,-0.0262130664205193,-1.66560138730113E-17");
            public static Problem Transistors = new Problem ("63,0.000197534216268165,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.0197534216268167,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.0199509558430849,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-3.4965034965035E-05,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3.4965034965035E-05,0,0,0,0,0,0,0,0,0,0,0,1,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.000197534216268166,-6.53324860457756E-254,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.0197534216268167,1.30664972091551E-253,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.0199509558430849,-6.53324860457756E-254,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,-1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-9.78640607049719E-05,0,-3.47226766247511E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.00978640607049723,0,6.94453532495022E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.0098842701312022,0,-3.47226766247511E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-9.77143714827496E-05,9.77143714827496E-05,0,-3.13438954508084E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.00977143714827486,0.00977143714827486,0,6.26877909016168E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.00986915151975761,-0.00986915151975761,0,-3.13438954508084E-187,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,-5E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,-5E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,1,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,1,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9.35578717631887E-05,0,-9.35578717631887E-05,-4.28915176293861E-86,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.00935578717631893,0,-0.00935578717631893,8.57830352587722E-86,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.00944934504808212,0,0.00944934504808212,-4.28915176293861E-86,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.000103484610758398,-0.000103484610758398,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0.0103484610758397,-0.0103484610758397,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.0104519456865981,0.0104519456865981,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,4.97530070899879E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0.0199012028359949,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.0199509558430849,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,1,0,0,0,0,0,0,0,0,0,0,-0.000333333333333333,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.000333333333333333,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,-1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3.21048895770503E-05,-3.21048895770503E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6.36942675159236E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-6.36942675159236E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.00321048895770504,-0.00321048895770504,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.000434782608695652,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.00324259384728209,0.00367737645597774,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,1,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,-1,1,0,0,0,0,0,0,0,0,0,0,0,9.05435796762343E-05,-9.05435796762343E-05,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.00905435796762346,-0.00905435796762346,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.0091449015472997,0.0091449015472997,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.000333333333333333,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.000333333333333333,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-0.00285767689586728,-0.28576768958673,0.288625366482598,0,0,0,0,-0.00285767689581728,-0.28576768958683,0.288625366482648,0,0,0,0,0,5.0468290871335E-05,0.00504682908203353,-0.00509729737290486,5.03873570863246E-05,0.00503873570353241,-0.00508912306061873,0,0,-0.00075,0.00075,-0.00075,0.00075,0,0,0,0,4.81423475654129E-05,0.00481423475144132,-0.00486237709900673,5.35112704015281E-05,0.00535112703505278,-0.00540463830545431,0,0,-0.000719764006141026,-0.287905602476507,0.288625366482648,0,-0.005,0.005,0,0,0,-1.56618447581281E-05,-0.000955414012738853,0.000955414012738853,-0.00156618447071282,0,-0.00652173913043478,0.00810358544590573,0,0,4.65171443742883E-05,0.00465171443232885,-0.00469823157670314,-0.005,0.005,-3.8268343236509E-05,-14.4417437867513,-0.540697902317792,-3.82683432365518E-05,10.0600182565445,10.0625393398832,9.52044545232827,14.3007270419988,14.813559508002,-13.7754750828552,-14.3142258748955,0.000504956076459835,-0.000504956076459835,0.000493835540571419,-4.93835540572559E-06,-1.11205358884758E-05,0.000498773895977078,-4.93835535571308E-06,-0.000493835540671395,4.93835535571308E-06,0.000246728787692774,-0.000247106752978572,-0.00180290828071866,2.44285923359249E-06,-2.44285923359255E-06,2.44660146414429E-06,-0.00024428592845925,-0.000244660151514428,0.00129306274356116,0.000246999087172777,0.00104618971055532,-0.000246873033005838,0.000236232506593196,-0.000261297564306937,-6.18218048270558E-06,-0.00130413438469157,-0.000498773896027094,-0.000497530070900132,-0.000246999087172777,-2.33893565831922E-06,-0.000233893570934843,0.00079919062338254,0.0002330909860004,-0.000258710459760334,0.000566099637382141,-0.0002330909860004,-8.02584934428419E-07,8.02584985429766E-05,0.000307389177621805,8.10610834774073E-05,7.79952176525345E-05,0.000228591375034852,-0.000226328094144405,7.79952176525345E-05,2.2632808904443E-06,0.000498773896027094,-0.00080536048871447,-0.000246873033005838,2.5871045466197E-06,-0.000306586592687376,-0.000228591375034841,0.00026129756430694,4.88946069773701E-06,1.24382512700638E-06");

            public Problem (int n, int[] ap, int[] ai, double[] ax, double[] b, double[] solution)
            {
                N = n;
                Ap = ap;
                Ai = ai;
                Ax = ax;
                B = b;
                Solution = solution;
                A = new double[N * N];
                for (var c = 0; c < N; c++) {
                    var end = Ap[c + 1];
                    for (var p = Ap[c]; p < end; p++) {
                        var r = Ai[p];
                        A[c * N + r] = Ax[p];
                    }
                }
            }

            public Problem (string csv)
            {
                var cols = csv.Split (',').Select (x => double.Parse (x)).ToArray ();

                N = (int)cols[0];
                A = cols.Skip (1).Take (N * N).ToArray ();
                B = cols.Skip (1 + N * N).Take (N).ToArray ();
                Solution = cols.Skip (1 + N * N + N).Take (N).ToArray ();

                Ap = new int[N + 1];
                var ai = new List<int> ();
                var ax = new List<double> ();
                for (var c = 0; c < N; c++) {
                    Ap[c] = ai.Count;
                    for (var r = 0; r < N; r++) {
                        var x = A[c * N + r];
                        if (x != 0) {
                            ai.Add (r);
                            ax.Add (x);
                        }
                    }
                }
                Ap[N] = ai.Count;
                Ai = ai.ToArray ();
                Ax = ax.ToArray ();
            }
        }

        [Test]
        public void MicroPython ()
        {
            var asmFileName = "MicroPython.dll";
            var irmods =
                GetZippedCode ("micropython.zip")
                .Select (x => Iril.Module.Parse (x.Code, x.Name))
                .ToArray ();
            var errors = (from m in irmods from e in m.Errors select m + ": " + e).ToArray ();
            if (errors.Length > 0) {
                Assert.Fail ("{1} Parse Errors: {0}", string.Join (", ", errors), errors.Length);
            }
            var compilation = new Compilation (new CompilationOptions (
                irmods,
                assemblyName: asmFileName));
            compilation.Compile ();

            var asmPath = Path.Combine (Path.GetTempPath (), asmFileName);
            try { File.Delete (asmPath); }
            catch { }
            compilation.WriteAssembly (asmPath);

            System.Console.WriteLine (asmPath);

            AssertNoErrors (compilation);
            CheckDisassemblyForErrors (asmPath);

            var asm = Assembly.Load (File.ReadAllBytes (asmPath));

            var types = asm.GetTypes ();
            Assert.Greater (types.Length, 0);
        }
    }
}
