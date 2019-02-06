using System.IO;
using NUnit.Framework;

namespace Tests
{
    public class TestsBase
    {
        protected string GetCode (string name)
        {
            var asm = GetType ().Assembly;
            var names = asm.GetManifestResourceNames ();
            string code;
            using (var s = asm.GetManifestResourceStream ("Tests.Inputs." + name)) {
                using (var r = new StreamReader (s)) {
                    code = r.ReadToEnd ();
                }
            }
            return code;
        }
    }
}
