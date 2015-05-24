using System.Collections.Generic;

using Orbital.Data;

namespace Orbital.GUI
{
    internal sealed class ItemLine
    {
        #region Fields

        private List<ItemLine> _children;

        #endregion

        #region Properties

        public IValueSource DataSource { get; internal set; }

        public IVisual Visual { get; internal set; }

        #endregion
    }
}