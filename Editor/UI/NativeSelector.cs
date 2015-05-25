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
            { typeof(PrimitiveValueSource), typeof(PrimitiveVisual) },
            { typeof(ObjectSource), typeof(ObjectVisual) }
        };

        #endregion

        #region Selector Methods

        public Type GetRenderer(IValueSource source)
        {
            Type type;
            if (!_typeMap.TryGetValue(source.Type.GetType(), out type))
                return null;

            return type;
        }

        #endregion
    }
}