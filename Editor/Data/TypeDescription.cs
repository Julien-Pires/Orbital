using System.Collections.Generic;

namespace Orbital.Data
{
    internal abstract class TypeDescription : BaseDescription
    {
        #region Fields

        private static readonly Dictionary<TypeKind, TypeDescription> PrimitiveTypeMap = new Dictionary<TypeKind, TypeDescription>
        {
            { TypeKind.Int, new PrimitiveDescription(typeof(int)) },
            { TypeKind.Short, new PrimitiveDescription(typeof(short)) },
            { TypeKind.Long, new PrimitiveDescription(typeof(long)) },
            { TypeKind.Float, new PrimitiveDescription(typeof(float)) },
            { TypeKind.Double, new PrimitiveDescription(typeof(double)) },
            { TypeKind.Byte, new PrimitiveDescription(typeof(byte)) },
            { TypeKind.String, new PrimitiveDescription(typeof(string)) }
        };

        #endregion

        #region Constructors

        protected TypeDescription(string name) : base(name)
        {
        }

        #endregion

        #region Search Methods

        internal static TypeDescription GetType(TypeKind kind)
        {
            TypeDescription type;
            if (PrimitiveTypeMap.TryGetValue(kind, out type))
                return type;

            return null;
        }

        #endregion
    }
}