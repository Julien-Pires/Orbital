using System;

using Orbital.Data;

using UnityEditor;

namespace Orbital.UI
{
    internal sealed class EnumVisual : IVisual
    {
        #region Draw Methods

        public void BeginDraw(IValueSource source, UIParameters parameters)
        {
        }

        public void Draw(IValueSource source, UIParameters parameters)
        {
            Enum value = EditorGUILayout.EnumPopup(source.GetValue<Enum>());
            source.SetValue(value);
        }

        public void EndDraw(IValueSource source, UIParameters parameters)
        {
        }

        #endregion
    }
}