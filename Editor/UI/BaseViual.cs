using Orbital.Data;

namespace Orbital.UI
{
    public abstract class BaseViual
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Methods

        public abstract void BeginDraw(IValueSource source, UIParameters parameters);

        public abstract void Draw(IValueSource source, UIParameters parameters);

        public abstract void EndDraw(IValueSource source, UIParameters parameters);

        #endregion
    }
}