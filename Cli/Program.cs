using System;
using System.IO;
using Repil.IR;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var a in args) {
                var m = Module.Parse (File.ReadAllText (a));
                Console.WriteLine (m);
            }
        }
    }
}
