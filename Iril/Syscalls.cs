using System;
using Mono.Cecil;
using System.Collections.Generic;
using Iril.Types;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System.Linq;

namespace Iril
{
    public class Syscalls
    {
        readonly Compilation compilation;
        readonly TypeDefinition syscallsType;
        readonly Module module = new Module ();

        public readonly SymbolTable<MethodDefinition> Calls =
            new SymbolTable<MethodDefinition> ();

        public Syscalls (Compilation compilation, TypeDefinition syscallsType)
        {
            this.compilation = compilation;
            this.syscallsType = syscallsType;
        }

        public void Emit ()
        {
            EmitStandardStreams ();

            EmitAllocateException ();
            EmitAssertRtn ();
            EmitCalloc ();
            EmitDelete ();
            EmitFree ();
            EmitFreeException ();
            EmitMalloc ();
            EmitNew ();
            EmitPersonality ();
            EmitThrow ();
            EmitVFPrintf ();
            EmitPrintf ();
            EmitFPrintf ();
            EmitVPrintf ();
            EmitPutchar ();
            EmitFputc ();
            EmitPuts ();
            EmitFPuts ();
            EmitFWrite ();
            EmitUnixRead ("@\"\\01_read\"");
            EmitUnixWrite ("@\"\\01_write\"");
            EmitRealloc ();
            EmitMemsetPattern (4);
            EmitMemsetPattern (8);
            EmitMemsetPattern (16);
            EmitStrlen ();
            EmitStrchr ();
            EmitStrcmp ();
            EmitStrncmp ();
            EmitMemchr ();
            EmitMemcmp ();
            EmitMemcpy ();
            EmitMemcpyChecked ();
            EmitMemmove ();
            EmitMemmoveChecked ();
            EmitMemset ();
            EmitMemsetChecked ();
            EmitUMulOvf64 ();
            EmitSetjmp ();
            EmitLongjmp ();
            EmitAbort ();
            EmitVAStart ();
            EmitVAEnd ();

            EmitStaticCtor ();
        }

        static readonly string[] printfDeps = { "@vfprintf" };
        static readonly string[] memcpyChkDeps = { "@memcpy" };
        static readonly string[] memmoveChkDeps = { "@memmove" };
        static readonly string[] memsetChkDeps = { "@memset" };

        public string[] GetDependencies (Symbol symbol)
        {
            switch (symbol.Text) {
                case "@printf":
                    return printfDeps;
                case "@vprintf":
                    return printfDeps;
                case "@__memcpy_chk":
                    return memcpyChkDeps;
                case "@__memmove_chk":
                    return memmoveChkDeps;
                case "@__memset_chk":
                    return memsetChkDeps;
                default:
                    return Array.Empty<string> ();
            }
        }

        MethodDefinition NewMethod (Symbol symbol, LType returnType, params (string, LType)[] parameters)
        {
            return NewMethod (symbol,
                compilation.GetClrType (returnType, module),
                parameters.Select (x => (x.Item1, compilation.GetClrType (x.Item2, module))).ToArray ());
        }

        MethodDefinition NewMethod (Symbol symbol, TypeReference returnType, params (string, TypeReference)[] parameters)
        {
            var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static;
            var md = new MethodDefinition (
                new IR.MangledName (symbol).Identifier,
                mattrs,
                returnType);
            for (var i = 0; i < parameters.Length; i++) {
                var p = parameters[i];
                var pd = new ParameterDefinition (p.Item1, ParameterAttributes.None, p.Item2);
                if (i == parameters.Length - 1 && p.Item2.IsArray && p.Item2.GetElementType().FullName == "System.Object") {
                    pd.CustomAttributes.Add (new CustomAttribute (compilation.sysParamsAttrCtor));
                }
                md.Parameters.Add (pd);
            }
            var body = new MethodBody (md);
            syscallsType.Methods.Add (md);
            Calls[symbol] = md;
            return md;
        }

        void EmitAllocateException ()
        {
            var m = NewMethod ("@__cxa_allocate_exception", Types.PointerType.I8Pointer, ("size", IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromInt64));
            il.Append (il.Create (OpCodes.Call, compilation.sysAllocHGlobal));
            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitAssertRtn ()
        {
            var m = NewMethod (
                "@__assert_rtn", Types.VoidType.Void,
                ("function", Types.PointerType.I8Pointer),
                ("file", Types.PointerType.I8Pointer),
                ("line", Types.IntegerType.I32),
                ("expression", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "Assert failed"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            b.Optimize ();
        }

        void EmitCalloc ()
        {
            var m = NewMethod ("@calloc", Types.PointerType.I8Pointer, ("count", IntegerType.I64), ("size", IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();
            b.Variables.Add (new VariableDefinition (compilation.sysUInt32));
            b.Variables.Add (new VariableDefinition (compilation.sysIntPtr));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Conv_U4));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Conv_U4));
            il.Append (il.Create (OpCodes.Mul));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Conv_U8));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromInt64));
            il.Append (il.Create (OpCodes.Call, compilation.sysAllocHGlobal));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U1));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Initblk));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitMalloc ()
        {
            var m = NewMethod ("@malloc", Types.PointerType.I8Pointer, ("size", IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromInt64));
            il.Append (il.Create (OpCodes.Call, compilation.sysAllocHGlobal));

            //il.Append (il.Create (OpCodes.Dup));
            //il.Append (il.Create (OpCodes.Ldc_I4_0));
            //il.Append (il.Create (OpCodes.Conv_U1));
            //il.Append (il.Create (OpCodes.Ldarg_0));
            //il.Append (il.Create (OpCodes.Conv_U4));
            //il.Append (il.Create (OpCodes.Initblk));

            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitNew ()
        {
            var m = NewMethod ("@_Znwm", Types.PointerType.I8Pointer, ("size", IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromInt64));
            il.Append (il.Create (OpCodes.Call, compilation.sysAllocHGlobal));
            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitPersonality ()
        {
            var m = NewMethod ("@__gxx_personality_v0", Types.IntegerType.I32, ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitThrow ()
        {
            var m = NewMethod ("@__cxa_throw", Types.VoidType.Void, ("p0", Types.PointerType.I8Pointer), ("p1", Types.PointerType.I8Pointer), ("p2", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldstr, "C++ Exception"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));
            b.Optimize ();
        }

        void EmitVFPrintf ()
        {
            var m = NewMethod ("@vfprintf", Types.IntegerType.I32, ("stream", Types.PointerType.I8Pointer), ("format", Types.PointerType.I8Pointer), ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
            var p = new VariableDefinition (compilation.GetClrType (Types.PointerType.I8Pointer, module: this.module));
            var i = new VariableDefinition (compilation.sysInt32);
            b.Variables.Add (p);
            b.Variables.Add (i);

            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Stloc, p));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Stloc_1));
            var checkDone = il.Create (OpCodes.Ldloc, p);
            il.Append (il.Create (OpCodes.Br, checkDone));

            var loop = il.Create (OpCodes.Ldloc, p);
            il.Append (loop);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldc_I4, 37));
            var normalChar = il.Create (OpCodes.Ldloc, p);
            il.Append (il.Create (OpCodes.Bne_Un, normalChar));

            // %
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Ldloc, i));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc, i));
            il.Append (il.Create (OpCodes.Ldelem_Ref));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleWriteObj));
            il.Append (il.Create (OpCodes.Ldloc, p));
            il.Append (il.Create (OpCodes.Ldc_I4_2));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc, p));
            il.Append (il.Create (OpCodes.Br, checkDone));

            il.Append (normalChar);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleWriteChar));
            il.Append (il.Create (OpCodes.Ldloc, p));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc, p));

            il.Append (checkDone);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brtrue, loop));

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitPrintf ()
        {
            var m = NewMethod ("@printf", Types.IntegerType.I32, ("format", Types.PointerType.I8Pointer), ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Call, Calls["@vfprintf"]));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitFPrintf ()
        {
            var m = NewMethod ("@fprintf", Types.IntegerType.I32, ("file", Types.PointerType.I8Pointer), ("format", Types.PointerType.I8Pointer), ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Call, Calls["@vfprintf"]));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitVPrintf ()
        {
            var m = NewMethod ("@vprintf", Types.IntegerType.I32, ("format", Types.PointerType.I8Pointer), ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Call, Calls["@vfprintf"]));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitPutchar ()
        {
            var m = NewMethod ("@putchar", Types.VoidType.Void, ("c", Types.IntegerType.I8));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleWriteChar));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitPuts ()
        {
            var m = NewMethod ("@puts", Types.IntegerType.I32, ("s", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Call, compilation.sysPtrToStringAuto));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitRealloc ()
        {
            var m = NewMethod ("@realloc", Types.PointerType.I8Pointer, ("ptr", Types.PointerType.I8Pointer), ("size", IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromInt64));
            il.Append (il.Create (OpCodes.Call, compilation.sysReAllocHGlobal));
            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitDelete ()
        {
            var m = NewMethod ("@_ZdlPv", Types.VoidType.Void, ("ptr", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Call, compilation.sysFreeHGlobal));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitFree ()
        {
            var m = NewMethod ("@free", Types.VoidType.Void, ("ptr", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Call, compilation.sysFreeHGlobal));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitFreeException ()
        {
            var m = NewMethod ("@__cxa_free_exception", Types.VoidType.Void, ("ptr", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Call, compilation.sysFreeHGlobal));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitMemsetPattern (int patternLength)
        {
            var m = NewMethod (
                "@memset_pattern" + patternLength, Types.VoidType.Void,
                ("b", Types.PointerType.I8Pointer),
                ("pattern" + patternLength, Types.PointerType.I8Pointer),
                ("len", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            b.Variables.Add (new VariableDefinition (compilation.GetClrType (Types.PointerType.I8Pointer, module: module)));
            b.Variables.Add (new VariableDefinition (compilation.sysUInt32));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_U4));
            il.Append (il.Create (OpCodes.Stloc_1));
            var loopCheck = il.Create (OpCodes.Ldloc_1);
            il.Append (il.Create (OpCodes.Br, loopCheck));

            var loop = il.Create (OpCodes.Ldloc_0);
            il.Append (loop);
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldc_I4, patternLength));
            il.Append (il.Create (OpCodes.Cpblk));

            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Ldc_I4, patternLength));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4, patternLength));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Stloc_1));

            il.Append (loopCheck);
            il.Append (il.Create (OpCodes.Ldc_I4, patternLength));
            il.Append (il.Create (OpCodes.Bge_Un, loop));

            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            var ret = il.Create (OpCodes.Ret);
            il.Append (il.Create (OpCodes.Ble_Un, ret));

            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Cpblk));

            il.Append (ret);

            b.Optimize ();
        }

        void EmitStrlen ()
        {
            var m = NewMethod ("@strlen", Types.IntegerType.I64, ("str", Types.PointerType.I8Pointer));
            var v = new VariableDefinition (compilation.sysBytePtr);
            var b = m.Body;
            b.Variables.Add (v);
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            var check = il.Create (OpCodes.Ldloc, v);
            il.Append (il.Create (OpCodes.Br, check));

            var loop = il.Create (OpCodes.Ldloc, v);
            il.Append (loop);
            il.Append (il.Create (OpCodes.Ldc_I4, 1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc, v));
            il.Append (check);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brtrue, loop));

            il.Append (il.Create (OpCodes.Ldloc, v));
            il.Append (il.Create (OpCodes.Ldarg, m.Parameters[0]));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ldc_I4, 1));
            il.Append (il.Create (OpCodes.Div));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemmoveChecked ()
        {
            var m = NewMethod ("@__memmove_chk", Types.PointerType.I8Pointer,
                               ("dest", Types.PointerType.I8Pointer),
                               ("src", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64),
                               ("destlen", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var ilf = il.Create (OpCodes.Ldarg_0);
            il.Append (il.Create (OpCodes.Ldarg_3));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Bge_Un, ilf));
            il.Append (il.Create (OpCodes.Ldstr, "Buffer overflow"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            il.Append (ilf);
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Call, Calls["@memmove"]));

            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemmove ()
        {
            var m = NewMethod ("@memmove", Types.PointerType.I8Pointer,
                               ("dest", Types.PointerType.I8Pointer),
                               ("src", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysBytePtr);
            var v1 = new VariableDefinition (compilation.sysBytePtr);
            var v2 = new VariableDefinition (compilation.sysBytePtr);
            var b = m.Body;
            b.Variables.Add (v0);
            b.Variables.Add (v1);
            b.Variables.Add (v2);
            var il = b.GetILProcessor ();

            var len = 2;

            var ilf = il.Create (OpCodes.Ldarg_0);

            var il48 = il.Create (OpCodes.Ldarg_2);
            il.Append (ilf);
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Stloc_2));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Bge_Un, il48));

            var il31 = il.Create (OpCodes.Ldarg_2);
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_I));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_I));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Br, il31));

            var il25 = il.Create (OpCodes.Ldloc_0);
            il.Append (il25);
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Stind_I1));

            il.Append (il31);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Starg, len));
            il.Append (il.Create (OpCodes.Brtrue, il25));

            var il51 = il.Create (OpCodes.Ldloc_2);
            il.Append (il.Create (OpCodes.Br, il51));

            var il3c = il.Create (OpCodes.Ldloc_0);
            il.Append (il3c);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Stind_I1));
            il.Append (il48);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Starg, len));
            il.Append (il.Create (OpCodes.Brtrue, il3c));

            il.Append (il51);
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemchr ()
        {
            var m = NewMethod ("@memchr", Types.PointerType.I8Pointer,
                               ("src", Types.PointerType.I8Pointer),
                               ("c", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var il11 = il.Create (OpCodes.Ldarg_2);
            il.Append (il.Create (OpCodes.Br, il11));

            var il2 = il.Create (OpCodes.Ldarg_0);
            il.Append (il2);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Conv_U1));
            il.Append (il.Create (OpCodes.Bne_Un, il11));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ret));

            il.Append (il11);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Starg, 2));
            il.Append (il.Create (OpCodes.Brtrue, il2));

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemsetChecked ()
        {
            var m = NewMethod ("@__memset_chk", Types.PointerType.I8Pointer,
                               ("b", Types.PointerType.I8Pointer),
                               ("c", Types.IntegerType.I32),
                               ("len", Types.IntegerType.I64),
                               ("dstlen", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var ilf = il.Create (OpCodes.Ldarg_0);
            il.Append (il.Create (OpCodes.Ldarg_3));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Bge_Un, ilf));
            il.Append (il.Create (OpCodes.Ldstr, "Buffer overflow"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            il.Append (ilf);
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Call, Calls["@memset"]));

            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemset ()
        {
            var m = NewMethod ("@memset", Types.PointerType.I8Pointer,
                               ("b", Types.PointerType.I8Pointer),
                               ("c", Types.IntegerType.I32),
                               ("len", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysBytePtr);
            var v1 = new VariableDefinition (compilation.sysBytePtr);
            var b = m.Body;
            b.Variables.Add (v0);
            b.Variables.Add (v1);
            var il = b.GetILProcessor ();

            var il11 = il.Create (OpCodes.Ldloc_0);

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_I));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Br, il11));

            var il9 = il.Create (OpCodes.Ldloc_0);
            il.Append (il9);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Conv_U1));
            il.Append (il.Create (OpCodes.Stind_I1));
            il.Append (il11);
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Blt_Un, il9));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemcpyChecked ()
        {
            var m = NewMethod ("@__memcpy_chk", Types.PointerType.I8Pointer,
                               ("dstpp", Types.PointerType.I8Pointer),
                               ("srcpp", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64),
                               ("dstlen", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var ilf = il.Create (OpCodes.Ldarg_0);
            il.Append (il.Create (OpCodes.Ldarg_3));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Bge_Un, ilf));
            il.Append (il.Create (OpCodes.Ldstr, "Buffer overflow"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            il.Append (ilf);
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Call, Calls["@memcpy"]));

            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemcpy ()
        {
            var m = NewMethod ("@memcpy", Types.PointerType.I8Pointer,
                               ("dst", Types.PointerType.I8Pointer),
                               ("src", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysBytePtr);
            var b = m.Body;
            b.Variables.Add (v0);
            var il = b.GetILProcessor ();

            var il12 = il.Create (OpCodes.Ldarg_2);

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Br, il12));

            var il4 = il.Create (OpCodes.Ldarg_0);
            il.Append (il4);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Stind_I1));
            il.Append (il12);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Starg, 2));
            il.Append (il.Create (OpCodes.Brtrue, il4));

            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitMemcmp ()
        {
            var m = NewMethod ("@memcmp", Types.IntegerType.I32,
                               ("s1", Types.PointerType.I8Pointer),
                               ("s2", Types.PointerType.I8Pointer),
                               ("len", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysInt32);
            var b = m.Body;
            b.Variables.Add (v0);
            var il = b.GetILProcessor ();

            var il1e = il.Create (OpCodes.Ldloc_0);
            var il2 = il.Create (OpCodes.Ldarg_2);

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il2);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Starg, 2));
            il.Append (il.Create (OpCodes.Brfalse, il1e));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Brfalse, il2));

            il.Append (il1e);
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        FieldDefinition stdin;
        FieldDefinition stdout;

        void EmitStandardStreams ()
        {
            var fattrs = FieldAttributes.Static;
            stdin = new FieldDefinition ("stdin", fattrs, compilation.sysStream);
            stdout = new FieldDefinition ("stdout", fattrs, compilation.sysStream);
            syscallsType.Fields.Add (stdin);
            syscallsType.Fields.Add (stdout);
        }

        void EmitStaticCtor ()
        {
            var mattrs = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
            var md = new MethodDefinition (".cctor", mattrs, compilation.sysVoid);
            syscallsType.Methods.Add (md);
            var b = new MethodBody (md);
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleOpenStandardInput));
            il.Append (il.Create (OpCodes.Stsfld, stdin));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleOpenStandardOutput));
            il.Append (il.Create (OpCodes.Stsfld, stdout));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
            md.Body = b;
        }

        void EmitFPuts ()
        {
            var m = NewMethod ("@fputs", Types.IntegerType.I32,
                               ("s", Types.PointerType.I8Pointer),
                               ("stream", Types.PointerType.I8Pointer));
            var v0 = new VariableDefinition (compilation.sysBytePtr);
            var b = m.Body;
            b.Variables.Add (v0);
            var il = b.GetILProcessor ();

            var il4 = il.Create (OpCodes.Ldsfld, stdout);
            var il14 = il.Create (OpCodes.Ldloc_0);
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Br, il14));

            il.Append (il4);
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Callvirt, compilation.sysStreamWriteByte));

            il.Append (il14);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brtrue, il4));

            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitFWrite ()
        {
            var m = NewMethod ("@fwrite", Types.IntegerType.I64,
                               ("ptr", Types.PointerType.I8Pointer),
                               ("size", Types.IntegerType.I64),
                               ("nitems", Types.IntegerType.I64),
                               ("stream", Types.PointerType.I8Pointer));
            var v0 = new VariableDefinition (compilation.sysInt32);
            var v1 = new VariableDefinition (compilation.sysByteArray);
            var b = m.Body;
            b.Variables.Add (v0);
            b.Variables.Add (v1);
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Mul));
            il.Append (il.Create (OpCodes.Conv_I4));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Newarr, compilation.sysByte));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysMarshalCopyIntToArray));
            il.Append (il.Create (OpCodes.Ldsfld, stdout));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Callvirt, compilation.sysStreamWrite));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitUnixRead (string symbol)
        {
            var m = NewMethod (symbol, Types.IntegerType.I64,
                               ("fildes", Types.IntegerType.I32),
                               ("buf", Types.PointerType.I8Pointer),
                               ("nbyte", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysInt32);
            var v1 = new VariableDefinition (compilation.sysByteArray);
            var b = m.Body;
            b.Variables.Add (v0);
            b.Variables.Add (v1);
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_I4));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Newarr, compilation.sysByte));
            il.Append (il.Create (OpCodes.Stloc_1));


            il.Append (il.Create (OpCodes.Ldsfld, stdin));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Callvirt, compilation.sysStreamRead));
            il.Append (il.Create (OpCodes.Stloc_0));


            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysMarshalCopyArrayToInt));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitUnixWrite (string symbol)
        {
            var m = NewMethod (symbol, Types.IntegerType.I64,
                               ("fildes", Types.IntegerType.I32),
                               ("buf", Types.PointerType.I8Pointer),
                               ("nbyte", Types.IntegerType.I64));
            var v0 = new VariableDefinition (compilation.sysInt32);
            var v1 = new VariableDefinition (compilation.sysByteArray);
            var b = m.Body;
            b.Variables.Add (v0);
            b.Variables.Add (v1);
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Conv_I4));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Newarr, compilation.sysByte));
            il.Append (il.Create (OpCodes.Stloc_1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysMarshalCopyIntToArray));
            il.Append (il.Create (OpCodes.Ldsfld, stdout));
            il.Append (il.Create (OpCodes.Ldloc_1));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Callvirt, compilation.sysStreamWrite));
            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitSetjmp ()
        {
            var m = NewMethod ("@setjmp", Types.IntegerType.I32,
                               ("env", Types.PointerType.I32Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitLongjmp ()
        {
            var m = NewMethod ("@longjmp", Types.VoidType.Void,
                               ("env", Types.PointerType.I32Pointer),
                               ("val", Types.IntegerType.I32));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "Cannot longjmp"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysNotSuppCtor));
            il.Append (il.Create (OpCodes.Throw));

            b.Optimize ();
        }

        void EmitUMulOvf64 ()
        {
            var st = new LiteralStructureType (new[] { Types.IntegerType.I64, Types.IntegerType.I1 });
            var ct = compilation.GetClrType (st, module).Resolve ();
            var m = NewMethod ("@llvm.umul.with.overflow.i64", st,
                               ("x", Types.IntegerType.I64),
                               ("y", Types.IntegerType.I64));
            var b = m.Body;
            var v0 = new VariableDefinition (ct);
            b.Variables.Add (v0);
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldloca_S, v0));
            il.Append (il.Create (OpCodes.Initobj, ct));
            il.Append (il.Create (OpCodes.Ldloca, v0));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Mul_Ovf_Un));
            il.Append (il.Create (OpCodes.Stfld, ct.Fields[0]));
            il.Append (il.Create (OpCodes.Ldloca, v0));
            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Stfld, ct.Fields[1]));
            il.Append (il.Create (OpCodes.Ldloc, v0));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitAbort ()
        {
            var m = NewMethod ("@abort", Types.VoidType.Void);
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "Abort"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            b.Optimize ();
        }

        void EmitVAStart ()
        {
            var m = NewMethod ("@llvm.va_start", compilation.sysVoid,
                               ("arglist", compilation.sysByte.MakePointerType()),
                               ("arguments", compilation.sysObjArray));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "VAStart"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            b.Optimize ();
        }

        void EmitVAEnd ()
        {
            var m = NewMethod ("@llvm.va_end", compilation.sysVoid,
                               ("arglist", compilation.sysByte.MakePointerType ()));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldstr, "VAEnd"));
            il.Append (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
            il.Append (il.Create (OpCodes.Throw));

            b.Optimize ();
        }

        void EmitFputc ()
        {
            var m = NewMethod ("@fputc", Types.IntegerType.I32,
                               ("c", Types.IntegerType.I32),
                               ("stream", Types.PointerType.VoidPointer));
            var b = m.Body;
            var il = b.GetILProcessor ();

            il.Append (il.Create (OpCodes.Ldsfld, stdout));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Conv_U1));
            il.Append (il.Create (OpCodes.Callvirt, compilation.sysStreamWriteByte));
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }

        void EmitStrchr ()
        {
            var m = NewMethod ("@strchr", Types.PointerType.I8Pointer,
                               ("s", Types.PointerType.I8Pointer),
                               ("c", Types.IntegerType.I32));
            var b = m.Body;
            var v0 = new VariableDefinition (compilation.sysBytePtr);
            b.Variables.Add (v0);
            var il = b.GetILProcessor ();

            var il2 = il.Create (OpCodes.Ldloc_0);
            var il9 = il.Create (OpCodes.Ldloc_0);
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il2);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Bne_Un, il9));

            il.Append (il.Create (OpCodes.Ldloc_0));
            il.Append (il.Create (OpCodes.Ret));

            var il10 = il.Create (OpCodes.Ldloc_0);
            il.Append (il9);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brtrue, il10));

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Conv_U));
            il.Append (il.Create (OpCodes.Ret));

            il.Append (il10);
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Stloc_0));
            il.Append (il.Create (OpCodes.Br, il2));

            b.Optimize ();
        }
        void EmitStrcmp ()
        {
            var m = NewMethod ("@strcmp", Types.IntegerType.I32,
                               ("s1", Types.PointerType.I8Pointer),
                               ("s2", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var il2 = il.Create (OpCodes.Ldarg_0);
            var ild = il.Create (OpCodes.Ldarg_0);
            il.Append (il.Create (OpCodes.Br, ild));

            il.Append (il2);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brtrue, ild));

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ret));

            il.Append (ild);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Beq, il2));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }
        void EmitStrncmp ()
        {
            var m = NewMethod ("@strncmp", Types.IntegerType.I32,
                               ("s1", Types.PointerType.I8Pointer),
                               ("s2", Types.PointerType.I8Pointer),
                               ("n", Types.IntegerType.I64));
            var b = m.Body;
            var il = b.GetILProcessor ();

            var il5 = il.Create (OpCodes.Ldarg_0);
            var il18 = il.Create (OpCodes.Ldarg_0);
            var il2a = il.Create (OpCodes.Ldc_I4_0);


            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Brtrue, il5));

            il.Append (il.Create (OpCodes.Ldc_I4_0));
            il.Append (il.Create (OpCodes.Ret));

            il.Append (il5);
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 1));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Beq, il18));

            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Ldarg_1));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Ret));

            il.Append (il18);
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Add));
            il.Append (il.Create (OpCodes.Starg, 0));
            il.Append (il.Create (OpCodes.Ldind_U1));
            il.Append (il.Create (OpCodes.Brfalse, il2a));

            il.Append (il.Create (OpCodes.Ldarg_2));
            il.Append (il.Create (OpCodes.Ldc_I4_1));
            il.Append (il.Create (OpCodes.Conv_I8));
            il.Append (il.Create (OpCodes.Sub));
            il.Append (il.Create (OpCodes.Dup));
            il.Append (il.Create (OpCodes.Starg, 2));
            il.Append (il.Create (OpCodes.Brtrue, il5));

            il.Append (il2a);
            il.Append (il.Create (OpCodes.Ret));

            b.Optimize ();
        }
    }
}
