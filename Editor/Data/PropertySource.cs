using System;
using System.Reflection;

using Orbital.Reflection;

namespace Orbital.Data
{
    public class PropertySource : IValueSource
    {
        #region Fields

        private const BindingFlags FieldFlags = BindingFlags.Instance | BindingFlags.Public;

        private readonly object _parent;
        private readonly PropertyDescription _property;
        private readonly MemberInfo _member;

        #endregion

        #region Properties

        public string Name
        {
            get { return _property.Name; }
        }

        public TypeDescription Type
        {
            get { return _property.TypeDescription; }
        }

        public Type CLRType
        {
            get { return _property.TypeDescription.CLRType; }
        }

        #endregion

        #region Constructors

        internal PropertySource(object parent, PropertyDescription property)
        {
            _parent = parent;
            _property = property;

            if (_property.IsField)
                _member = _parent.GetType().GetField(_property.Name, FieldFlags);
            else
                _member = _parent.GetType().GetProperty(_property.Name, FieldFlags);
        }

        #endregion

        public T GetValue<T>()
        {
            object value = _property.IsField ? ((FieldInfo) _member).GetValue(_parent) : ((PropertyInfo) _member).GetValue(_parent, null);

            return (T) value;
        }

        public void SetValue<T>(T value)
        {
            if(_property.IsField)
                ((FieldInfo)_member).SetValue(_parent, value);
            else
                ((PropertyInfo)_member).SetValue(_parent, value, null);
        }
    }
}