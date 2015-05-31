using System.Collections.Generic;

using Orbital.UI;
using Orbital.Data;

namespace Orbital.View
{
    internal sealed class ItemLine
    {
        #region Fields

        private List<ItemLine> _children;

        #endregion

        #region Properties

        public IValueSource DataSource { get; internal set; }

        public UIParameters Parameters { get; internal set; }

        public IVisual Visual { get; internal set; }

        #endregion
    }
}