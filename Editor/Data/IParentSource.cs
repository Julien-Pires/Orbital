using System.Collections.Generic;

namespace Orbital.Data
{
    public interface IParentSource
    {
        #region Properties

        IList<IValueSource> Items { get; }

        #endregion
    }
}