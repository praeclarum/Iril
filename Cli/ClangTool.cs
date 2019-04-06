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

        public override IEnumerable<string> Run (IEnumerable<string> inputFiles)
        {
            var files = inputFiles.Select (Path.GetFullPath).ToList ();
            if (files.Count == 0)
                return Enumerable.Empty<string> ();

            var buildDir = Path.GetTempPath ();

            var iswin = Environment.OSVersion.Platform == PlatformID.Win32NT;

            var args = new List<string> {
                "-g", "-O1", "-S", "-emit-llvm", "-std=c++17", "-frtti"
            };
            if (!iswin)
                args.Add ("-fpic");
            args.AddRange (files);

            var outFiles = new List<string> ();
            if (Run (buildDir, "clang", args.ToArray ()) == 0) {
                foreach (var f in files) {
                    var outName = Path.ChangeExtension (Path.GetFileName (f), ".ll");
                    var outPath = Path.Combine (buildDir, outName);
                    //Console.WriteLine (outPath);
                    outFiles.Add (outPath);
                }
            }
            return outFiles;
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
