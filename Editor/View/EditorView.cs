using System.Collections.Generic;

using Orbital.Data;
using Orbital.Core;

namespace Orbital.View
{
    internal sealed class EditorView : IView
    {
        #region Fields

        private readonly List<ItemLine> _lines = new List<ItemLine>();

        private readonly IDataSourceManager _dataSourceManager;
        private DataSourceInfo _currentSource;

        #endregion

        #region Constructors

        internal EditorView()
        {
            _dataSourceManager = ServiceProvider.Current.GetService<IDataSourceManager>();
        }

        #endregion

        #region Source Methods

        private void SwitchCurrentSource(string path)
        {
            DataSourceInfo source = _dataSourceManager.GetSource(path);
            UnbindDataSource();
            BindDataSource(source);

            CreateLinesFromSource();
        }

        private void BindDataSource(DataSourceInfo source)
        {
            _currentSource = source;

            if (_currentSource == null)
                return;

            _currentSource.ItemAdded += OnItemAdded;
            _currentSource.ItemRemoved += OnItemRemoved;
        }

        private void UnbindDataSource()
        {
            if (_currentSource == null)
                return;

            _currentSource.ItemAdded -= OnItemAdded;
            _currentSource.ItemRemoved -= OnItemRemoved;
        }

        private void OnItemAdded(object sender, DataSourceItemArgs e)
        {
            CreateLine(e.Item);
        }

        private void OnItemRemoved(object sender, DataSourceItemArgs e)
        {
            RemoveLine(e.Item);
        }

        #endregion

        #region Items Methods

        private void CreateLinesFromSource()
        {
            for (int i = 0; i < _currentSource.Data.Count; i++)
                CreateLine(_currentSource.Data[i]);
        }

        private void ResetsLines()
        {
            _lines.Clear();
        }

        private void CreateLine(IValueSource source)
        {
            ItemLine line = new ItemLine(source);
            _lines.Add(line);
        }

        private void RemoveLine(IValueSource source)
        {
            int index = _lines.FindIndex(c => c.DataSource == source);
            if (index < 0)
                return;

            RemoveLine(index);
        }

        private void RemoveLine(int index)
        {
            _lines.RemoveAt(index);
        }

        #endregion

        #region Draw Methods

        public void Draw()
        {
        }

        #endregion
    }
}