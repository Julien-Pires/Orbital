using Orbital.Source;

namespace Orbital.UI
{
    public abstract class SelectorFilter
    {
        #region Filter Methods

        public abstract bool Filter(IDataSource source);

        #endregion
    }
}
