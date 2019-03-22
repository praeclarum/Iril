using System;
using System.IO;
using Iril;
using System.Linq;
using System.Collections.Generic;

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
                    else {
                        i++;
                    }
                }
                else {
                    files.Add (a);
                    i++;
                }
            }

            if (showVersion) {
                var version = typeof (Iril.Module).Assembly.GetName ().Version;
                Console.WriteLine ($"Krueger Systems IRIL {version}");
                if (!showHelp)
                    return 0;
            }

            if (showHelp) {
                Console.WriteLine ($"OVERVIEW: C to .NET assembly compiler by Frank A. Krueger");
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
    }
}
