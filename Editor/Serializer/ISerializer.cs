using System;

namespace Orbital.Serializer
{
    public interface ISerializer
    {
        #region Methods

        object GetData(string filename, Type type);

        #endregion
    }
}