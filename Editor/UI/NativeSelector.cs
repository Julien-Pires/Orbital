using System;
using System.Collections.Generic;

using Orbital.Data;

namespace Orbital.UI
{
    internal sealed class NativeSelector : IUISelector
    {
        #region Fields

        private static readonly Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>
        {
            { typeof(PrimitiveValueSource), typeof(PrimitiveVisual) }
        };

        #endregion

        #region Selector Methods

        public Type GetRenderer(IValueSource source)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}