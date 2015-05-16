using System;
using System.Collections.Generic;

namespace Orbital.Data
{
    internal abstract class TypeDescription : BaseDescription
    {
        #region Fields

        private static readonly Dictionary<Type, TypeKind> PrimitiveMap = new Dictionary<Type, TypeKind>
        {
            { typeof(int), TypeKind.Int},
            { typeof(Int32), TypeKind.Int},
            { typeof(short), TypeKind.Short},
            { typeof(Int16), TypeKind.Short},
            { typeof(long), TypeKind.Long},
            { typeof(Int64), TypeKind.Long},
            { typeof(byte), TypeKind.Byte},
            { typeof(Byte), TypeKind.Byte},
            { typeof(float), TypeKind.Float},
            { typeof(Single), TypeKind.Float},
            { typeof(double), TypeKind.Double},
            { typeof(Double), TypeKind.Double},
            { typeof(string), TypeKind.String},
            { typeof(String), TypeKind.String}
        };

        private static readonly Dictionary<TypeKind, TypeDescription> PrimitiveTypeMap = new Dictionary<TypeKind, TypeDescription>
        {
            { TypeKind.Int, new PrimitiveDescription(typeof(int), TypeKind.Int) },
            { TypeKind.Short, new PrimitiveDescription(typeof(short), TypeKind.Short) },
            { TypeKind.Long, new PrimitiveDescription(typeof(long), TypeKind.Long) },
            { TypeKind.Float, new PrimitiveDescription(typeof(float), TypeKind.Float) },
            { TypeKind.Double, new PrimitiveDescription(typeof(double), TypeKind.Double) },
            { TypeKind.Byte, new PrimitiveDescription(typeof(byte), TypeKind.Byte) },
            { TypeKind.String, new PrimitiveDescription(typeof(string), TypeKind.String) }
        };

        #endregion

        #region Properties

        public TypeKind Kind { get; private set; }

        #endregion

        #region Constructors

        protected TypeDescription(string name, TypeKind kind) : base(name)
        {
            Kind = kind;
        }

        #endregion

        #region Search Methods

        internal static TypeDescription GetPrimitiveType(Type type)
        {
            TypeKind kind;
            if (!PrimitiveMap.TryGetValue(type, out kind))
                return null;

            return GetPrimitiveType(kind);
        }

        internal static TypeDescription GetPrimitiveType(TypeKind kind)
        {
            TypeDescription type;
            if (!PrimitiveTypeMap.TryGetValue(kind, out type))
                return null;

            return type;
        }

        #endregion
    }
}