using System;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class CollectionItemSource : IValueSource
    {
        #region Fields

        private readonly ICollectionSource _collectionSource;

        #endregion

        #region Properties

        public string Name { get; internal set; }

        public object Index { get; internal set; }

        public Type CLRType
        {
            get { return Type.CLRType; }
        }

        public TypeDescription Type
        {
            get { return _collectionSource.Type.ItemTypes[0]; }
        }

        public Type KeyCLRType
        {
            get { return (KeyType != null) ? KeyType.CLRType : null; }
        }

        public TypeDescription KeyType
        {
            get { return _collectionSource.Type.IsKeyed ? _collectionSource.Type.ItemTypes[1] : null; }
        }

        public IValueSource UnderlyingSource
        {
            get { return _collectionSource; }
        }

        #endregion

        #region Constructors

        internal CollectionItemSource(ICollectionSource source)
        {
            _collectionSource = source;
        }

        #endregion

        #region Value Source Methods

        public object GetValue()
        {
            return _collectionSource.GetValue(Index);
        }

        public T GetValue<T>()
        {
            return (T)_collectionSource.GetValue(Index);
        }

        public void SetValue(object value)
        {
            _collectionSource.SetValue(Index, value);
        }

        public void SetValue<T>(T value)
        {
            _collectionSource.SetValue(Index, value);
        }

        #endregion
    }
}