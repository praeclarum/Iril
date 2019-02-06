using System;
using System.IO;
using Repil;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Module.Parse ("declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1");
        }
    }
}
