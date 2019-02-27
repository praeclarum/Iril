using System;
using System.IO;
using Repil;
using System.Linq;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var files =
                from f in Directory.GetFiles ("/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite", "*.ll")
                let code = File.ReadAllText (f)
                let m = Module.Parse (code, f)
                select m;

            var comp = new Compilation (files, "SuiteSparse.dll");

            comp.WriteAssembly ("SuiteSparse.dll");
        }
    }
}
