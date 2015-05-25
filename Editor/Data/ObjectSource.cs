using System.Collections.Generic;
using System.Collections.ObjectModel;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class ObjectSource : ValueSource, IParentSource
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
            ObjectDescription objDescription = (ObjectDescription)Type;
            foreach(string name in objDescription.PropertiesName)
            {
                PropertyDescription propertyDescription = objDescription[name];
                PropertySource propertySource = new PropertySource(parent, propertyDescription);
                IValueSource source = DataSourceHelper.CreateSource(propertyDescription.Name, propertyDescription.TypeDescription, propertySource);
                _properties.Add(source);
            }
        }

        #endregion
    }
}