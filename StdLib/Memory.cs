#nullable enable

using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;

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

        static ArrayList registeredMemory = new ArrayList ();

        //static unsafe readonly MemoryBlock regHead = new MemoryBlock (null, 0, "Root of All Memory");

        [DllExport]
        unsafe class MemoryBlock
        {
            public byte* Pointer;
            public long Length;
            public string Purpose;

            //public MemoryBlock? Less;
            //public MemoryBlock? Greater;

            public MemoryBlock (byte* pointer, long length, string purpose)
            {
                Pointer = pointer;
                Length = length;
                Purpose = purpose;
            }
        }

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
            lock (registeredMemory) {
                var n = registeredMemory.Count;
                int insertAt = 0;
                while (insertAt < n && pointer >= ((MemoryBlock)registeredMemory[insertAt]).Pointer) {
                    insertAt++;
                }
                var i = insertAt - 1;
                if (0 <= i && i < registeredMemory.Count) {
                    var b = (MemoryBlock)registeredMemory[i];
                    var offset = pointer - b.Pointer;
                    if (offset > b.Length) {
                        throw new Exception ($"Read Access Violation 0x{(ulong)pointer:x}. Address outside the range of block @{i}/{registeredMemory.Count} {b.Purpose}[{(IntPtr)offset}/{b.Length}]");
                    }
                    //Console.WriteLine ($"READ 0x{(ulong)pointer:x} block @{i}/{registeredMemory.Count} {b.Purpose}[{(IntPtr)offset}/{b.Length}]");
                }
                else {
                    throw new Exception ($"Read Access Violation 0x{(ulong)pointer:x}. Could not find allocated block @{i}/{registeredMemory.Count}");
                }
            }

            //Console.WriteLine ("READ " + (IntPtr)pointer);
        }

        [DllExport ("@_verify_write_pointer")]
        public unsafe static void VerifyWritePointer (byte* pointer)
        {
            lock (registeredMemory) {
                var n = registeredMemory.Count;
                int insertAt = 0;
                while (insertAt < n && pointer >= ((MemoryBlock)registeredMemory[insertAt]).Pointer) {
                    insertAt++;
                }
                var i = insertAt - 1;
                if (0 <= i && i < registeredMemory.Count) {
                    var b = (MemoryBlock)registeredMemory[i];
                    var offset = pointer - b.Pointer;
                    if (offset > b.Length) {
                        throw new Exception ($"Write Access Violation 0x{(ulong)pointer:x}. Address outside the range of block @{i}/{registeredMemory.Count} {b.Purpose}[{(IntPtr)offset}/{b.Length}]");
                    }
                    //Console.WriteLine ($"WRITE 0x{(ulong)pointer:x} block @{i}/{registeredMemory.Count} {b.Purpose}[{(IntPtr)offset}/{b.Length}]");
                }
                else {
                    throw new Exception ($"Write Access Violation 0x{(ulong)pointer:x}. Could not find allocated block @{i}/{registeredMemory.Count}");
                }
            }
        }

        [DllExport ("@_register_memory")]
        public unsafe static void RegisterMemory (byte* pointer, long size, string purpose)
        {
            //var b = new MemoryBlock (pointer, size, purpose);

            // Save it in the list
            //lock (regHead) {
            //    var p = regHead;

            //    if (p.Pointer == pointer) {
            //        throw new Exception ($"Reallocated memory at {new IntPtr(pointer)}");
            //    }
            //    else if (pointer > p.Pointer) {
            //        if (p.Greater == null) {

            //        }
            //    }
            //}

            lock (registeredMemory) {
                var n = registeredMemory.Count;
                int insertAt = 0;
                while (insertAt < n && pointer >= ((MemoryBlock)registeredMemory[insertAt]).Pointer) {
                    insertAt++;
                }
                registeredMemory.Insert (insertAt, new MemoryBlock (pointer, size, purpose));
                Console.WriteLine ($"REGISTER @{insertAt} 0x{(ulong)pointer:x} length {size} for {purpose}");
            }
            //Console.WriteLine ("REGISTER " + (IntPtr)pointer + " length " + size + " for " + purpose);
        }

        [DllExport ("@_unregister_memory")]
        public unsafe static void UnregisterMemory (byte* pointer)
        {
            if (pointer == null)
                return;
            lock (registeredMemory) {
                var n = registeredMemory.Count;
                int insertAt = 0;
                for (var i = 0; i < n; i++) {
                    var b = ((MemoryBlock)registeredMemory[i]);
                    if (b.Pointer == pointer) {
                        Console.WriteLine ($"UNREGISTER @{insertAt} 0x{(ulong)pointer:x} length {b.Length} for {b.Purpose}");
                        registeredMemory.RemoveAt (i);
                        return;
                    }
                }
                throw new Exception ($"Free Access Violation. No block for 0x{(ulong)pointer:x}");
            }
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
        public unsafe static void free (byte* pointer)
        {
            Marshal.FreeHGlobal ((IntPtr)pointer);
            if (Safe) {
                UnregisterMemory (pointer);
            }
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
