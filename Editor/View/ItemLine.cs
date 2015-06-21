using System.Collections.Generic;

using Orbital.UI;
using Orbital.Data;
using Orbital.Core;

using UnityEditor;

namespace Orbital.View
{
    internal sealed class ItemLine
    {
        #region Fields

        private const string ChildrenFoldParameter = "ChildFold";

        private List<ItemLine> _children;
        private bool _hasChildren;

        #endregion

        #region Properties

        public IValueSource DataSource { get; internal set; }

        public UIParameters Parameters { get; internal set; }

        public BaseViual Visual { get; internal set; }

        #endregion

        #region Constructors

        internal ItemLine(IValueSource source)
        {
            _children = new List<ItemLine>();

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
            if (Visual == null)
                return;

            IParentSource parent = DataSource as IParentSource;
            bool childVisible = Parameters.InternalGetValue<bool>(ChildrenFoldParameter);
            if (parent != null)
                childVisible = EditorGUILayout.Foldout(childVisible, Visual.Title);

            Visual.Draw(DataSource, Parameters);
            if (childVisible)
            {
                for (int i = 0; i < _children.Count; i++)
                    _children[i].Draw();
            }

            Parameters.InternalSetValue(ChildrenFoldParameter, childVisible);
        }

        #endregion
    }
}