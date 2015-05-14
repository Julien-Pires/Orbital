using System;
using System.Collections.Generic;

namespace Orbital.Data
{
    internal sealed class ObjectDescription : TypeDescription
    {
        #region Fields

        private readonly Dictionary<string, PropertyDescription> _properties = new Dictionary<string, PropertyDescription>();

        #endregion

        #region Properties

        public bool IsClass
        {
            get { return Kind == TypeKind.Class; }
        }

        public bool IsStruct
        {
            get { return Kind == TypeKind.Struct; }
        }

        #endregion

        #region Constructors

        internal ObjectDescription(string name, TypeKind kind) : base(name, kind)
        {
        }

        #endregion

        #region Object Properties Methods

        public void AddProperty(PropertyDescription property)
        {
            if(property == null)
                throw new ArgumentNullException("property");

            _properties[property.Name] = property;
        }

        public bool RemoveProperty(string name)
        {
            return _properties.Remove(name);
        }

        #endregion
    }
}