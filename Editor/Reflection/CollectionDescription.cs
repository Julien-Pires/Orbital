using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Orbital.Reflection
{
    internal sealed class CollectionDescription : TypeDescription
    {
        #region Fields

        private readonly List<TypeDescription> _itemTypes = new List<TypeDescription>();
        private readonly ReadOnlyCollection<TypeDescription> _itemTypesReadOnly;

        #endregion

        #region Properties

        public IList<TypeDescription> ItemTypes
        {
            get { return _itemTypesReadOnly; }
        }

        public bool IsKeyed
        {
            get { return Kind == TypeKind.Dictionary; }
        }

        public bool IsArray
        {
            get { return Kind == TypeKind.Array; }
        }

        #endregion

        #region Constructors

        internal CollectionDescription(string name, TypeKind kind, Type clrType)
            : base(name, kind, clrType)
        {
            _itemTypesReadOnly = new ReadOnlyCollection<TypeDescription>(_itemTypes);
        }

        #endregion

        #region Type Methods

        internal void AddType(TypeDescription type)
        {
            _itemTypes.Add(type);
        }

        #endregion
    }
}