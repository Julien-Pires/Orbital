using System.Reflection;

namespace Orbital.Reflection
{
    internal sealed class ObjectProperty : IPropertySource
    {
        #region Fields

        private readonly object _parent;
        private readonly PropertyInfo _property;

        #endregion

        #region Properties

        public object Value
        {
            get { return _property.GetValue(_parent, null); }
            set { _property.SetValue(_parent, value, null); }
        }

        #endregion

        #region Constructors

        internal ObjectProperty(object parent, string propertyName)
        {
            _parent = parent;
            _property = parent.GetType().GetProperty(propertyName);
        }

        #endregion
    }
}