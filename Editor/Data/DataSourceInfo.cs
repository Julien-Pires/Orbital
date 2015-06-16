using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed partial class DataSourceInfo
    {
        #region Fields

        private readonly DataSource _source = new DataSource();
        private readonly ObjectDescription _type;
        private readonly List<ObjectSource> _datas = new List<ObjectSource>();

        #endregion

        #region Properties

        internal DataSource SourceInfo
        {
            get { return _source; }
        }

        internal ObjectDescription Type
        {
            get { return _type; }
        }

        #endregion

        #region Events

        public event EventHandler<DataSourceItemArgs> ItemAdded;

        public event EventHandler<DataSourceItemArgs> ItemRemoved;

        #endregion

        #region Events Invokers

        private void OnItemAdded(IValueSource item)
        {
            DataSourceItemArgs args = new DataSourceItemArgs(item);
            EventHandler<DataSourceItemArgs> handler = ItemAdded;
            if (handler != null)
                handler(this, args);
        }

        private void OnItemRemoved(IValueSource item)
        {
            DataSourceItemArgs args = new DataSourceItemArgs(item);
            EventHandler<DataSourceItemArgs> handler = ItemRemoved;
            if (handler != null)
                handler(this, args);
        }

        #endregion

        #region Constructors

        internal DataSourceInfo(string path, ObjectDescription type)
        {
            _source.Path = path;

            Type clrType = type.CLRType;
            _source.Assembly = clrType.Assembly.GetName().Name;
            _source.Type = clrType.FullName;
        }

        #endregion

        #region Items Methods

        internal bool ItemExists(string primaryKey)
        {
            return _datas.Any(c => c.PrimaryKey.GetValue<string>() == primaryKey);
        }

        internal void AddItem(string primaryKey)
        {
            if (ItemExists(primaryKey))
                throw new ArgumentException(string.Format("An item with id {0} already exists", primaryKey));

            object item = Activator.CreateInstance(_type.CLRType, (BindingFlags.Instance | BindingFlags.Public), null, null, 
                CultureInfo.CurrentCulture);
            RootObjectSource rootObject = new RootObjectSource(this, item);
            ObjectSource source = new ObjectSource(primaryKey, _type, rootObject);
            source.PrimaryKey.SetValue(primaryKey);

            _datas.Add(source);

            OnItemAdded(source);
        }

        internal void RemoveItem(string primaryKey)
        {
            ObjectSource source = _datas.FirstOrDefault(c => c.PrimaryKey.GetValue<string>() == primaryKey);
            if (source == null)
                return;

            _datas.RemoveAll(c => c.PrimaryKey.GetValue<string>() == primaryKey);

            OnItemRemoved(source);
        }

        #endregion

        #region File Methods

        internal void EnsureFileExists()
        {
            if (File.Exists(_source.Path))
                return;

            File.Create(_source.Path).Dispose();
        }

        #endregion
    }
}