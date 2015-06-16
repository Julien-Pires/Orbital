using System;

namespace Orbital.Data
{
    public sealed class DataSourceItemArgs : EventArgs
    {
        #region Fields

        private readonly IValueSource _item;

        #endregion

        #region Properties

        public IValueSource Item
        {
            get { return _item; }
        }

        #endregion

        #region Constructors

        internal DataSourceItemArgs(IValueSource item)
        {
            _item = item;
        }

        #endregion
    }
}