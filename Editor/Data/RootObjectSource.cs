using System;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed partial class DataSourceInfo
    {
        private sealed class RootObjectSource : IValueSource
        {
            #region Fields

            private object _item;
            private readonly DataSourceInfo _sourceInfo;

            #endregion

            #region Properties

            public Type CLRType
            {
                get { return _sourceInfo.Type.CLRType; }
            }

            public string Name
            {
                get { return string.Empty; }
            }

            public TypeDescription Type
            {
                get { return _sourceInfo.Type; }
            }

            public IValueSource UnderlyingSource
            {
                get { return null; }
            }

            #endregion

            #region Cnonstructors

            internal RootObjectSource(DataSourceInfo sourceInfo, object item)
            {
                _sourceInfo = sourceInfo;
                _item = item;
            }

            #endregion

            #region Value Methods

            public object GetValue()
            {
                return _item;
            }

            public T GetValue<T>()
            {
                return (T)_item;
            }

            public void SetValue(object value)
            {
                _item = value;
            }

            public void SetValue<T>(T value)
            {
                _item = value;
            }

            #endregion
        }
    }
}