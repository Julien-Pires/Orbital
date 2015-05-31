using System;

using Orbital.Data;

namespace Orbital.UI
{
    internal sealed class InternalSelectorFilter : SelectorFilter 
    {
        #region Fields

        private readonly Func<object, object, bool> _filterFunc;
        private readonly Func<IValueSource, object> _getMemberFunc;
        private readonly object _value;

        #endregion

        #region Constructors

        internal InternalSelectorFilter(Func<object, object, bool> filterFunc, Func<IValueSource, object> memberFunc, object value)
        {
            _filterFunc = filterFunc;
            _value = value;
        }

        #endregion

        #region Filter Methods

        public override bool Filter(IValueSource source)
        {
            object value = _getMemberFunc(source);

            return _filterFunc(value, _value);
        }

        #endregion
    }
}
