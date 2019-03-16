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
    }
}
