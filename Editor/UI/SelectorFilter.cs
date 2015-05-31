using Orbital.Data;

namespace Orbital.UI
{
    public abstract class SelectorFilter
    {
        #region Filter Methods

        public abstract bool Filter(IValueSource source);

        #endregion
    }
}
