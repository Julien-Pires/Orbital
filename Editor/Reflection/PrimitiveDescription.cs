using System;

namespace Orbital.Reflection
{
    public sealed class PrimitiveDescription : TypeDescription
    {
        #region Constructors

        internal PrimitiveDescription(Type clrType, TypeKind kind)
            : base(clrType.Name, kind, clrType)
        {
        }

        #endregion
    }
}