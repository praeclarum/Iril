using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using Repil;
using System.Text.RegularExpressions;
using System.Linq;
using System;

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
                    if (Path.GetExtension (e.Name) == ".ll" && !e.Name.StartsWith (".", System.StringComparison.Ordinal)) {
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

        static readonly Regex[] regices = {
            //False positive, IL Spy doesn't handle pointer args right
            //new Regex (@"//\s*IL_[a-zA-Z0-9]+: Expected.*\n"),

            new Regex (@"/\*\s*Error.*?\*/"),
        };

        protected static void CheckDisassemblyForErrors (string asmPath)
        {
            var cmd = Path.Combine (Environment.GetEnvironmentVariable ("HOME"), ".dotnet", "tools", "ilspycmd");
            var disProc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = cmd,
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

            var errorsq =
                from r in regices
                from m in r.Matches (disAsm).Cast<Match> ()
                select m.Value.Trim();

            var errors = errorsq.ToList ();
            Assert.AreEqual (0, errors.Count,
                errors.Count + " Errors:\n" +
                string.Join ("\n", errors.Take (200)));
        }
    }
}
