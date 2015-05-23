using System.Collections.Generic;

using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class CollectionSource : ValueSource
    {
        #region Fields

        private readonly List<CollectionItemSource> _items = new List<CollectionItemSource>();

        #endregion

        #region Constructors

        internal CollectionSource(string name, TypeDescription type, IValueSource source) : base(name, type, source)
        {
            ExtractItems();
        }

        #endregion

        #region Items Methods

        private void ExtractItems()
        {
        }

        #endregion
    }
}
