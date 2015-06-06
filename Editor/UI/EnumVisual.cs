using System;

using Orbital.Data;

using UnityEditor;

namespace Orbital.UI
{
    internal sealed class EnumVisual : BaseViual
    {
        #region Draw Methods

        public override void BeginDraw(IValueSource source, UIParameters parameters)
        {
        }

        public override void Draw(IValueSource source, UIParameters parameters)
        {
            Enum value = EditorGUILayout.EnumPopup(source.GetValue<Enum>());
            source.SetValue(value);
        }

        public override void EndDraw(IValueSource source, UIParameters parameters)
        {
        }

        #endregion
    }
}