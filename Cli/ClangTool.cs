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

            var args = new List<string> {
                "-g", "-O1", "-S", "-emit-llvm", "-fpic"
            };
            args.AddRange (files);

            var outFiles = new List<string> ();
            if (Run (buildDir, "clang", args.ToArray ()) == 0) {
                foreach (var f in files) {
                    var outName = Path.ChangeExtension (Path.GetFileName (f), ".ll");
                    var outPath = Path.Combine (buildDir, outName);
                    Console.WriteLine (outPath);
                    outFiles.Add (outPath);
                }
            }
            return outFiles;
        }
    }
}
