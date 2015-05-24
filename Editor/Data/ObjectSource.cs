using System.Collections.Generic;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class ObjectSource : ValueSource
    {
        #region Fields

        private readonly List<IValueSource> _properties = new List<IValueSource>();

        #endregion

        #region Constructors

        internal ObjectSource(string name, ObjectDescription type, IValueSource source) : base(name, type, source)
        {
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