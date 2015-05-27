using Orbital.UI;
using Orbital.Core;
using Orbital.Source;
using Orbital.Reflection;

using UnityEditor;
using UnityEngine;

namespace Orbital
{
    public sealed class OrbitalWindow : EditorWindow
    {
        #region Constructors

        public OrbitalWindow()
        {
            ServiceProvider.Current = new ServiceProvider();
            ServiceProvider.Current.RegisterService<IDataSourceManager>(new DataSourceManager());
            ServiceProvider.Current.RegisterService<IVisualRendererManager>(new VisualRendererManager());
            ServiceProvider.Current.RegisterService<IAppDomainManager>(new AppDomainManager());

            CreateDomain();
        }

        #endregion

        #region UI Methods

        public void OnGUI()
        {
        }

        #endregion

        #region Application Domain Methods

        private void CreateDomain()
        {
            IAppDomainManager appDomainManager = ServiceProvider.Current.GetService<IAppDomainManager>();
            appDomainManager.CreateDomain(Application.productName);
        }

        #endregion

        #region Data Source Methods

        public void AddDataSource(string extension, IDataSource dataSource)
        {
            IDataSourceManager dataSourceManager = ServiceProvider.Current.GetService<IDataSourceManager>();
            dataSourceManager.AddDataSource(extension, dataSource);
        }

        public void AddDataSource(string originalExtension, string newExtension)
        {
            IDataSourceManager dataSourceManager = ServiceProvider.Current.GetService<IDataSourceManager>();
            IDataSource dataSource = dataSourceManager.GetDataSource(originalExtension);
            if(dataSource == null)
                return;

            dataSourceManager.AddDataSource(newExtension, dataSource);
        }

        #endregion
    }
}