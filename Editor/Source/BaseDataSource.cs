using System;
using System.IO;

namespace Orbital.Source
{
    public abstract class BaseDataSource : IDataSource
    {
        #region IDataSource Implementation

        public abstract object GetData(string filename, Type type);

        #endregion

        #region IO Methods

        protected string GetText(string filename)
        {
            return File.Exists(filename) ?  File.ReadAllText(filename) : null;
        }

        protected byte[] GetBytes(string filename)
        {
            return File.Exists(filename) ? File.ReadAllBytes(filename) : null;
        }

        #endregion
    }
}