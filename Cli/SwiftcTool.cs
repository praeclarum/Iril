using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Cli
{
    public class SwiftcTool : CommandLineTool
    {
        public override HashSet<string> InputExtensions { get; } = new HashSet<string> {
            ".swift",
        };

        public override IEnumerable<string> Run (ToolContext context)
        {
            var files = context.InputFiles.Select (Path.GetFullPath).ToList ();
            if (files.Count == 0)
                return Enumerable.Empty<string> ();

            var buildDir = Path.GetTempPath ();

            var args = new List<string> {
                "-emit-ir", "-g", "-Osize"
            };
            args.AddRange (files);

            var outFiles = new List<string> ();
            if (Run (buildDir, "swiftc", args.ToArray ()) == 0) {
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
