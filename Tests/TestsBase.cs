using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Repil;

namespace Tests
{
    public class TestsBase
    {
        protected string GetCode (string resourceName)
        {
            var asm = GetType ().Assembly;
            var names = asm.GetManifestResourceNames ();
            string code;
            using (var s = asm.GetManifestResourceStream ("Tests.Inputs." + resourceName)) {
                using (var r = new StreamReader (s)) {
                    code = r.ReadToEnd ();
                }
            }
            return code;
        }

        protected (string Code, string Name)[] GetZippedCode (string resourceName)
        {
            var asm = GetType ().Assembly;
            var names = asm.GetManifestResourceNames ();
            var code = new List<(string, string)> ();
            using (var s = asm.GetManifestResourceStream ("Tests.Inputs." + resourceName)) {
                var a = new System.IO.Compression.ZipArchive (s, System.IO.Compression.ZipArchiveMode.Read);
                foreach (var e in a.Entries) {
                    if (Path.GetExtension (e.Name) == ".ll") {
                        string c;
                        using (var es = e.Open ()) {
                            using (var r = new StreamReader (es))
                                c = r.ReadToEnd ();
                        }
                        //System.Console.WriteLine (c.Length);
                        code.Add ((c, e.Name));
                    }
                }
            }
            return code.ToArray ();
        }

        protected static void AssertNoErrors (Compilation compilation)
        {
            Assert.IsFalse (compilation.HasErrors, "Errors Found:\n    " +
                            string.Join ("\n    ", compilation.Messages) +
                            "\n" + compilation.ErrorCount + " errors, " +
                            compilation.CompiledFunctionCount + " functions");
        }

        // clang -g -O1 -S -emit-llvm -fpic *.c

        protected string CToIR (string c)
        {
            var temp = Path.GetTempFileName ();
            var input = Path.ChangeExtension (temp, ".c");
            var output = Path.ChangeExtension (temp, ".ll");

            File.WriteAllText (input, c);
            var proc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "clang",
                    Arguments = $"-g -O1 -S -emit-llvm -fpic \"{input}\" -o \"{output}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    CreateNoWindow = true
                }
            };
            proc.Start ();
            try {
                proc.WaitForExit (60 * 1000);
                Assert.AreEqual (0, proc.ExitCode, "Clang error code");
                var r = File.ReadAllText (output);
                return r;
            }
            finally {
                File.Delete (temp);
                File.Delete (input);
                File.Delete (output);
            }
        }
    }
}
