using System;
using Mono.Cecil;
using System.Collections.Generic;
using Repil.Types;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace Repil
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
            EmitCalloc ();
            EmitFree ();
            EmitMalloc ();
            EmitPrintf ();
            EmitRealloc ();
            EmitMemsetPattern (4);
            EmitMemsetPattern (8);
            EmitMemsetPattern (16);
        }

        MethodDefinition NewMethod (Symbol symbol, LType returnType, params (string, LType)[] parameters)
        {
            var mattrs = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Static;
            var md = new MethodDefinition (
                compilation.GetIdentifier (symbol),
                mattrs,
                compilation.GetClrType (returnType));
            foreach (var p in parameters) {
                var pd = new ParameterDefinition (p.Item1, ParameterAttributes.None, compilation.GetClrType (p.Item2));
                md.Parameters.Add (pd);
            }
            var body = new MethodBody (md);
            syscallsType.Methods.Add (md);
            Calls[symbol] = md;
            return md;
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
            il.Append (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            il.Append (il.Create (OpCodes.Ret));
            b.Optimize ();
        }

        void EmitPrintf ()
        {
            var m = NewMethod ("@printf", Types.VoidType.Void, ("format", Types.PointerType.I8Pointer), ("arguments", VarArgsType.VarArgs));
            var b = m.Body;
            var il = b.GetILProcessor ();
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