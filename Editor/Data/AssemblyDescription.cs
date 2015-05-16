using System;
using System.Collections.Generic;

namespace Orbital.Data
{
    using NamespaceMap = Dictionary<string, TypeDescription>;

    internal sealed class AssemblyDescription : BaseDescription
    {
        #region Fields

        private readonly Dictionary<string, NamespaceMap> _namespacesMap = new Dictionary<string, NamespaceMap>();

        #endregion

        #region Constructors

        internal AssemblyDescription(string name) : base(name)
        {
        }

        #endregion

        #region Namespace Management Methods

        private NamespaceMap EnsureNamespace(string namespaceName)
        {
            if(namespaceName == null)
                throw new ArgumentNullException("namespaceName");

            NamespaceMap map;
            if (!_namespacesMap.TryGetValue(namespaceName, out map))
            {
                map = new NamespaceMap();
                _namespacesMap.Add(namespaceName, map);
            }

            return map;
        }

        #endregion

        #region Type Management Methods

        public void RegisterType(string namespaceName, TypeDescription typeDescription)
        {
            if(namespaceName == null)
                throw new ArgumentNullException("namespaceName");

            if(typeDescription == null)
                throw new ArgumentNullException("typeDescription");

            NamespaceMap map = EnsureNamespace(namespaceName);
            map[typeDescription.Name] = typeDescription;
        }

        public bool ContainsType(string fullname)
        {
            string namespaceName, typeName;
            DescriptionHelper.ExtractNames(fullname, out namespaceName, out typeName);

            return ContainsType(namespaceName, typeName);
        }

        public bool ContainsType(string namespaceName, string name)
        {
            NamespaceMap typeMap;
            if (!_namespacesMap.TryGetValue(namespaceName, out typeMap))
                return false;

            return typeMap.ContainsKey(name);
        }

        public TypeDescription GetType(string fullname)
        {
            string namespaceName, typeName;
            DescriptionHelper.ExtractNames(fullname, out namespaceName, out typeName);

            return GetType(namespaceName, typeName);
        }

        public TypeDescription GetType(string namespaceName, string name)
        {
            NamespaceMap typeMap;
            if (!_namespacesMap.TryGetValue(namespaceName, out typeMap))
                return null;

            TypeDescription type;
            if (!typeMap.TryGetValue(name, out type))
                return null;

            return type;
        }

        #endregion
    }
}