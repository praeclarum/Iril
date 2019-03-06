using System;
using System.IO;
using Repil;
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
                    else {
                        i++;
                    }
                }
                else {
                    files.Add (a);
                    i++;
                }
            }

            //
            // Cleanup input
            //
            if (string.IsNullOrWhiteSpace (outName)) {
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
                Console.WriteLine ("No inputs");
                return 1;
            }

            //
            // Parse
            //
            Console.WriteLine ($"Parsing {llfiles.Count} files...");
            var modules = llfiles.AsParallel ().Select (x => {
                var code = File.ReadAllText (x);
                return Module.Parse (code, x);
            });

            //
            // Compile
            //
            Console.WriteLine ("Compiling...");
            var comp = new Compilation (modules, outName);

            //
            // Output
            //
            Console.WriteLine ($"Writing {outName}...");
            comp.WriteAssembly (outName);
            return 0;
        }
    }
}
