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
            // Start loading in the background
            //
            Library.LoadStandardLibrariesAsync ();

            //
            // Inputs
            //
            var files = new List<string> ();
            var extraArgs = new List<string> ();
            var outName = "";
            var safeMemory = false;
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
                            i += 2;
                        }
                        else {
                            i++;
                        }
                    }
                    else if (a == "-h" || a == "--help" || a == "-?") {
                        showHelp = true;
                        i++;
                    }
                    else if (a == "-v" || a == "--version") {
                        showVersion = true;
                        i++;
                    }
                    else if (a == "--safe-memory") {
                        safeMemory = true;
                        i++;
                    }
                    else {
                        extraArgs.Add (a);
                        i++;
                    }
                }
                else {
                    files.Add (a);
                    i++;
                }
            }

            if (showVersion) {
                var version = typeof (Program).Assembly.GetName ().Version;
                Console.WriteLine ($"Krueger Systems IRIL {version}");
                if (!showHelp)
                    return 0;
            }

            if (showHelp) {
                Console.WriteLine ($"OVERVIEW: C/C++ to .NET assembly compiler by Frank A. Krueger");
                Console.WriteLine ();
                Console.WriteLine ($"USAGE: iril [options] <inputs>");
                Console.WriteLine ();
                Console.WriteLine ($"INPUTS: .c and .ll files");
                Console.WriteLine ();
                Console.WriteLine ($"OPTIONS:");
                Console.WriteLine ($"  -o <asm file>      Path to the assembly .dll to output");
                Console.WriteLine ($"  -h, -?, --help     Display this help");
                Console.WriteLine ($"  -v, --version      Display the version");
                Console.WriteLine ($"  --safe-memory      Verify memory accesses to make code safe from crashes");
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
            var context = new ToolContext {
                InputFiles = cfiles.ToArray (),
                ExtraArguments = extraArgs.ToArray (),
                OutputFile = outName,
            };
            var cllfiles = clang.Run (context);

            var llfiles = (from f in files
                           let e = Path.GetExtension (f)
                           where e == ".ll" || e == ".o"
                           select f)
                          .Concat (cllfiles)
                          .ToList ();

            //
            // Early out
            //
            if (llfiles.Count == 0) {
                if (context.ExtraArguments.Contains ("-c"))
                    return 0;
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
                }).ToList ();

                //
                // Compile
                //
                Info ("Compiling...");
                var comp = new Compilation (new CompilationOptions (modules, outName, safeMemory: safeMemory));
                comp.Compile ();

                //
                // Show errors
                //
                var errors = (from m in modules from e in m.Errors select e)
                    .Concat (comp.Messages)
                    .OrderBy (x => x.FilePath).ThenBy (x => x.Text)
                    .ToList ();
                foreach (var e in errors) {
                    Console.Write ("iril: ");
                    if (e.Type == MessageType.Error) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write ("error: ");
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write ("warning: ");
                    }
                    Console.ResetColor ();

                    if (!string.IsNullOrEmpty (e.FilePath)) {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write (e.FilePath);
                        Console.Write (": ");
                        Console.ResetColor ();
                    }

                    Console.WriteLine (e.Text);

                    if (!string.IsNullOrEmpty (e.Surrounding)) {
                        Console.WriteLine (e.Surrounding);
                    }
#if DEBUG
                    if (e.Exception != null) {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine (e.Exception);
                        Console.ResetColor ();
                    }
#endif
                }

                //
                // Output
                //
                Info ($"Writing {outName}...");
                comp.WriteAssembly (outName);

                if (errors.Count > 0) {
                    Info ($"{errors.Count(x => x.Type == MessageType.Error)} errors, {errors.Count (x => x.Type == MessageType.Warning)} warnings");
                }

                return modules.Any (m => m.HasErrors) ? 3 : 0;
            }
            catch (Exception ex) {
                Error (ex.ToString ());
                return 2;
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
