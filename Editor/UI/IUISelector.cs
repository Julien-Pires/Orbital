using System;

using Orbital.Data;

namespace Orbital.UI
{
    public interface IUISelector
    {
        #region Methods

        Type GetRenderer(IValueSource source);

        #endregion
    }
}