using Orbital.Reflection;

namespace Orbital.Data
{
    public interface IDataSourceManager
    {
        #region Methods

        bool SourceExists(string path);

        void AddSource(string path, ObjectDescription type);

        #endregion
    }
}