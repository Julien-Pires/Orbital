using System.Collections.Generic;

namespace Orbital.GUI
{
    internal sealed class VisualObject : IVisual
    {
        #region Fields

        private readonly List<IVisual> _children = new List<IVisual>();

        #endregion

        #region IVisual Implementation

        public void OnGUI()
        {
            
        }

        #endregion
    }
}