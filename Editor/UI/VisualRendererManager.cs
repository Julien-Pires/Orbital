using System;
using System.Collections.Generic;

using Orbital.Data;

namespace Orbital.UI
{
    internal sealed class VisualRendererManager
    {
        #region Fields

        private readonly List<IUISelector> _selectors = new List<IUISelector>();
        private readonly Dictionary<Type, IVisual> _renderers = new Dictionary<Type, IVisual>();

        #endregion

        #region Renderers Methods

        public IVisual GetRenderer(IValueSource source)
        {
            Type rendererType = null;
            for (int i = 0; i < _selectors.Count; i++)
            {
                rendererType = _selectors[i].GetRenderer(source);
                if(rendererType != null)
                    break;
            }

            return (rendererType == null) ? null : EnsureRenderer(rendererType);
        }

        private IVisual EnsureRenderer(Type type)
        {
            IVisual renderer;
            if (_renderers.TryGetValue(type, out renderer))
                return renderer;

            renderer = (IVisual)Activator.CreateInstance(type);
            _renderers[type] = renderer;

            return renderer;
        }

        #endregion

        #region Selectors Methods

        public void RegisterSelector(IUISelector selector)
        {
            if (_selectors.Contains(selector))
                return;

            _selectors.Add(selector);
        }

        #endregion
    }
}
