using Orbital.Data;

namespace Orbital.UI
{
    public interface IVisual
    {
        #region Methods

        void BeginDraw(IValueSource source, UIParameters parameters);

        void Draw(IValueSource source, UIParameters parameters);

        void EndDraw(IValueSource source, UIParameters parameters);

        #endregion
    }
}