using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Cli
{
    public class ClangTool : CommandLineTool
    {
        public override HashSet<string> InputExtensions { get; } = new HashSet<string> {
            ".c",
            ".cpp",
            ".ino"
        };

        readonly HashSet<string> cppExtensions = new HashSet<string> {
            ".cpp",
        };

        public override IEnumerable<string> Run (ToolContext context)
        {
            var files = context.InputFiles.Select (Path.GetFullPath).ToList ();
            if (files.Count == 0)
                return Enumerable.Empty<string> ();

            var srcDir = Environment.CurrentDirectory;
            var buildDir = Path.GetTempPath ();

            var iswin = Environment.OSVersion.Platform == PlatformID.Win32NT;

            var outputAssembly = true;

            var args = new List<string> {
                "-g", "-O1", "-S", "-emit-llvm", "-frtti"
            };

            var argsHasStd = false;
            foreach (var e in context.ExtraArguments) {
                switch (e) {
                    case "-c":
                        outputAssembly = false;
                        args.Add ("-o");
                        args.Add (context.OutputFile);
                        break;
                    case var _ when e.StartsWith("-O"):
                        // Optimization is already set
                        break;
                    case var _ when e.StartsWith("-std"):
                        argsHasStd = true;
                        args.Add (e);
                        break;
                    case var _ when e.StartsWith ("-I"): {
                            var orig = e.Substring (2);
                            var fix = Path.GetFullPath (orig, srcDir);
                            args.Add ("-I" + fix);
                        }
                        break;
                    default:
                        args.Add (e);
                        break;
                }
            }

            if (!argsHasStd) {
                if (context.InputFiles.Any(x => cppExtensions.Contains (Path.GetExtension (x))))
                    args.Add ("-std=c++17");
                else
                    args.Add ("-std=c99");
            }

            if (!iswin)
                args.Add ("-fpic");

            var clangFileCount = 0;

            if (outputAssembly) {
                // If creating an assembly, only clang updated files
                foreach (var f in files) {
                    var outPath = GetOutFilePath (buildDir, f);
                    var srcInfo = new FileInfo (f);
                    var needsClang = true;
                    if (srcInfo.Exists) {
                        var destInfo = new FileInfo (outPath);
                        if (destInfo.Exists && destInfo.LastWriteTimeUtc > new FileInfo (f).LastWriteTimeUtc) {
                            needsClang = false;
                        }
                    }
                    if (needsClang) {
                        args.Add (f);
                        clangFileCount++;
                    }
                }
            }
            else {
                args.AddRange (files);
                clangFileCount = files.Count;
            }

            args.Insert (0, "-I" + Environment.CurrentDirectory);

            var outFiles = new List<string> ();

            var clangResult = clangFileCount > 0 ? Run (buildDir, "clang", args.ToArray ()) : 0;

            if (clangResult == 0) {
                if (outputAssembly) {
                    foreach (var f in files) {
                        var outPath = GetOutFilePath (buildDir, f);
                        //Console.WriteLine ($"OUTLL = {outPath}");
                        outFiles.Add (outPath);
                    }
                }
                else {
                }
            }

            return outFiles;
        }

        static string GetOutFilePath (string buildDir, string f)
        {
            var outName = Path.ChangeExtension (Path.GetFileName (f), ".ll");
            var outPath = Path.Combine (buildDir, outName);
            return outPath;
        }

        protected override string GetWindowsInstructions () =>
            @"To install clang, follow these steps:
    1. Install Chocolately from https://chocolatey.org/install
    2. Install LLVM by typing `choco install llvm`
    3. Test that clang is installed by typing `clang -v`";

        protected override string GetMacInstructions () =>
            @"To install clang, follow these steps:
    1. Install Xcode from the App Store
    2. Install LLVM by typing `xcode-select --install`
    3. Test that clang is installed by typing `clang -v`";
    }
}
