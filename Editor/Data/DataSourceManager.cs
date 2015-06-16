using System;
using System.Linq;
using System.Collections.Generic;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class DataSourceManager : IDataSourceManager
    {
        #region Fields

        private readonly List<DataSourceInfo> _sources = new List<DataSourceInfo>();

        #endregion

        #region Data Source Methods

        public bool SourceExists(string path)
        {
            return _sources.Any(c => c.SourceInfo.Path == path);
        }

        public void AddSource(string path, ObjectDescription type)
        {
            if (SourceExists(path))
                throw new ArgumentException("File {0} already exists", path);

            DataSourceInfo dataSource = new DataSourceInfo(path, type);
            dataSource.EnsureFileExists();
        }

        #endregion
    }
}