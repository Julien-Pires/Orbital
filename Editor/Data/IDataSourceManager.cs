using Orbital.Reflection;

namespace Orbital.Data
{
    internal interface IDataSourceManager
    {
        #region Methods

        bool SourceExists(string path);

        DataSourceInfo GetSource(string path);

        void AddSource(string path, ObjectDescription type);

        #endregion
    }
}