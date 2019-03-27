using System;
using System.IO;
using System.Text;

namespace Keygen
{
    class Program
    {
        // dotnet run -- "Alex Blount|0|m"

        static int Main(string[] args)
        {
            var p = ".irilic";
            var header = args[0];
            var r = header.Trim ().Split (new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (r.Length < 3)
                return 3;
            var k = "Thanks for buying Iril! " + header.Trim ();
            var n = r[0];
            for (var i = 0; i < n.Length; i += 2) {
                k += n[i];
            }
            k += r[2];
            var crypt = new System.Security.Cryptography.SHA256Managed ();
            var hash = "";
            var crypto = crypt.ComputeHash (Encoding.UTF8.GetBytes (k));
            foreach (byte theByte in crypto) {
                hash += theByte.ToString ("x2");
            }
            File.WriteAllLines (p, new[] { header, hash });
            return 0;
        }
    }
}
