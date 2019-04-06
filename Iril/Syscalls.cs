using System;
using Mono.Cecil;
using System.Collections.Generic;
using Iril.Types;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace Iril
{
    class Syscalls
    {
        readonly Compilation compilation;
        readonly TypeDefinition syscallsType;

        public readonly SymbolTable<MethodDefinition> Calls =
            new SymbolTable<MethodDefinition> ();

        public Syscalls (Compilation compilation, TypeDefinition syscallsType)
        {
            this.compilation = compilation;
            this.syscallsType = syscallsType;
        }

        public void Emit()
        {
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
            EmitVPrintf ();
            EmitPutchar ();
            EmitPuts ();
            EmitRealloc ();
            EmitMemsetPattern (4);
            EmitMemsetPattern (8);
            EmitMemsetPattern (16);
        }

        MethodDefinition NewMethod (Symbol symbol, LType returnType, params (string, LType)[] parameters)
        {
            var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static;
            var md = new MethodDefinition (
                new IR.MangledName (symbol).Identifier,
                mattrs,
                compilation.GetClrType (returnType));
            foreach (var p in parameters) {
                var pd = new ParameterDefinition (p.Item1, ParameterAttributes.None, compilation.GetClrType (p.Item2));
                if (p.Item2 is VarArgsType) {
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
            var p = new VariableDefinition (compilation.GetClrType (Types.PointerType.I8Pointer));
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
            var m = NewMethod ("@puts", Types.VoidType.Void, ("s", Types.PointerType.I8Pointer));
            var b = m.Body;
            var il = b.GetILProcessor ();
            il.Append (il.Create (OpCodes.Ldarg_0));
            il.Append (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
            il.Append (il.Create (OpCodes.Call, compilation.sysPtrToStringAuto));
            il.Append (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
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

            b.Variables.Add (new VariableDefinition (compilation.GetClrType (Types.PointerType.I8Pointer)));
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
    }
}