using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class CollectionSource : ValueSource, ICollectionSource
    {
        #region Fields

        private readonly List<IValueSource> _items = new List<IValueSource>();
        private readonly ReadOnlyCollection<IValueSource> _readItems;

        #endregion

        #region Properties

        public new CollectionDescription Type
        {
            get { return (CollectionDescription)base.Type; }
        }

        public int Count
        {
            get
            {
                ICollection collection = GetValue() as ICollection;
                if (collection == null)
                    throw new InvalidOperationException(string.Format("{0} does not implement ICollection", Type.CLRType));

                return collection.Count;
            }
        }

        public IList<IValueSource> Items
        {
            get { return _readItems; }
        }

        public TypeDescription ItemType
        {
            get { return Type.IsKeyed ? Type.ItemTypes[1] : Type.ItemTypes[0]; }
        }

        #endregion

        #region Constructors

        internal CollectionSource(string name, TypeDescription type, IValueSource source) : base(name, type, source)
        {
            _readItems = new ReadOnlyCollection<IValueSource>(_items);

            ExtractItems();
        }

        #endregion

        #region Items Methods

        private void ExtractItems()
        {
            List<object> indexes = GetIndexes();
            for (int i = 0; i < indexes.Count; i++)
                AddItem(indexes[i]);
        }

        private object CreateItem()
        {
            return Activator.CreateInstance(ItemType.CLRType);
        }

        public void AddItem(object index)
        {
            SetValue(index, CreateItem());

            CollectionItemSource item = new CollectionItemSource(this) { Index = index };
            IValueSource source = DataSourceHelper.CreateSource(index.ToString(), ItemType, item);
            _items.Add(source);
        }

        public List<object> GetIndexes()
        {
            List<object> indexes = new List<object>(Count);
            if (Type.IsList)
            {
                IList list = GetValue<IList>();
                for (int i = 0; i < list.Count; i++)
                    indexes.Add(i);
            }
            else if (Type.IsKeyed)
            {
                IDictionary dictionary = GetValue<IDictionary>();
                indexes.AddRange(from DictionaryEntry entry in dictionary select entry.Key);
            }

            return indexes;
        }

        public object GetValue(object index)
        {
            object value = null;
            if (Type.IsList)
                value = GetValue<IList>()[(int)index];
            else if (Type.IsKeyed)
                value = GetValue<IDictionary>()[index];

            return value;
        }

        public void SetValue(object index, object value)
        {
            if (Type.IsList)
                GetValue<IList>()[(int)index] = value;
            else if (Type.IsKeyed)
                GetValue<IDictionary>()[index] = value;
        }

        #endregion
    }
}