using Orbital.Data;

namespace Orbital.GUI
{
    public interface IVisual
    {
        #region Methods

        void OnGUI(IValueSource source);

        #endregion
    }
}