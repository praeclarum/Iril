using System;
namespace StdLib
{
    [DllExport]
    public static class IO
    {
        [DllExport ("@__snprintf_chk")]
        public static unsafe int snprintf_chk (byte* s, long maxlen, int flags, long len, byte* format, params object[] arguments)
        {
            // var args = stackalloc __va_list_tag[1];
            // Llvm.va_start ((byte*)args, arguments);
            // Llvm.va_end ((byte*)args);
            throw new NotImplementedException ();
        }

        [DllExport ("@snprintf")]
        public static unsafe int snprintf (System.Byte* str, System.Int64 size, System.Byte* format, params System.Object[] arguments)
        {
            // var args = stackalloc __va_list_tag[1];
            // Llvm.va_start ((byte*)args, arguments);
            // Llvm.va_end ((byte*)args);
            throw new NotImplementedException ();
        }
    }
}
