using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class ObjectSource : ValueSource<ObjectDescription>, IParentSource
    {
        #region Fields

        private readonly List<IValueSource> _properties = new List<IValueSource>();
        private readonly ReadOnlyCollection<IValueSource> _readProperties;

        #endregion

        #region Properties

        public IList<IValueSource> Items
        {
            get { return _readProperties; }
        }

        public IValueSource this[string name]
        {
            get { return _properties.FirstOrDefault(c => c.Name == name); }
        }

        public IValueSource PrimaryKey
        {
            get
            {
                PropertyDescription primaryKey = Type.PrimaryKey;

                return (primaryKey != null) ? _properties.FirstOrDefault(c => c.Name == primaryKey.Name) : null;
            }
        }

        #endregion

        #region Constructors

        internal ObjectSource(string name, ObjectDescription type, IValueSource source) : base(name, type, source)
        {
            _readProperties = new ReadOnlyCollection<IValueSource>(_properties);

            ExtractProperties();
        }

        #endregion

        #region Properties Methods

        private void ExtractProperties()
        {
            object parent = GetValue();
            foreach(string name in Type.PropertiesName)
            {
                PropertyDescription propertyDescription = Type[name];
                PropertySource propertySource = new PropertySource(parent, propertyDescription);
                IValueSource source = DataSourceHelper.CreateSource(propertyDescription.Name, propertyDescription.TypeDescription, propertySource);
                _properties.Add(source);
            }
        }

        #endregion
    }
}