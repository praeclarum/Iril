using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;

namespace StdLib
{
    [DllExport]
    public static class LibC
    {
        [DllExport ("@bcmp")]
        public static unsafe int bcmp (void *b1, void *b2, size_t length)
        {
            byte* p1, p2;

            if (length == 0)
                return(0);
            p1 = (byte *)b1;
            p2 = (byte *)b2;
            do
                if (*p1++ != *p2++)
                    break;
            while (--length != 0);
            return (int)(length);
        }

        [DllExport ("@\"\\01_clock\"")]
        public static long _01_clock ()
        {
            throw new NotSupportedException ();
        }

        [DllExport ("@exit")]
        public static void exit (int status)
        {
            Environment.Exit (status);
        }

        [DllExport ("@iswspace")]
        public static unsafe int iswspace (int c)
        {
            // _CTYPE_S	0x00004000L		/* Space */
            return char.IsWhiteSpace ((char)c) ? 1 : 0;
        }

        [DllExport ("@__maskrune")]
        public static unsafe int maskrune (int c, long f)
        {
            // https://opensource.apple.com/source/Libc/Libc-1044.1.2/include/ctype.h.auto.html
            var cat = char.GetUnicodeCategory ((char)c);
            throw new NotSupportedException ();
        }
    }
}
