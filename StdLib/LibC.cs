using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;

namespace StdLib
{
    [DllExport]
    public static class LibC
    {
        [DllExport ("@bcmp")]
        public static unsafe int bcmp(void *b1, void *b2, size_t length)
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
    }
}
