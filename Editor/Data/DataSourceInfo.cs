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
        }

        internal void RemoveItem(string primaryKey)
        {
            _datas.RemoveAll(c => c.PrimaryKey.GetValue<string>() == primaryKey);
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