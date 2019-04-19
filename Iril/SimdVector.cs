using Mono.Cecil;

namespace Iril
{
    public class SimdVector
    {
        //public int Length;
        //public LType ElementType;
        public TypeReference ElementClrType;

        public TypeReference ClrType;

        public MethodReference Ctor;

        public FieldReference[] ElementFields;

        public MethodReference ToInt8, ToInt16, ToInt32, ToInt64;
        public MethodReference Add, Subtract;
        public MethodReference Multiply, Divide;

        public MethodReference FcmpOrderedGreaterThan, FcmpOrderedLessThan;
        public MethodReference IcmpNotEqual, IcmpSignedLessThan, IcmpSignedGreaterThan;

        public MethodReference Select;
    }
}
