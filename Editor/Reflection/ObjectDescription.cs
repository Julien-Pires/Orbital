using System;
using System.Linq;
using System.Collections.Generic;

namespace Orbital.Reflection
{
    public sealed class ObjectDescription : TypeDescription
    {
        #region Fields

        private readonly Dictionary<string, PropertyDescription> _properties = new Dictionary<string, PropertyDescription>();

        #endregion

        #region Properties

        public int PropertiesCount
        {
            get { return _properties.Count; }
        }

        public IEnumerable<string> PropertiesName
        {
            get { return _properties.Keys; }
        }

        public bool IsClass
        {
            get { return Kind == TypeKind.Class; }
        }

        public bool IsStruct
        {
            get { return Kind == TypeKind.Struct; }
        }

        public PropertyDescription PrimaryKey
        {
            get { return _properties.Values.Where(c => c.IsPrimaryKey).FirstOrDefault(); }
        }

        #endregion

        #region Indexers

        public PropertyDescription this[string name]
        {
            get { return _properties[name]; }
            set
            {
                if (value != null)
                    AddProperty(value);
                else
                    RemoveProperty(name);
            }
        }

        #endregion

        #region Constructors

        internal ObjectDescription(string name, TypeKind kind, Type clrType)
            : base(name, kind, clrType)
        {
        }

        #endregion

        #region Object Properties Methods

        internal void AddProperty(PropertyDescription property)
        {
            if(property == null)
                throw new ArgumentNullException("property");

            _properties[property.Name] = property;
        }

        internal bool RemoveProperty(string name)
        {
            return _properties.Remove(name);
        }

        #endregion
    }
}