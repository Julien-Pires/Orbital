using Orbital.Data;

namespace Orbital.UI
{
    internal interface IVisualRendererManager
    {
        #region Methods

        IVisual GetRenderer(IValueSource source);

        void RegisterSelector(IUISelector selector);

        #endregion
    }
}