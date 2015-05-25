using System;

using Orbital.Reflection;

namespace Orbital.Data
{
    public interface IValueSource
    {
        #region Properties

        string Name { get; }

        TypeDescription Type { get; }

        Type CLRType { get; }

        IValueSource UnderlyingSource { get; }

        #endregion

        #region Methods

        object GetValue();

        void SetValue(object value);

        T GetValue<T>();

        void SetValue<T>(T value);

        #endregion
    }
}