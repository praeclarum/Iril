using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Iril.Types;
using CecilInstruction = Mono.Cecil.Cil.Instruction;
using System.Runtime.InteropServices;
using System.Numerics;
using System.Collections;
using Iril.IR;
using Path = System.Collections.Generic.List<System.ValueTuple<System.Object, Iril.Types.LType>>;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace Iril
{
    class GlobalInitializersCompiler : InnerGlobalInitializersCompiler
    {
        public GlobalInitializersCompiler (Compilation compilation, Module module, MethodDefinition methodDefinition, ModuleGlobalInfo info)
            : base (compilation, module, methodDefinition, info)
        {
        }

        // Uncomment to make a new function for each field (helps in debugging)
        //public new void Compile (List<(IR.GlobalVariable, FieldDefinition)> needsInit)
        //{
        //    foreach (var (g, f) in needsInit) {
        //        var mattrs = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig;
        //        var cctor = new MethodDefinition ("__init" + f.Name, mattrs, compilation.sysVoid);
        //        method.DeclaringType.Methods.Add (cctor);
        //        var compiler = new InnerGlobalInitializersCompiler (compilation, module, cctor);
        //        compiler.Compile (new List<(IR.GlobalVariable, FieldDefinition)> { (g, f) });
        //        Emit (il.Create (OpCodes.Call, cctor));
        //    }
        //    Emit (OpCodes.Ret);
        //    body.Optimize ();
        //    method.Body = body;
        //}
    }

    public class ModuleGlobalInfo
    {
        public TypeDefinition DataType;
        public FieldDefinition DataField;
        public List<(IR.GlobalVariable Global, FieldDefinition Field)> NeedsInit = new List<(GlobalVariable, FieldDefinition)> ();
        public SymbolTable<(IR.GlobalVariable Global, FieldDefinition Field)> GlobalFields = new SymbolTable<(GlobalVariable, FieldDefinition)> ();
        public SymbolTable<(ModuleGlobalInfo Module, IR.GlobalVariable Global, FieldDefinition Field)> ExternalGlobals = new SymbolTable<(ModuleGlobalInfo Module, GlobalVariable Global, FieldDefinition Field)> ();
    }

    class InnerGlobalInitializersCompiler : Emitter
    {
        readonly ModuleGlobalInfo info;

        public InnerGlobalInitializersCompiler (Compilation compilation, Module module, MethodDefinition methodDefinition, ModuleGlobalInfo info)
            : base (compilation, module, methodDefinition)
        {
            this.info = info;
        }

        public void Compile ()
        {
            var gcHandleV = new Lazy<VariableDefinition> (() => {
                var v = new VariableDefinition (compilation.sysGCHandle);
                body.Variables.Add (v);
                return v;
            });

            var needsInit = info.NeedsInit;

            // Allocate the globals
            Emit (il.Create (OpCodes.Sizeof, info.DataType));
            Emit (il.Create (OpCodes.Call, compilation.sysAllocHGlobalInt));

            // Zero init
            Emit (il.Create (OpCodes.Dup));
            Emit (il.Create (OpCodes.Ldc_I4_0));
            Emit (il.Create (OpCodes.Conv_U1));
            Emit (il.Create (OpCodes.Sizeof, info.DataType));
            Emit (il.Create (OpCodes.Initblk));

            // Register with the memory manager
            if (compilation.Options.SafeMemory) {
                Emit (il.Create (OpCodes.Dup));
                Emit (il.Create (OpCodes.Sizeof, info.DataType));
                Emit (il.Create (OpCodes.Conv_I8));
                Emit (il.Create (OpCodes.Ldstr, IR.MangledName.Demangle (module.Symbol) + ".globals"));
                Emit (il.Create (OpCodes.Call, compilation.GetSystemMethod ("@_register_memory")));
            }

            // Store them for later
            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
            Emit (il.Create (OpCodes.Stsfld, info.DataField));

            var startPath = new List<(object, LType)> { (info.DataField, default(LType)) };

            foreach (var (g, f) in needsInit) {
                try {
                    if (ShouldTrace >= 3) {
                        Emit (il.Create (OpCodes.Ldstr, $"Init Field: {f.DeclaringType}::{f.Name}"));
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleGetOut));
                        Emit (il.Create (OpCodes.Callvirt, compilation.sysTextWriterFlush));
                    }
                    var fp = new Path (startPath);
                    var gf = info.GlobalFields[g.Symbol].Field;
                    fp.Add ((gf, g.Type));
                    foreach (var p in BuildPaths (fp, g.Initializer)) {                        
                        EmitPath (p);
                    }
                }
                catch (Exception ex) {
                    compilation.ErrorMessage (module.SourceFilename, $"Failed to init global `{g.Symbol}`", ex);
                }
            }

            Emit (OpCodes.Ret);

            body.Optimize ();
            body.InitLocals = true;
            method.Body = body;
        }

        void EmitPath (Path path)
        {
            //EmitConsoleWriteLine ($"INIT {string.Join (" << ", path.Select (x => (x.Item1, x.Item2)))}");

            var vi = path.Count - 1;
            var (vo, vt) = path[vi];
            var ri = vi - 1;
            var (ro, rt) = path[ri];

            if (vo is BytesConstant c && vt is Types.ArrayType carrt) {
                var bytes = EmitBytesConstantArray (c);
                Emit (il.Create (OpCodes.Ldc_I4_0));
                EmitPathPointer (path, vi);
                Emit (il.Create (OpCodes.Conv_U));
                Emit (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
                Emit (il.Create (OpCodes.Ldc_I4, bytes.Length));
                Emit (il.Create (OpCodes.Conv_U));
                Emit (il.Create (OpCodes.Call, compilation.sysMarshalCopyArrayToInt));
            }
            else {
                if (ro is int i) {                    
                    var t = EmitPathPointer (path, ri);
                    Emit (il.Create (OpCodes.Conv_U));
                    Emit (il.Create (OpCodes.Ldc_I4, i));
                    Emit (il.Create (OpCodes.Conv_I));
                    Emit (il.Create (OpCodes.Sizeof, compilation.GetClrType (rt, module)));
                    Emit (il.Create (OpCodes.Mul));
                    Emit (il.Create (OpCodes.Add));
                    EmitPathValue ((Value)vo, vt, t);
                    EmitStind (vt);
                }
                else if (ro is FieldIndex fi) {
                    var t = EmitPathPointer (path, ri);
                    var f = t.Resolve ().Fields[fi.Index];
                    EmitPathValue ((Value)vo, vt, f.FieldType);
                    Emit (il.Create (OpCodes.Stfld, f));
                }
                else if (ro is FieldReference fr) {
                    var t = EmitPathPointer (path, ri);
                    EmitPathValue ((Value)vo, vt, fr.FieldType);
                    Emit (il.Create (OpCodes.Stfld, fr));
                }
                else {
                    throw new NotSupportedException (ro.GetType ().Name);
                }
            }
        }

        TypeReference EmitPathPointer (Path path, int end)
        {
            TypeReference lastType = null;
            for (var pi = 0; pi < end; pi++) {
                var (po, pt) = path[pi];
                if (pi == 0) {
                    Emit (il.Create (OpCodes.Ldsfld, info.DataField));
                    lastType = info.DataField.FieldType;
                }
                else if (po is FieldReference fr) {
                    Emit (il.Create (OpCodes.Ldflda, fr));
                    lastType = fr.FieldType;
                }
                else if (po is FieldIndex fi) {
                    var td = lastType.Resolve ();
                    var fd = td.Fields[fi.Index];
                    Emit (il.Create (OpCodes.Ldflda, fd));
                    lastType = fd.FieldType;
                }
                else if (po is int i) {
                    Emit (il.Create (OpCodes.Conv_U));
                    Emit (il.Create (OpCodes.Ldc_I4, i));
                    Emit (il.Create (OpCodes.Conv_I));
                    Emit (il.Create (OpCodes.Sizeof, lastType));
                    Emit (il.Create (OpCodes.Mul));
                    Emit (il.Create (OpCodes.Add));
                }
                else {
                    throw new NotSupportedException (po.ToString ());
                }
            }
            return lastType;
        }

        void EmitPathValue (Value value, LType valueType, TypeReference pointerType)
        {
            var cvt = compilation.GetClrType (valueType, module);
            if (cvt.FullName != pointerType.FullName) {
                //if (valueType != pointerType) {
                Console.WriteLine ($"!!! v={value},\tvt={valueType},\tcvt={cvt},\tpt={pointerType}");
                //}
            }
            EmitValue (value, valueType);
        }

        byte[] EmitBytesConstantArray (BytesConstant c)
        {
            var chars = new List<byte> ();
            var s = c.Bytes.Text;
            var i = 2;
            var n = s.Length - 1;
            while (i < n) {
                if (s[i] == '\\' && i + 1 < n && s[i + 1] == '\\') {
                    chars.Add ((byte)'\\');
                    i += 2;
                }
                else if (s[i] == '\\' && i + 2 < n) {
                    var hex = s.Substring (i + 1, 2);
                    var v = int.Parse (hex, System.Globalization.NumberStyles.HexNumber);
                    var sv = Math.Min (255, Math.Max (0, v));
                    chars.Add ((byte)sv);
                    i += 3;
                }
                else {
                    chars.Add ((byte)s[i]);
                    i++;
                }
            }
            var bytes = chars.ToArray ();
            var isAscii = bytes.All (x => (x == '\n' || x == '\r' || x == '\0') || (x >= ' ' && x < 128));
            var size = bytes.Length;
            if (isAscii) {
                var str = System.Text.Encoding.ASCII.GetString (bytes);
                Emit (il.Create (OpCodes.Call, compilation.sysAscii));
                Emit (il.Create (OpCodes.Ldstr, str));
                Emit (il.Create (OpCodes.Callvirt, compilation.sysAsciiGetBytes));
            }
            else {
                var dataField = compilation.AddDataField (bytes);
                Emit (il.Create (OpCodes.Ldc_I4, size));
                Emit (il.Create (OpCodes.Newarr, compilation.sysByte));
                Emit (il.Create (OpCodes.Dup));
                Emit (il.Create (OpCodes.Ldtoken, dataField));
                Emit (il.Create (OpCodes.Call, compilation.sysRuntimeHelpersInitArray));
            }
            return bytes;
        }

        class FieldIndex
        {
            public int Index;
            public override string ToString () => "F" + Index;
        }

        List<Path> BuildPaths (Path path, Value value)
        {
            var r = new List<Path> ();
            var lastType = path.Last ().Item2;
            if (value is ArrayConstant arr) {
                var et = ((Types.ArrayType)lastType).ElementType;
                for (var i = 0; i < arr.Elements.Length; i++) {
                    var p = new Path (path);
                    p.Add ((i, et));
                    r.AddRange (BuildPaths (p, arr.Elements[i].Value));
                }
            }
            else if (value is StructureConstant str) {
                var ltd = (LiteralStructureType)lastType.Resolve (module);
                for (var i = 0; i < str.Elements.Length; i++) {
                    var p = new Path (path);
                    var f = ltd.Elements[i];
                    p.Add ((new FieldIndex { Index = i }, f));
                    r.AddRange (BuildPaths (p, str.Elements[i].Value));
                }
            }
            else if (value is ZeroConstant) {
                // Nothing to do, memory is zerod to start with
            }
            else if (value is UndefinedConstant) {
                // Nothing to do
            }
            else {
                var p = new Path (path);
                p.Add ((value, lastType));
                r.Add (p);
            }
            return r;
        }

        List<(IR.GlobalVariable, FieldDefinition)> SortFields (List<(IR.GlobalVariable, FieldDefinition)> needsInit)
        {
            var r = new List<(IR.GlobalVariable, FieldDefinition)> ();
            var added = new SymbolTable<bool> ();
            var adding = new SymbolTable<bool> ();
            var all = new SymbolTable<(IR.GlobalVariable, FieldDefinition)> ();
            foreach (var i in needsInit) {
                all[i.Item1.Symbol] = i;
            }
            void Init (GlobalSymbol symbol)
            {
                if (added.ContainsKey (symbol))
                    return;
                if (adding.ContainsKey (symbol))
                    return;
                if (!all.ContainsKey (symbol))
                    return;
                //Console.WriteLine ("INIT " + symbol);
                var i = all[symbol];
                var deps = i.Item1.Initializer.ReferencedGlobals;
                adding[symbol] = true;
                foreach (var d in deps) {
                    Init (d);
                }
                if (!added.ContainsKey (symbol)) {
                    r.Add (i);
                    added.Add (symbol, true);
                }
                adding.Remove (symbol);
            }
            foreach (var i in needsInit) {
                Init (i.Item1.Symbol);
            }
            return r;
        }

        readonly Dictionary<string, List<VariableDefinition>> pinnedRefs = new Dictionary<string, List<VariableDefinition>> ();

        VariableDefinition GetPinnedRef (TypeReference typeReference)
        {
            var key = typeReference.FullName;
            if (!pinnedRefs.TryGetValue (key, out var vars)) {
                vars = new List<VariableDefinition> ();
                pinnedRefs.Add (key, vars);
            }
            if (vars.Count > 0) {
                var ev = vars[vars.Count - 1];
                vars.RemoveAt (vars.Count - 1);
                return ev;
            }
            var nv = new VariableDefinition (typeReference.MakePinnedType ());
            body.Variables.Add (nv);
            return nv;
        }

        void ReleasePinnedRef (TypeReference typeReference, VariableDefinition variable)
        {
            Emit (il.Create (OpCodes.Ldc_I4_0));
            Emit (il.Create (OpCodes.Conv_U));
            Emit (il.Create (OpCodes.Stloc, variable));
            pinnedRefs[typeReference.FullName].Add (variable);
        }

        void EmitInitializer (IR.Value initializer, LType type, Lazy<VariableDefinition> gcHandleV, CecilInstruction store)
        {
            switch (initializer) {
                case IR.ArrayConstant c: {
                        //Console.WriteLine ("EMIT ARRAY TO " + store);
                        var size = c.Elements.Length;
                        var et = c.Elements[0].Type;
                        var cet = compilation.GetClrType (et, module: module);
                        if (store.OpCode == OpCodes.Stfld) {
                            var operand = (FieldReference)store.Operand;
                            Emit (il.Create (OpCodes.Ldflda, operand));
                            var locRefType = operand.FieldType.MakePointerType ();
                            var locRef = GetPinnedRef (locRefType);
                            Emit (il.Create (OpCodes.Stloc, locRef));
                            Emit (il.Create (OpCodes.Ldloc, locRef));
                            Emit (il.Create (OpCodes.Conv_U));
                            for (int i = 0; i < c.Elements.Length; i++) {
                                if (i + 1 < c.Elements.Length) {
                                    Emit (il.Create (OpCodes.Dup));
                                }
                                Emit (il.Create (OpCodes.Ldc_I4, i));
                                Emit (il.Create (OpCodes.Conv_I));
                                Emit (il.Create (OpCodes.Sizeof, cet));
                                Emit (il.Create (OpCodes.Mul));
                                Emit (il.Create (OpCodes.Add));
                                var e = c.Elements[i];
                                var storee = il.Create (OpCodes.Stobj, cet);
                                EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                            }
                            ReleasePinnedRef (locRefType, locRef);
                        }
                        else if (store.OpCode == OpCodes.Stsfld) {
                            var field = (FieldReference)store.Operand;
                            Emit (il.Create (OpCodes.Ldc_I4, c.Elements.Length));
                            Emit (il.Create (OpCodes.Sizeof, cet));
                            Emit (il.Create (OpCodes.Mul));
                            Emit (il.Create (OpCodes.Call, compilation.sysAllocHGlobalInt));
                            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                            Emit (store);
                            for (int i = 0; i < c.Elements.Length; i++) {
                                Emit (il.Create (OpCodes.Ldsfld, field));
                                Emit (il.Create (OpCodes.Ldc_I4, i));
                                Emit (il.Create (OpCodes.Conv_I));
                                Emit (il.Create (OpCodes.Sizeof, cet));
                                Emit (il.Create (OpCodes.Mul));
                                Emit (il.Create (OpCodes.Add));
                                var e = c.Elements[i];
                                var storee = il.Create (OpCodes.Stobj, cet);
                                EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                            }
                        }
                        else {
                            var ocet = cet;
                            if (cet.IsPointer) {
                                ocet = compilation.sysIntPtr;
                            }
                            Emit (il.Create (OpCodes.Ldc_I4, size));
                            Emit (il.Create (OpCodes.Ldc_I4, size));
                            Emit (il.Create (OpCodes.Newarr, ocet));
                            Emit (il.Create (OpCodes.Dup));
                            for (int i = 0; i < c.Elements.Length; i++) {
                                var e = c.Elements[i];
                                Emit (il.Create (OpCodes.Ldc_I4, i));
                                if (cet.IsPointer) {
                                    var converte = il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer);
                                    EmitInitializer (e.Value, e.Type, gcHandleV, converte);
                                    Emit (il.Create (OpCodes.Stelem_Any, ocet));
                                }
                                else {
                                    var storee = il.Create (OpCodes.Stelem_Any, ocet);
                                    EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                                }
                                Emit (il.Create (OpCodes.Dup));
                            }
                            Emit (il.Create (OpCodes.Pop));
                            Emit (OpCodes.Ldc_I4_3);
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                            Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                            Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                            Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                            Emit (store);
                        }
                    }
                    break;
                case IR.ZeroConstant _ when type is Types.ArrayType art: {
                        var size = (int)art.Length;
                        var et = art.ElementType;
                        var cet = compilation.GetClrType (et, module: module);
                        var ocet = cet;
                        if (cet.IsPointer) {
                            ocet = compilation.sysIntPtr;
                        }
                        Emit (il.Create (OpCodes.Ldc_I4, size));
                        Emit (il.Create (OpCodes.Newarr, ocet));
                        Emit (OpCodes.Ldc_I4_3);
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                        Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                        Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                        Emit (store);
                    }
                    break;
                case IR.UndefinedConstant _ when type is Types.ArrayType art: {
                        var size = (int)art.Length;
                        var et = art.ElementType;
                        var cet = compilation.GetClrType (et, module: module);
                        var ocet = cet;
                        if (cet.IsPointer) {
                            ocet = compilation.sysIntPtr;
                        }
                        Emit (il.Create (OpCodes.Ldc_I4, size));
                        Emit (il.Create (OpCodes.Newarr, ocet));
                        Emit (OpCodes.Ldc_I4_3);
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                        Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                        Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                        Emit (store);
                    }
                    break;
                case IR.BytesConstant c: {
                        EmitBytesConstantArray (c);
                        Emit (OpCodes.Ldc_I4_3);
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAlloc));
                        Emit (il.Create (OpCodes.Stloc, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Ldloca, gcHandleV.Value));
                        Emit (il.Create (OpCodes.Call, compilation.sysGCHandleAddrOfPinnedObject));
                        Emit (il.Create (OpCodes.Call, compilation.sysPointerFromIntPtr));
                        Emit (store);
                    }
                    break;
                case IR.StructureConstant c:
                    if (store.OpCode.Code == Code.Stsfld) {
                        var f = (FieldReference)store.Operand;
                        var td = f.FieldType.Resolve ();
                        var n = c.Elements.Length;
                        for (int i = 0; i < n; i++) {
                            var e = c.Elements[i];
                            Emit (il.Create (OpCodes.Ldsflda, f));
                            var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                            EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                        }
                    }
                    else if (store.OpCode.Code == Code.Stelem_Any && type is NamedType namedType) {
                        var td = ((TypeReference)store.Operand).Resolve ();
                        var v = GetStructTempLocal (namedType);
                        var n = c.Elements.Length;
                        for (int i = 0; i < n; i++) {
                            var e = c.Elements[i];
                            Emit (il.Create (OpCodes.Ldloca, v));
                            var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                            EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                        }
                        Emit (il.Create (OpCodes.Ldloc, v));
                        Emit (store);
                    }
                    else {
                        TypeDefinition td;
                        if (store.OpCode.Code == Code.Stfld) {
                            var f = (FieldReference)store.Operand;
                            td = f.FieldType.Resolve ();
                        }
                        else {
                            td = ((TypeReference)store.Operand).Resolve ();
                        }
                        var v = GetStructTempLocal (td);
                        var n = c.Elements.Length;
                        for (int i = 0; i < n; i++) {
                            var e = c.Elements[i];
                            if (ShouldTrace >= 3) {
                                Emit (il.Create (OpCodes.Ldstr, $"Init field #{i}: {e.Type} = {e.Value}   (for type {type})"));
                                Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
                            }
                            Emit (il.Create (OpCodes.Ldloca, v));
                            var storee = il.Create (OpCodes.Stfld, td.Fields[i]);
                            EmitInitializer (e.Value, e.Type, gcHandleV, storee);
                        }
                        Emit (il.Create (OpCodes.Ldloc, v));
                        Emit (store);
                    }
                    break;
                default:
                    EmitValue (initializer, type);
                    Emit (store);
                    break;
            }
        }
    }
}
