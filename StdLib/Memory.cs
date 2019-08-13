using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;

namespace StdLib
{
    [DllExport]
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

        //static List<MemoryBlock> registeredMemory = new List<MemoryBlock> ();

        //[DllExport]
        //unsafe class MemoryBlock
        //{
        //    public byte* Pointer;
        //    public long Length;
        //    public string Purpose;
        //}

        [DllExport ("@strncmp")]
        public unsafe static int strncmp (byte* s1, byte* s2, long n)
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

        [DllExport ("@_verify_read_pointer")]
        public unsafe static void VerifyReadPointer (byte* pointer)
        {
            Console.WriteLine ("READ " + (IntPtr)pointer);
        }

        [DllExport ("@_verify_write_pointer")]
        public unsafe static void VerifyWritePointer (byte* pointer)
        {
            Console.WriteLine ("WRITE " + (IntPtr)pointer);
        }

        [DllExport ("@_register_memory")]
        public unsafe static void RegisterMemory (byte* pointer, long size, string purpose)
        {
            // Save it in the list
            //lock (registeredMemory) {
            //    var n = registeredMemory.Count;
            //    int insertAt = 0;
            //    while (insertAt < n && pointer > (byte*)registeredMemory[insertAt].Pointer) {
            //        insertAt++;
            //    }
            //    registeredMemory.Insert (insertAt, new MemoryBlock {
            //        Pointer = pointer,
            //        Length = size,
            //        Purpose = purpose
            //    });
            //    Console.WriteLine ("REGISTER @" + insertAt + " " + (IntPtr)pointer + " length " + size + " for " + purpose);
            //}
            Console.WriteLine ("REGISTER " + (IntPtr)pointer + " length " + size + " for " + purpose);
        }

        [DllExport ("@_unregister_memory")]
        public unsafe static void UnregisterMemory (byte* pointer)
        {
            Console.WriteLine ("UNREGISTER " + (IntPtr)pointer);
        }

        [DllExport ("@malloc")]
        public unsafe static byte* malloc (long size)
        {
            var pointer = (byte*)Marshal.AllocHGlobal ((IntPtr)size);
            if (Safe) {
                RegisterMemory (pointer, size, "malloc");
            }
            return pointer;
        }

        [DllExport ("@free")]
        public unsafe static void free (void* pointer)
        {
            Marshal.FreeHGlobal ((IntPtr)pointer);
        }

        public static bool Safe = true;
    }

    public class DllExportAttribute : Attribute
    {
        public DllExportAttribute (string symbol)
        {
        }

        public DllExportAttribute ()
        {
        }
    }
}
