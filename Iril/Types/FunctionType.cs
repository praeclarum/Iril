using System;
using System.Collections.Generic;
using System.Linq;
namespace Iril.Types
{
    public class FunctionType : LType
    {
        public readonly LType ReturnType;
        public readonly LType[] ParameterTypes;

        public FunctionType (LType returnType, IEnumerable<LType> parameterTypes)
        {
            ReturnType = returnType;
            ParameterTypes = parameterTypes.ToArray ();
        }

        public override string ToString () =>
            $"{ReturnType} ({String.Join(", ", (object[])ParameterTypes)})";

        public override long GetByteSize (Module module) => module.PointerByteSize;

        public override int GetAlignment (Module module) => module.PointerByteSize;

        public override bool StructurallyEquals (LType other) =>
            other is FunctionType a
            && ReturnType.StructurallyEquals (a.ReturnType)
            && ParametersEqual (a.ParameterTypes);

        bool ParametersEqual (LType[] parameterTypes)
        {
            if (ParameterTypes.Length != parameterTypes.Length)
                return false;
            for (var i = 0; i < ParameterTypes.Length; i++) {
                if (!ParameterTypes[i].StructurallyEquals (parameterTypes[i]))
                    return false;
            }
            return true;
        }

        public override int GetStructuralHashCode () =>
            234
            + ReturnType.GetStructuralHashCode ()
            + GetParametersHash ();

        int GetParametersHash ()
        {
            var h = ParameterTypes.Length.GetHashCode ();
            for (var i = 0; i < ParameterTypes.Length; i++) {
                h += ParameterTypes[i].GetStructuralHashCode ();
            }
            return h;
        }
    }
}
