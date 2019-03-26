using System;
using System.IO;
using Iril;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            //
            // Inputs
            //
            var files = new List<string> ();
            var outName = "";
            var showHelp = false;
            var showVersion = false;
            var r = GetDateTime ();

            //
            // Parse command line
            //
            for (int i = 0; i < args.Length;) {
                var a = args[i];
                if (a[0] == '-') {
                    if (a == "-o") {
                        if (i + 1 < args.Length) {
                            outName = args[i + 1];
                        }
                        i += 2;
                    }
                    if (a == "-h" || a == "--help" || a == "-?") {
                        showHelp = true;
                        i++;
                    }
                    else if (a == "-v" || a == "--version") {
                        showVersion = true;
                        i++;
                    }
                    else if (a == "--trial") {
                        r = null;
                        i++;
                    }
                    else {
                        i++;
                    }
                }
                else {
                    files.Add (a);
                    i++;
                }
            }

            int maxFuncs = r != null ? int.MaxValue : 10;
            if (r == null) {
                Console.WriteLine ($"IRIL TRIAL VERSION");
                Console.WriteLine ();
                Console.WriteLine ($"Thanks for trying out Iril. This version is limited to compiling {maxFuncs} functions.");
                Console.WriteLine ();
                //Console.WriteLine ($"If you would like to purchase Iril to unlock all its features, send an email to fak@kruegersystems.com");
                //Console.WriteLine ();
            }

            if (showVersion) {
                var version = typeof (Iril.Module).Assembly.GetName ().Version;
                Console.WriteLine ($"Krueger Systems IRIL {version}");
                if (!showHelp)
                    return 0;
            }

            if (showHelp) {
                Console.WriteLine ($"OVERVIEW: C to .NET assembly compiler by Frank A. Krueger");
                if (r != null) {
                    Console.WriteLine ();
                    Console.WriteLine ($"REGISTERED TO: {r}");
                }
                Console.WriteLine ();
                Console.WriteLine ($"USAGE: iril [options] <inputs>");
                Console.WriteLine ();
                Console.WriteLine ($"INPUTS: .c and .ll files");
                Console.WriteLine ();
                Console.WriteLine ($"OPTIONS:");
                Console.WriteLine ($"  -o <asm file>      Path to the assembly .dll to output");
                Console.WriteLine ($"  -h, -?, --help     Display this help");
                Console.WriteLine ($"  -v, --version      Display the version");
                return 0;
            }

            //
            // Cleanup input
            //
            if (string.IsNullOrWhiteSpace (outName) && files.Count > 0) {
                outName = Path.ChangeExtension (Path.GetFileName (files[0]), ".dll");
            }

            //
            // Compile C Files
            //
            var clang = new ClangTool ();
            var cfiles = files.Where (x => clang.InputExtensions.Contains (Path.GetExtension (x)));
            var cllfiles = clang.Run (cfiles);

            var llfiles = files
                          .Where (x => Path.GetExtension (x) == ".ll")
                          .Concat (cllfiles)
                          .ToList ();

            //
            // Early out
            //
            if (llfiles.Count == 0) {
                Error ("No inputs");
                return 1;
            }

            try {

                //
                // Parse
                //
                Info ($"Parsing {llfiles.Count} files...");
                var modules = llfiles.AsParallel ().Select (x => {
                    var code = File.ReadAllText (x);
                    return Module.Parse (code, x);
                });

                //
                // Compile
                //
                Info ("Compiling...");
                var comp = new Compilation (modules, outName);
                comp.MaxFunctions = maxFuncs;
                comp.Compile ();

                //
                // Output
                //
                Info ($"Writing {outName}...");
                comp.WriteAssembly (outName);
                return 0;
            }
            catch (Exception ex) {
                Error (ex.ToString ());
                return 1;
            }
        }

        public static void Info (string message)
        {
            Console.Write ("iril: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write ("info: ");
            Console.ResetColor ();
            Console.WriteLine (message);
        }

        public static void Error (string message)
        {
            Console.Write ("iril: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write ("error: ");
            Console.ResetColor ();
            Console.WriteLine (message);
        }

        static string GetDateTime ()
        {
            try {
                var p = Path.Combine (Environment.GetEnvironmentVariable ("HOME"), ".irilic");
                if (!File.Exists (p))
                    return null;
                var lines = File.ReadAllLines (p);
                if (lines.Length < 2)
                    return null;
                var r = lines[0].Trim().Split (new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (r.Length < 3)
                    return null;
                var k = "Thanks for buying Iril! " + lines[0].Trim();
                var n = r[0];
                for (var i = 0; i < n.Length; i += 2) {
                    k += n[i];
                }
                k += r[2];
                var crypt = new System.Security.Cryptography.SHA256Managed ();
                var hash = "";
                var crypto = crypt.ComputeHash (Encoding.UTF8.GetBytes (k));
                foreach (byte theByte in crypto) {
                    hash += theByte.ToString ("x2");
                }
                if (hash != lines[1].Trim())
                    return null;
                return r[0];
            }
            catch {
                return null;
            }
        }
    }
}
