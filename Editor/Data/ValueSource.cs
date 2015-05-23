using System;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal class ValueSource : IValueSource
    {
        #region Fields

        private readonly string _name;
        private readonly IValueSource _source;
        private readonly TypeDescription _type;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
        }

        public Type CLRType
        {
            get { return _type.CLRType; }
        }

        public TypeDescription Type
        {
            get { return _type; }
        }

        #endregion

        #region Constructors

        internal ValueSource(string name, TypeDescription type, IValueSource source)
        {
            _name = name;
            _type = type;
            _source = source;
        }

        #endregion

        #region Value Source Methods

        public object GetValue()
        {
            return _source.GetValue();
        }

        public void SetValue(object value)
        {
            _source.SetValue(value);
        }

        public T GetValue<T>()
        {
            return _source.GetValue<T>();
        }

        public void SetValue<T>(T value)
        {
            _source.SetValue(value);
        }

        #endregion
    }
}