using System.Collections.Generic;
using Mono.Cecil;

namespace Iril
{
    public class AnonymousStruct
    {
        //public int Length;
        //public LType ElementType;
        public TypeReference[] ElementClrTypes;

        public TypeDefinition ClrType;

        public FieldReference[] ElementFields;

        public static readonly IEqualityComparer<TypeReference[]> TypesEquality = new TEq ();
        public static readonly IEqualityComparer<Types.LType[]> LTypesEquality = new LTEq ();

        class TEq : IEqualityComparer<TypeReference[]>
        {
            public bool Equals (TypeReference[] x, TypeReference[] y)
            {
                if (x.Length != y.Length)
                    return false;
                for (int i = 0; i < x.Length; i++)
                {
                    var t = x[i];
                    var u = y[i];
                    if (t.FullName != u.FullName)
                        return false;
                }
                return true;
            }

            public int GetHashCode (TypeReference[] x)
            {
                var h = 0;
                foreach (var t in x) {
                    h += t.FullName.GetHashCode ();
                }
                return h;
            }
        }

        class LTEq : IEqualityComparer<Types.LType[]>
        {
            public bool Equals (Types.LType[] x, Types.LType[] y)
            {
                if (x.Length != y.Length)
                    return false;
                for (int i = 0; i < x.Length; i++) {
                    var t = x[i];
                    var u = y[i];
                    if (!t.StructurallyEquals (u))
                        return false;
                }
                return true;
            }

            public int GetHashCode (Types.LType[] x)
            {
                var h = 0;
                foreach (var t in x) {
                    h += t.GetStructuralHashCode ();
                }
                return h;
            }
        }
    }
}
