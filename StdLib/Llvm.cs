using System;
using size_t = System.UInt64;
using System.Runtime.InteropServices;

namespace StdLib
{
    unsafe struct __va_list_tag
    {
        public int fp_offset;
        public int gp_offset;
        public byte* overflow_arg_area;
        public byte* reg_save_area;
    }

    public static class Llvm
    {
        [DllExport ("@llvm.va_start")]
        public unsafe static void va_start (byte* arglist, object[] arguments)
        {
            // Get required size
            var list = (__va_list_tag*)arglist;
            int gp_offset = 0;
            int fp_offset = 0;
            int overflow = 0;
            for (var i = 0; i < arguments.Length; i++) {
                var o = arguments[i];
                int num_gp;
                int num_fp;
                int size;
                if (o is int) {
                    num_gp = 1;
                    num_fp = 0;
                    size = 4;
                }
                else if (o is long) {
                    num_gp = 1;
                    num_fp = 0;
                    size = 8;
                }
                else if (o is IntPtr) {
                    num_gp = 1;
                    num_fp = 0;
                    size = sizeof(IntPtr);
                }
                else {
                    throw new Exception ($"Cannot pass {o} ({o?.GetType()}) as an argument");
                }
                if ((gp_offset > 48 - num_gp * 8) || (fp_offset > 304 - num_fp * 16)) {
                    overflow += size;
                }
                else {
                    gp_offset += num_gp * 8;
                    fp_offset += num_fp * 16;
                }
            }

            list->fp_offset = 0;
            list->gp_offset = fp_offset * 16;
            var overflowOffset = list->gp_offset + gp_offset * 8;
            list->reg_save_area = (byte*)Marshal.AllocHGlobal (overflowOffset + overflow);
            list->overflow_arg_area = list->reg_save_area + overflowOffset;

            gp_offset = list->gp_offset;
            fp_offset = list->fp_offset;
            var overflowPointer = list->overflow_arg_area;
            for (var i = 0; i < arguments.Length; i++) {
                var o = arguments[i];
                int num_gp;
                int num_fp;
                int size;
                if (o is int) {
                    num_gp = 1;
                    num_fp = 0;
                    size = 4;
                }
                else if (o is long) {
                    num_gp = 1;
                    num_fp = 0;
                    size = 8;
                }
                else if (o is IntPtr) {
                    num_gp = 1;
                    num_fp = 0;
                    size = sizeof(IntPtr);
                }
                else {
                    throw new Exception ($"Cannot pass {o} ({o?.GetType()}) as an argument");
                }
                if ((gp_offset > 48 - num_gp * 8) || (fp_offset > 304 - num_fp * 16)) {
                    if (o is int i32) {
                        *((int*)overflowPointer) = i32;
                    }
                    else if (o is long i64) {
                        *((long*)overflowPointer) = i64;
                    }
                    else if (o is IntPtr intptr) {
                        *((IntPtr*)overflowPointer) = intptr;
                    }
                    overflowPointer += size;
                }
                else {
                    if (o is int i32) {
                        *((int*)&list->reg_save_area[gp_offset]) = i32;
                    }
                    else if (o is long i64) {
                        *((long*)&list->reg_save_area[gp_offset]) = i64;
                    }
                    else if (o is IntPtr intptr) {
                        *((IntPtr*)&list->reg_save_area[gp_offset]) = intptr;
                    }
                    gp_offset += num_gp * 8;
                    fp_offset += num_fp * 16;
                }
            }
        }

        [DllExport ("@llvm.va_end")]
        public unsafe static void va_end (byte* arglist)
        {
            var list = (__va_list_tag*)arglist;
            Marshal.FreeHGlobal ((IntPtr)list->reg_save_area);
        }
    }
}
