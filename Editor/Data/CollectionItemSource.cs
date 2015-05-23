using System;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class CollectionItemSource : IValueSource
    {
        #region Fields

        private readonly TypeDescription _type;
        private readonly string _name;

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

        internal CollectionItemSource(string name, TypeDescription type)
        {
            _name = name;
            _type = type;
        }

        #endregion

        public object GetValue()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>()
        {
            throw new NotImplementedException();
        }

        public void SetValue(object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue<T>(T value)
        {
            throw new NotImplementedException();
        }
    }
}