using System;
using System.Linq;
using System.Collections.Generic;

using Orbital.Data;

namespace Orbital.UI
{
    internal sealed class VisualRendererManager : IVisualRendererManager
    {
        #region Fields

        private readonly List<VisualSelector> _selectors = new List<VisualSelector>();
        private readonly Dictionary<Type, IVisual> _renderers = new Dictionary<Type, IVisual>();

        #endregion

        #region Renderers Methods

        public IVisual GetRenderer(IValueSource source)
        {
            Type rendererType = null;
            var validSelector = from c in _selectors
                                let result = new { Selector = c, FilterResult = c.Filter(source) }
                                where result.FilterResult > 0
                                orderby result.FilterResult, result.Selector.IsBuiltIn descending
                                select result;

            if (!validSelector.Any())
                return null;

            if (validSelector.Count() > 1)
            {
                var previousSelector = validSelector.First();
                foreach(var obj in validSelector)
                {
                    if (previousSelector == obj)
                        continue;

                    if (previousSelector.FilterResult != obj.FilterResult)
                        continue;

                    if (!previousSelector.Selector.IsBuiltIn)
                        continue;

                    previousSelector = obj;
                }

                rendererType = previousSelector.Selector.VisualType;
            }
            else
                rendererType = validSelector.First().Selector.VisualType;

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

        public void AddVisualFilter(VisualSelector selector)
        {
            if (_selectors.Contains(selector))
                return;

            _selectors.Add(selector);
        }

        #endregion
    }
}