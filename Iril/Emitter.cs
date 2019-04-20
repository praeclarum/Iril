using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Iril.Types;
using CecilInstruction = Mono.Cecil.Cil.Instruction;
using System.Linq;
using System.Collections.Generic;

namespace Iril
{
    abstract class Emitter
    {
        // Input
        protected readonly Compilation compilation;
        protected readonly Module module;
        protected readonly MethodDefinition method;

        // Created
        protected readonly MethodBody body;
        protected readonly ILProcessor il;

        // Working Variables
        protected CecilInstruction prev;

        readonly SymbolTable<VariableDefinition> structTempLocals = new SymbolTable<VariableDefinition> ();
        readonly Dictionary<TypeReference[], VariableDefinition> astructTempLocals = new Dictionary<TypeReference[], VariableDefinition> (AnonymousStruct.TypesEquality);

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

        protected void EmitTypedValue(IR.TypedValue value)
        {
            EmitValue(value.Value, value.Type);
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
                        throw new NotSupportedException($"Undeclared global variable `{IR.MangledName.Demangle (g.Symbol)}` ({g.Symbol})");
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
                        switch (((IntegerType)type).Bits)
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
                case IR.IntegerConstant i:
                    switch (((IntegerType)type).Bits)
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
                    break;
                case IR.IntToPointerValue itop:
                    EmitTypedValue(itop.Value);
                    Emit(il.Create(OpCodes.Conv_U));
                    break;
                case IR.LocalValue local:
                    EmitLocalValue(local);
                    break;
                case IR.NullConstant nll:
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Conv_U));
                    break;
                case IR.PtrtointValue i:
                    EmitTypedValue(i.Value);
                    switch (((IntegerType)i.Type).Bits)
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
                        var vt = compilation.GetVectorType((VectorType)type);
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

        protected virtual void EmitLocalValue(IR.LocalValue local)
        {
            throw new NotSupportedException();
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
                            EmitValue(new IR.IntegerConstant(esize * ic.Int32Value), index.Type);
                        }
                        else
                        {
                            EmitValue(index.Value, index.Type);
                            EmitValue(new IR.IntegerConstant(esize), index.Type);
                            Emit(il.Create(OpCodes.Mul));
                        }
                        Emit(il.Create(OpCodes.Conv_U));
                        Emit(il.Create(OpCodes.Add));
                    }
                }
                else if (t is NamedType
                         && t.Resolve(module) is Types.LiteralStructureType st
                         && index.Value is IR.IntegerConstant iconst)
                {
                    var cst = compilation.GetClrType(t).Resolve();
                    var field = cst.Fields[(int)iconst.Value];
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
                    EmitValue(index.Value, index.Type);
                    EmitValue(new IR.IntegerConstant(esize), index.Type);
                    Emit(il.Create(OpCodes.Mul));
                    Emit(il.Create(OpCodes.Conv_U));
                    Emit(il.Create(OpCodes.Add));
                    t = artt.ElementType;
                }
                else
                {
                    if (i + 1 < n) {
                        throw new InvalidOperationException("Cannot get pointer to " + t);
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
            if (offset > 0) {
                //Console.WriteLine ("EMIT " + offset);
                if (offset < int.MaxValue) {
                    EmitValue (new IR.IntegerConstant (offset), Types.IntegerType.I32);
                }
                else {
                    EmitValue (new IR.IntegerConstant (offset), Types.IntegerType.I64);
                }
                Emit (il.Create (OpCodes.Conv_U));
                Emit (il.Create (OpCodes.Add));
            }
            else {
                //Console.WriteLine ("SKIP ZERO");
            }
            return true;
        }

        protected void EmitZeroValue(LType type)
        {
            if (type is VectorType vt) {
                var v = compilation.GetVectorType (vt);
                for (var i = 0; i < vt.Length; i++) {
                    EmitZeroValue (vt.ElementType);
                }
                Emit (il.Create (OpCodes.Newobj, v.Ctor));
            }
            else if (type is Types.IntegerType intt) {
                switch (intt.Bits) {
                    default:
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        break;
                    case 64:
                        Emit (il.Create (OpCodes.Ldc_I8, 0L));
                        break;
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
                var td = compilation.GetClrType (type).Resolve ();
                var v = GetStructTempLocal (namedType);
                Emit (il.Create (OpCodes.Ldloca, v));
                Emit (il.Create (OpCodes.Initobj, td));
                Emit (il.Create (OpCodes.Ldloc, v));
            }
            else if (type is FunctionType) {
                Emit (il.Create (OpCodes.Ldc_I4_0));
                Emit (il.Create (OpCodes.Conv_U));
            }
            else {
                throw new NotSupportedException ("Cannot get zero for " + type);
            }
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
            var t = compilation.GetClrType (type);
            v = new VariableDefinition (t);
            body.Variables.Add (v);
            structTempLocals[type.Symbol] = v;
            return v;
        }

        protected VariableDefinition GetStructTempLocal (LiteralStructureType type)
        {
            var key = type.Elements.Select (x => compilation.GetClrType (x)).ToArray ();
            if (astructTempLocals.TryGetValue (key, out var v))
                return v;
            var t = compilation.GetClrType (type);
            v = new VariableDefinition (t);
            body.Variables.Add (v);
            astructTempLocals[key] = v;
            return v;
        }

        protected void EmitBox (LType type)
        {
            Emit (il.Create (OpCodes.Box, compilation.GetClrType (type)));
        }
    }
}
