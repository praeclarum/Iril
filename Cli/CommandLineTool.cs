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
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    WorkingDirectory = dir,
                }
            };
            try {
                //Console.WriteLine ($"{proc.StartInfo.FileName} {proc.StartInfo.Arguments}");
                proc.Start ();
                proc.WaitForExit (5 * 60 * 1000);
                //Console.WriteLine ($"== {proc.ExitCode}");
                if (proc.ExitCode != 0) {
                    Program.Error ($"`{proc.StartInfo.FileName} {proc.StartInfo.Arguments}` failed: {proc.ExitCode}");
                }
                return proc.ExitCode;
            }
            catch (Win32Exception wex) when (wex.ErrorCode == unchecked ((int)0x80004005u)) {
                Program.Error ($"Could not find the tool `{fileName}`");
                var installInstructions = Environment.OSVersion.Platform == PlatformID.Win32NT ?
                    GetWindowsInstructions () : GetMacInstructions ();
                if (!string.IsNullOrEmpty (installInstructions)) {
                    Program.Error (installInstructions);
                }
                return 126;
            }
            catch (Exception ex) {
                Program.Error (ex.ToString ());
                return 127;
            }
        }

        protected virtual string GetWindowsInstructions () => "";
        protected virtual string GetMacInstructions () => "";

        static string EscapeArg (string arg)
        {
            if (arg.All (x => char.IsLetterOrDigit (x) || ('0' <= x && x < 127) || x == '-'))
                return arg;
            return $"\"{arg}\"";
        }
    }
}
