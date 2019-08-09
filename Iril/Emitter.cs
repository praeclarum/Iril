using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Iril.Types;
using CecilInstruction = Mono.Cecil.Cil.Instruction;
using System.Linq;
using System.Collections.Generic;
using Iril.IR;
using System.Numerics;

namespace Iril
{
    abstract class Emitter
    {
        public const int ShouldTrace = 0;

        // Input
        protected readonly Compilation compilation;
        protected readonly Module module;
        protected readonly MethodDefinition method;

        // Created
        protected readonly MethodBody body;
        protected readonly ILProcessor il;

        // Working Variables
        protected CecilInstruction prev;
        protected readonly SymbolTable<VariableDefinition> locals = new SymbolTable<VariableDefinition> ();
        protected readonly SymbolTable<SymbolTable<string>> blockLocalNames = new SymbolTable<SymbolTable<string>> ();

        readonly Dictionary<(string, int), VariableDefinition> vectorTemps = new Dictionary<(string, int), VariableDefinition> ();
        readonly SymbolTable<VariableDefinition> structTempLocals = new SymbolTable<VariableDefinition> ();
        readonly Dictionary<string, VariableDefinition> trTempLocals = new Dictionary<string, VariableDefinition> ();
        readonly Dictionary<LType[], VariableDefinition> astructTempLocals =
            new Dictionary<LType[], VariableDefinition> (AnonymousStruct.LTypesEquality);

        protected Emitter(Compilation compilation, Module module, MethodDefinition methodDefinition)
        {
            this.compilation = compilation;
            this.module = module;
            this.method = methodDefinition;

            body = new MethodBody (methodDefinition);
            il = body.GetILProcessor();
            prev = default(CecilInstruction);

            nativeExceptionLocal = new Lazy<VariableDefinition> (() => {
                var r = new VariableDefinition (compilation.nativeException.Value);
                body.Variables.Add (r);
                return r;
            });
        }

        protected void Emit (OpCode code)
        {
            Emit (il.Create (code));
        }

        protected void Emit(CecilInstruction i)
        {
            if (prev != null)
                il.InsertAfter (prev, i);
            else
                il.Append (i);
            prev = i;
        }

        protected void EmitTypedValue (IR.TypedValue value)
        {
            EmitValue (value.Value, value.Type);
        }

        protected void EmitValue(IR.Value value, LType type)
        {
            switch (value)
            {
                case IR.BitcastValue bitcast:
                    // CLR doesn't need bitcast
                    EmitTypedValue(bitcast.Value);
                    break;
                case IR.BooleanConstant b:
                    Emit(il.Create(b.IsTrue ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
                    break;
                case IR.FloatConstant flt:
                    Emit(il.Create(
                        ((FloatType)type).Bits == 32 ? OpCodes.Ldc_R4 : OpCodes.Ldc_R8,
                        flt.Value));
                    break;
                case IR.GetElementPointerValue gep:
                    EmitGetElementPointer (gep.Pointer, gep.Indices);
                    break;
                case IR.GlobalValue g:
                    if (compilation.TryGetFunction(module, g.Symbol, out var ff))
                    {
                        Emit (il.Create (OpCodes.Ldftn, ff.ILDefinition));
                    }
                    else if (compilation.TryGetGlobal(module.Symbol, g.Symbol, out var globalVariable))
                    {
                        if (globalVariable.Global.Type is Types.ArrayType) {
                            Emit (il.Create (OpCodes.Ldsfld, globalVariable.Field));
                        }
                        else {
                            Emit (il.Create (OpCodes.Ldsflda, globalVariable.Field));
                            Emit (il.Create (OpCodes.Conv_U));
                        }
                    }
                    else
                    {
                        throw new NotSupportedException($"Undeclared global variable `{IR.MangledName.Demangle (g.Symbol)}` ({g.Symbol}) referenced in {module}");
                    }
                    break;
                case IR.HexIntegerConstant i:
                    if (type is FloatType fltt)
                    {
                        var ba = new byte[8];
                        var da = new double[1];
                        ba[7] = (byte)((i.Value >> 56) & 0xFF);
                        ba[6] = (byte)((i.Value >> 48) & 0xFF);
                        ba[5] = (byte)((i.Value >> 40) & 0xFF);
                        ba[4] = (byte)((i.Value >> 32) & 0xFF);
                        ba[3] = (byte)((i.Value >> 24) & 0xFF);
                        ba[2] = (byte)((i.Value >> 16) & 0xFF);
                        ba[1] = (byte)((i.Value >> 8) & 0xFF);
                        ba[0] = (byte)((i.Value >> 0) & 0xFF);
                        Buffer.BlockCopy(ba, 0, da, 0, 8);
                        switch (fltt.Bits)
                        {
                            case 32:
                                Emit(il.Create(OpCodes.Ldc_R4, (float)da[0]));
                                break;
                            case 64:
                                Emit(il.Create(OpCodes.Ldc_R8, da[0]));
                                break;
                            default:
                                throw new NotSupportedException($"{((IntegerType)type).Bits}-bit float integers");
                        }
                    }
                    else
                    {
                        switch (Compilation.RoundUpIntBits (((IntegerType)type).Bits))
                        {
                            case 8:
                                Emit(il.Create(OpCodes.Ldc_I4, ((int)i.Value) & 0xFF));
                                break;
                            case 16:
                                Emit(il.Create(OpCodes.Ldc_I4, ((int)i.Value) & 0xFFFF));
                                break;
                            case 32:
                                Emit(il.Create(OpCodes.Ldc_I4, (int)i.Value));
                                break;
                            case 64:
                                Emit(il.Create(OpCodes.Ldc_I8, (long)i.Value));
                                break;
                            default:
                                throw new NotSupportedException($"{((IntegerType)type).Bits}-bit integers");
                        }
                    }
                    break;
                case IR.IntegerConstant i: {
                        var intt = (IntegerType)type;
                        var bits = intt.Bits;
                        var upBits = Compilation.RoundUpIntBits (bits);
                        var mask = IntegerConstant.MaskBits (bits).Value;
                        var maskedValue = i.Value;
                        if (bits != upBits) {
                            if (maskedValue >= 0) {
                                maskedValue = (i.Value & mask);
                            }
                            else {
                                if (upBits == 8) {
                                    //var sext = IntegerConstant.MaskBits (8 - bits).Value << bits;
                                    maskedValue = new BigInteger ((byte)(sbyte)(i.Value)) & mask;
                                }
                            }
                        }
                        switch (upBits) {
                            case 8:
                                Emit (il.Create (OpCodes.Ldc_I4, (int)maskedValue));
                                Emit (il.Create (OpCodes.Conv_U1));
                                break;
                            case 16:
                                Emit (il.Create (OpCodes.Ldc_I4, (int)maskedValue));
                                Emit (il.Create (OpCodes.Conv_I2));
                                break;
                            case 32:
                                Emit (il.Create (OpCodes.Ldc_I4, (int)maskedValue));
                                break;
                            case 64:
                                //Console.WriteLine ($"{i.Value} masked = {(long)maskedValue}");
                                Emit (il.Create (OpCodes.Ldc_I8, (long)maskedValue));
                                break;
                            default:
                                throw new NotSupportedException ($"{((IntegerType)type).Bits}-bit integers");
                        }
                    }
                    break;
                case IR.IntToPointerValue itop:
                    EmitTypedValue(itop.Value);
                    Emit(il.Create(OpCodes.Conv_U));
                    break;
                case IR.LocalValue local:
                    EmitLocalValue(local, type, unsigned: false);
                    break;
                case IR.NullConstant nll:
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Conv_U));
                    break;
                case IR.PtrtointValue i:
                    EmitTypedValue(i.Value);
                    switch (Compilation.RoundUpIntBits (((IntegerType)i.Type).Bits))
                    {
                        case 8:
                            Emit(il.Create(OpCodes.Conv_I1));
                            break;
                        case 16:
                            Emit(il.Create(OpCodes.Conv_I2));
                            break;
                        case 32:
                            Emit(il.Create(OpCodes.Conv_I4));
                            break;
                        case 64:
                            Emit(il.Create(OpCodes.Conv_I8));
                            break;
                        default:
                            throw new NotSupportedException($"Cannot ptrtoint {i.Type}");
                    }
                    break;
                case IR.UndefinedConstant und:
                    EmitZeroValue(type);
                    break;
                case IR.VectorConstant vec:
                    foreach (var c in vec.Constants)
                    {
                        EmitValue(c.Value, c.Type);
                    }
                    {
                        var vt = compilation.GetVectorType((VectorType)type, module: module);
                        Emit(il.Create(OpCodes.Newobj, vt.Ctor));
                    }
                    break;
                case IR.VoidValue vv:
                    // Should only be used in returns
                    break;
                case IR.ZeroConstant zero:
                    EmitZeroValue(type);
                    break;
                default:
                    throw new NotSupportedException($"Cannot emit value {value} ({value?.GetType()?.Name}) with type {type}");
            }
        }

        protected virtual void EmitLocalValue(IR.LocalValue local, LType resultType, bool unsigned)
        {
            throw new NotSupportedException();
        }

        protected void EmitTypedLValue (IR.TypedValue value)
        {
            EmitLValue (value.Value, value.Type);
        }

        protected void EmitLValue (IR.Value value, LType type)
        {
            switch (value) {
                case IR.UndefinedConstant uc when type is VectorType vt: {
                        var v = GetVectorTempVariable (compilation.GetVectorType (vt, module: module), value, 0);
                        Emit (il.Create (OpCodes.Ldloca, v));
                    }
                    break;
                case IR.UndefinedConstant uc when type is LiteralStructureType st: {
                        var v = GetStructTempLocal (st);
                        Emit (il.Create (OpCodes.Ldloca, v));
                    }
                    break;
                case IR.LocalValue lv when locals.ContainsKey (lv.Symbol): {
                        var v = locals[lv.Symbol];
                        Emit (il.Create (OpCodes.Ldloca, v));
                    }
                    break;
                case IR.LocalValue lv when type is VectorType vt: {
                        var v = GetVectorTempVariable (compilation.GetVectorType (vt, module: module), value, 0);
                        EmitValue (value, type);
                        Emit (il.Create (OpCodes.Stloc, v));
                        Emit (il.Create (OpCodes.Ldloca, v));
                    }
                    break;
                default:
                    throw new NotSupportedException ($"Cannot emit lvalue {value} ({value?.GetType ()?.Name}) with type {type}");
            }
        }

        protected void EmitGetElementPointer(IR.TypedValue pointer, IR.TypedValue[] indices)
        {
            EmitTypedValue (pointer);
            if (TryEmitConstantGetElementPointer (pointer, indices)) {
                return;
            }
            var t = pointer.Type;
            var n = indices.Length;
            for (var i = 0; i < n; i++)
            {
                var index = indices[i];
                if (t is Types.PointerType pt)
                {
                    t = pt.ElementType;
                    if (index.Value is IR.Constant c && c.Int32Value == 0)
                    {
                        // Do nothing
                    }
                    else
                    {
                        var esize = t.GetByteSize(module);
                        if (index.Value is IR.Constant ic)
                        {
                            var offset = esize * ic.Int32Value;
                            EmitPointerOffset (offset);
                        }
                        else
                        {
                            EmitValue (index.Value, index.Type);
                            if (esize != 1) {
                                EmitValue (new IR.IntegerConstant (esize), index.Type);
                                Emit (il.Create (OpCodes.Mul));
                            }
                            Emit (il.Create (OpCodes.Conv_U));
                            Emit (il.Create (OpCodes.Add));
                        }
                    }
                }
                else if (t is NamedType
                         && t.Resolve(module) is Types.LiteralStructureType st
                         && index.Value is IR.IntegerConstant iconst)
                {
                    var cst = compilation.GetClrType(t, module).Resolve();
                    var fieldIndex = (int)iconst.Value;
                    if (fieldIndex < 0 || fieldIndex >= cst.Fields.Count)
                        throw new IndexOutOfRangeException ($"Field #{fieldIndex} does not exist in {cst.FullName} ({pointer}[{string.Join(", ", (object[])indices)}])");
                    var field = cst.Fields[fieldIndex];
                    Emit(il.Create(OpCodes.Ldflda, field));
                    Emit(il.Create(OpCodes.Conv_U));
                    var iindex = iconst.Int32Value;
                    if (0 <= iindex && iindex < st.Elements.Length)
                    {
                        t = st.Elements[iindex];
                    }
                    else
                    {
                        throw new InvalidOperationException($"Cannot get element #{i} from {st} ({t})");
                    }
                }
                else if (t is Types.ArrayType artt)
                {
                    var esize = artt.ElementType.GetByteSize(module);
                    if (index.Value is Constant ic) {
                        var offset = (long)esize * ic.Int32Value;
                        EmitPointerOffset (offset);
                    }
                    else {
                        EmitValue (index.Value, index.Type);
                        if (esize != 1) {
                            EmitValue (new IR.IntegerConstant (esize), index.Type);
                            Emit (il.Create (OpCodes.Mul));
                        }
                        Emit (il.Create (OpCodes.Conv_U));
                        Emit (il.Create (OpCodes.Add));
                    }
                    t = artt.ElementType;
                }
                else
                {
                    throw new InvalidOperationException("Cannot get pointer to element of " + t);
                }
            }
        }

        void EmitPointerOffset (long offset)
        {
            if (offset != 0) {
                if (int.MinValue < offset && offset < int.MaxValue) {
                    if (offset > 0) {
                        Emit (il.Create (OpCodes.Ldc_I4, (int)offset));
                        Emit (il.Create (OpCodes.Add));
                    }
                    else {
                        Emit (il.Create (OpCodes.Ldc_I4, (int)(-offset)));
                        Emit (il.Create (OpCodes.Sub));
                    }
                }
                else {
                    if (offset > 0) {
                        Emit (il.Create (OpCodes.Ldc_I8, offset));
                        Emit (il.Create (OpCodes.Conv_U));
                        Emit (il.Create (OpCodes.Add));
                    }
                    else {
                        Emit (il.Create (OpCodes.Ldc_I8, -offset));
                        Emit (il.Create (OpCodes.Conv_U));
                        Emit (il.Create (OpCodes.Sub));
                    }
                }
            }
        }

        bool TryEmitConstantGetElementPointer (IR.TypedValue pointer, IR.TypedValue[] indices)
        {
            var t = pointer.Type;
            var n = indices.Length;
            long offset = 0L;
            for (var i = 0; i < n; i++) {
                var index = indices[i];
                if (t is Types.PointerType pt) {
                    t = pt.ElementType;
                    if (index.Value is IR.Constant c && c.Int32Value == 0) {
                        // Do nothing
                    }
                    else if (index.Value is IR.Constant ic) {
                        var esize = t.GetByteSize (module);
                        offset += esize * ic.Int32Value;
                    }
                    else {
                        return false;
                    }
                }
                else if (t is Types.ArrayType artt) {
                    var esize = artt.ElementType.GetByteSize (module);
                    if (index.Value is IR.Constant ic) {
                        offset += esize * ic.Int32Value;
                    }
                    else {
                        return false;
                    }
                    t = artt.ElementType;
                }
                else {
                    return false;
                }
            }
            EmitPointerOffset (offset);
            return true;
        }

        protected void EmitExtractValue (IR.TypedValue aggregateValue, IR.Value[] indices)
        {
            EmitTypedValue (aggregateValue);
            var t = aggregateValue.Type;
            var n = indices.Length;
            for (var i = 0; i < n; i++) {
                var index = (IR.Constant)indices[i];
                if (t.Resolve (module) is Types.LiteralStructureType st) {
                    var iindex = index.Int32Value;
                    var cst = compilation.GetClrType (st, module).Resolve ();
                    if (0 <= iindex && iindex < st.Elements.Length) {
                        var field = cst.Fields[iindex];
                        Emit (il.Create (OpCodes.Ldfld, field));
                        t = st.Elements[iindex];
                    }
                    else {
                        throw new InvalidOperationException ($"Cannot extract value #{i} from {st} ({t})");
                    }
                }
                else {
                    throw new InvalidOperationException ("Cannot extract value from " + t);
                }
            }
        }

        protected void EmitInsertElement (IR.TypedValue vectorValue, IR.TypedValue elementValue, IR.TypedValue index)
        {
            var vt = compilation.GetVectorType ((VectorType)vectorValue.Type, module: module);
            
            EmitTypedLValue (vectorValue);
            Emit (OpCodes.Dup);
            EmitTypedValue (elementValue);
            if (index.Value is IR.Constant ic) {
                Emit (il.Create (OpCodes.Stfld, vt.ElementFields[ic.Int32Value]));
                Emit (il.Create (OpCodes.Ldobj, vt.ClrType));
            }
            else {
                throw new NotSupportedException ("Cannot insert into variable element index");
            }
        }

        protected void EmitInsertValue (IR.TypedValue aggregateValue, IR.TypedValue value, IR.Value[] indices)
        {
            EmitTypedLValue (aggregateValue);

            var t = aggregateValue.Type;
            var n = indices.Length;
            CecilInstruction seti = null;
            for (var i = 0; i < n; i++) {
                var index = (IR.Constant)indices[i];
                if (t.Resolve (module) is Types.LiteralStructureType st) {
                    var iindex = index.Int32Value;
                    var cst = compilation.GetClrType (st, module).Resolve ();
                    if (0 <= iindex && iindex < st.Elements.Length) {
                        var field = cst.Fields[iindex];
                        seti = il.Create (OpCodes.Stfld, field);
                        t = st.Elements[iindex];
                    }
                    else {
                        throw new InvalidOperationException ($"Cannot insert value #{i} from {st} ({t})");
                    }
                }
                else {
                    throw new InvalidOperationException ("Cannot insert value to " + t);
                }
            }

            if (seti != null) {
                Emit (OpCodes.Dup);
                EmitTypedValue (value);
                Emit (seti);
                Emit (il.Create (OpCodes.Ldobj, compilation.GetClrType(aggregateValue.Type, module: this.module)));
            }
            else {
                compilation.ErrorMessage (module.SourceFilename, $"No insertvalue indices");
                Emit (OpCodes.Pop);
            }
        }

        protected void EmitZeroValue(LType type)
        {
            if (type is VectorType vt) {
                var v = compilation.GetVectorType (vt, module: module);
                for (var i = 0; i < vt.Length; i++) {
                    EmitZeroValue (vt.ElementType);
                }
                Emit (il.Create (OpCodes.Newobj, v.Ctor));
            }
            else if (type is Types.IntegerType intt) {
                switch (Compilation.RoundUpIntBits (intt.Bits)) {
                    case 8:
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        Emit (il.Create (OpCodes.Conv_I1));
                        break;
                    case 16:
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        Emit (il.Create (OpCodes.Conv_I2));
                        break;
                    case 32:
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        break;
                    case 64:
                        Emit (il.Create (OpCodes.Ldc_I8, 0L));
                        break;
                    default:
                        throw new NotSupportedException ($"Cannot emit 0 for integer type `{type}`");
                }
            }
            else if (type is Types.PointerType ptr) {
                Emit (il.Create (OpCodes.Ldc_I4_0));
                Emit (il.Create (OpCodes.Conv_U));
            }
            else if (type is Types.FloatType floatt) {
                if (floatt.Bits == 32) {
                    Emit (il.Create (OpCodes.Ldc_R4, 0.0f));
                }
                else {
                    Emit (il.Create (OpCodes.Ldc_R8, 0.0));
                }
            }
            else if (type is NamedType namedType
                     && type.Resolve (module) is Types.LiteralStructureType st) {
                var td = compilation.GetClrType (type, module).Resolve ();
                var v = GetStructTempLocal (namedType);
                Emit (il.Create (OpCodes.Ldloca, v));
                Emit (il.Create (OpCodes.Initobj, td));
                Emit (il.Create (OpCodes.Ldloc, v));
            }
            else if (type is Types.LiteralStructureType lst) {
                var td = compilation.GetClrType (type, module).Resolve ();
                var v = GetStructTempLocal (lst);
                Emit (il.Create (OpCodes.Ldloca, v));
                Emit (il.Create (OpCodes.Initobj, td));
                Emit (il.Create (OpCodes.Ldloc, v));
            }
            else if (type is FunctionType) {
                Emit (il.Create (OpCodes.Ldc_I4_0));
                Emit (il.Create (OpCodes.Conv_U));
            }
            else {
                throw new NotSupportedException ($"Cannot get zero for {type} ({type.GetType().Name})");
            }
        }

        protected VariableDefinition GetVectorTempVariable (SimdVector type, IR.Value value, int uid)
        {
            //
            // First check if this value is already stored into a local
            // If so, just use that.
            //
            if (value is IR.LocalValue lv && locals.TryGetValue (lv.Symbol, out var vd))
                return vd;

            //
            // Ah, the value was inlined. Lookup/Allocate a register for it.
            //
            var key = (type.ClrType.FullName, uid);
            if (vectorTemps.TryGetValue (key, out vd))
                return vd;


            vd = new VariableDefinition (type.ClrType);
            vectorTemps[key] = vd;
            body.Variables.Add (vd);

            //var name = $"vectorTemp{vectorTemps.Count}";
            //var dbg = new VariableDebugInformation (vd, name);
            //vdbgs.Add (dbg);

            return vd;
        }

        protected VariableDefinition GetStructTempLocal (LType type)
        {
            switch (type) {
                case NamedType named:
                    return GetStructTempLocal (named);
                case LiteralStructureType literal:
                    return GetStructTempLocal (literal);
                default:
                    throw new NotSupportedException ($"Cannot get struct temp local for {type}");
            }
        }

        protected readonly Lazy<VariableDefinition> nativeExceptionLocal;        

        protected VariableDefinition GetStructTempLocal (NamedType type)
        {
            if (structTempLocals.TryGetValue (type.Symbol, out var v))
                return v;
            var t = compilation.GetClrType (type, module: this.module);
            v = new VariableDefinition (t);
            body.Variables.Add (v);
            structTempLocals[type.Symbol] = v;
            return v;
        }

        protected VariableDefinition GetStructTempLocal (LiteralStructureType type)
        {
            var key = type.Elements;
            if (astructTempLocals.TryGetValue (key, out var v))
                return v;
            var t = compilation.GetClrType (type, module: this.module);
            v = new VariableDefinition (t);
            body.Variables.Add (v);
            astructTempLocals[key] = v;
            return v;
        }

        protected VariableDefinition GetStructTempLocal (TypeReference type)
        {
            var key = type.FullName;
            if (trTempLocals.TryGetValue (key, out var v))
                return v;
            v = new VariableDefinition (type);
            body.Variables.Add (v);
            trTempLocals[key] = v;
            return v;
        }

        protected void EmitBox (LType type)
        {
            if (type is Types.PointerType) {
                Emit (il.Create (OpCodes.Call, compilation.sysIntPtrFromPointer));
                Emit (il.Create (OpCodes.Box, compilation.sysIntPtr));
            }
            else {
                Emit (il.Create (OpCodes.Box, compilation.GetClrType (type, module: this.module)));
            }
        }

        protected void EmitConsoleWriteLine (string message)
        {
            Emit (il.Create (OpCodes.Ldstr, message));
            Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
        }
    }
}
