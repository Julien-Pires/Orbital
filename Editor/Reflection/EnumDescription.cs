using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Orbital.Reflection
{
    public sealed class EnumDescription : TypeDescription
    {
        #region Fields

        private readonly ReadOnlyCollection<string> _values;

        #endregion

        #region Properties

        public IList<string> Values
        {
            get { return _values; }
        }

        #endregion

        #region Constructors

        internal EnumDescription(string name, IList<string> values, Type clrType)
            : base(name, TypeKind.Enum, clrType)
        {
            _values = new ReadOnlyCollection<string>(values);
        }

        #endregion
    }
}