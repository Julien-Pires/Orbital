using Orbital.Source;

using UnityEditor;

namespace Orbital
{
    public sealed class OrbitalWindow : EditorWindow
    {
        #region Fields

        private readonly DataSourceManager _dataSourceManager = new DataSourceManager();

        #endregion

        #region UI Methods

        public void OnGUI()
        {
            
        }

        #endregion

        #region Data Source Methods

        public void AddDataSource(string extension, IDataSource dataSource)
        {
            _dataSourceManager.AddDataSource(extension, dataSource);
        }

        public void AddDataSource(string originalExtension, string newExtension)
        {
            IDataSource dataSource = _dataSourceManager.GetDataSource(originalExtension);
            if(dataSource == null)
                return;

            _dataSourceManager.AddDataSource(newExtension, dataSource);
        }

        #endregion
    }
}