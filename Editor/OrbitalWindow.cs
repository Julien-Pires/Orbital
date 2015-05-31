using System.Linq;
using System.Collections.Generic;

using Orbital.UI;
using Orbital.Core;
using Orbital.View;
using Orbital.Source;
using Orbital.Reflection;

using UnityEditor;
using UnityEngine;

namespace Orbital
{
    public sealed class OrbitalWindow : EditorWindow
    {
        #region Fields

        private const string WindowName = "Orbital Editor";

        private int _currentTab;
        private readonly string[] _tabs;
        private Dictionary<string, IView> _views = new Dictionary<string, IView>
        {
            { "Editor", new EditorView() },
            { "Schema", null }
        };

        #endregion

        #region Constructors

        public OrbitalWindow()
        {
            title = WindowName;

            _tabs = _views.Keys.ToArray();

            ServiceProvider.Current = new ServiceProvider();
            ServiceProvider.Current.RegisterService<IDataSourceManager>(new DataSourceManager());
            ServiceProvider.Current.RegisterService<IVisualRendererManager>(new VisualRendererManager());
            ServiceProvider.Current.RegisterService<IAppDomainManager>(new AppDomainManager());

            CreateDomain();
            CreateUISelectors();
        }

        #endregion

        #region UI Methods

        public void OnGUI()
        {
            _currentTab = GUILayout.Toolbar(_currentTab, _tabs);

            IView view;
            if (!_views.TryGetValue(_tabs[_currentTab], out view))
                return;

            if (view == null)
                return;

            view.Draw();
        }

        private void CreateUISelectors()
        {
            IVisualRendererManager rendererManager = ServiceProvider.Current.GetService<IVisualRendererManager>();

            VisualSelector selector = new VisualSelector(typeof(PrimitiveVisual));
            selector.In(c => c.Type.Kind, new[] { TypeKind.Bool, TypeKind.Byte, TypeKind.Double, TypeKind.Float, TypeKind.Int,
                TypeKind.Long, TypeKind.Short, TypeKind.String });
            rendererManager.AddVisualFilter(selector);

            selector = new VisualSelector(typeof(ObjectVisual));
            selector.In(c => c.Type.Kind, new[] { TypeKind.Class, TypeKind.Struct });
            rendererManager.AddVisualFilter(selector);
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