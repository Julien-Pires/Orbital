using System.Collections.Generic;

using Orbital.Reflection;

namespace Orbital.Data
{
    public interface ICollectionSource : IValueSource, IParentSource
    {
        #region Properties

        new CollectionDescription Type { get; }

        int Count { get; }

        #endregion

        #region Collection Methods

        List<object> GetIndexes();

        object GetValue(object index);

        void SetValue(object index, object value);

        #endregion
    }
}
