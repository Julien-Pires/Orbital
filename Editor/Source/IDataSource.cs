using System;

namespace Orbital.Source
{
    public interface IDataSource
    {
        #region Methods

        object GetData(string filename, Type type);

        #endregion
    }
}