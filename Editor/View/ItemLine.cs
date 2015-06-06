using System.Collections.Generic;

using Orbital.UI;
using Orbital.Data;
using Orbital.Core;

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

        public BaseViual Visual { get; internal set; }

        #endregion

        #region Constructors

        internal ItemLine(IValueSource source)
        {
            DataSource = source;
            Visual = GetVisual(source);
            Parameters = new UIParameters();
        }

        #endregion

        #region Renderer Methods

        private static BaseViual GetVisual(IValueSource source)
        {
            IVisualRendererManager rendererManager = ServiceProvider.Current.GetService<IVisualRendererManager>();

            return (rendererManager != null) ? rendererManager.GetRenderer(source) : null;
        }

        #endregion

        #region Draw Methods

        internal void Draw()
        {

        }

        #endregion
    }
}