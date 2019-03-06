using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;

namespace Cli
{
    public abstract class CommandLineTool
    {
        public abstract HashSet<string> InputExtensions { get; }
        public abstract IEnumerable<string> Run (IEnumerable<string> inputFiles);

        protected int Run (string dir, string fileName, params string[] arguments)
        {
            var proc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = fileName,
                    Arguments = string.Join (" ", arguments.Select (EscapeArg)),
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    CreateNoWindow = true,
                    WorkingDirectory = dir,
                }
            };
            try {
                proc.Start ();
                proc.WaitForExit (5 * 60 * 1000);
                return proc.ExitCode;
            }
            catch (Win32Exception wex) when (wex.ErrorCode == unchecked ((int)0x80004005u)) {
                Console.WriteLine ($"Could not find the tool `{fileName}`");
                return 126;
            }
            catch (Exception ex) {
                Console.WriteLine (ex);
                return 127;
            }
        }

        static string EscapeArg (string arg)
        {
            if (arg.All (x => char.IsLetterOrDigit (x) || ('0' <= x && x < 127)))
                return arg;
            return $"\"{arg}\"";
        }
    }
}