using System;

namespace Orbital.Data
{
    internal sealed class PrimitiveDescription : TypeDescription
    {
        #region Fields

        private readonly Type _type;

        #endregion

        #region Properties

        public Type ClrType
        {
            get { return _type; }
        }

        #endregion

        #region Constructors

        internal PrimitiveDescription(Type type) : base(type.Name)
        {
            _type = type;
        }

        #endregion
    }
}