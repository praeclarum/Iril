using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;

namespace StdLib
{
    public static class Memory
    {
        static unsafe void* __memmove_chk (void* dest, void* src, size_t len, size_t destlen)
        {
            if (destlen < len)
                throw new Exception ("Buffer overlow");
            return memmove (dest, src, len);
        }

        static unsafe void* memmove (void* dest, void* src, size_t len)
        {
            byte* d = (byte*)dest;
            byte* s = (byte*)src;
            byte* r = d;
            if (s < d) {
                d += len;
                s += len;
                while (len-- != 0)
                    *--d = *--s;
            }
            else
                while (len-- != 0)
                    *d++ = *s++;
            return r;
        }

        static unsafe void* memchr (byte* s, int c, size_t n)
        {
            while (n-- != 0)
                if (*s++ == (byte)c)
                    return s - 1;
            return null;
        }

        static unsafe byte* __memset_chk (byte* dstpp, int c, size_t len, size_t dstlen)
        {
            if (dstlen < len)
                throw new Exception ("Buffer overlow");

            return memset (dstpp, c, len);
        }

        static unsafe byte* memset (byte* s, int c, size_t n)
        {
            byte* r = s, end = s + n;
            while (r < end)
                *r++ = (byte)c;
            return s;
        }

        static unsafe byte* __memcpy_chk (byte* dstpp, byte* srcpp, size_t len, size_t dstlen)
        {
            if (dstlen < len)
                throw new Exception ("Buffer overlow");

            return memcpy (dstpp, srcpp, len);
        }

        static unsafe byte* memcpy (byte* dst, byte* src, size_t n)
        {
            byte* ret = dst;
            while (n-- != 0)
                *dst++ = *src++;
            return ret;
        }

        static unsafe int memcmp (byte* s1, byte* s2, size_t n)
        {
            int ret = 0;
            while (n-- != 0 &&
                   (ret = *(byte*)s1++ - *(byte*)s2++) == 0)
                ;
            return ret;
        }

        public unsafe static byte* strchr (byte* s, int c)
        {
            byte* p = s;

            for (; ; ++p) {
                if (*p == c)
                    return (p);
                if (*p == '\0')
                    return null;
            }
        }

        unsafe static int strcmp (byte* s1, byte* s2)
        {
            while (*s1 == *s2++)
                if (*s1++ == '\0')
                    return (0);
            return (*(byte*)s1 - *(byte*)(s2 - 1));
        }

        [DllExport ("@strncmp")]
        unsafe static int strncmp (byte* s1, byte* s2, long n)
        {

            if (n == 0)
                return (0);
            do {
                if (*s1 != *s2++)
                    return (*(byte*)s1 -
                        *(byte*)(s2 - 1));
                if (*s1++ == '\0')
                    break;
            } while (--n != 0);
            return (0);
        }
    }

    public class DllExportAttribute : Attribute
    {
        public DllExportAttribute (string symbol)
        {
        }
    }
}
