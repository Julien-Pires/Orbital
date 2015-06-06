using Orbital.Data;

namespace Orbital.UI
{
    internal interface IVisualRendererManager
    {
        #region Methods

        BaseViual GetRenderer(IValueSource source);

        void AddVisualFilter(VisualSelector selector);

        #endregion
    }
}