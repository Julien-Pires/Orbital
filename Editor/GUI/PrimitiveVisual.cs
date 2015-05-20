using System;
using System.Collections.Generic;

using Orbital.Data;

using UnityEditor;

namespace Orbital.GUI
{
    internal sealed class PrimitiveVisual
    {
        #region Delegates

        private delegate void DrawPrimitive(IValueSource source);

        #endregion

        #region Fields

        private static readonly string[] BoolValues = { "False", "True" };
        private static readonly Dictionary<Type, DrawPrimitive> DrawMaps = new Dictionary<Type, DrawPrimitive>
        {
            { typeof(int), DrawInt },
            { typeof(long), DrawLong },
            { typeof(short), DrawShort },
            { typeof(float), DrawFloat },
            { typeof(double), DrawDouble },
            { typeof(string), DrawString },
            { typeof(byte), DrawByte },
            { typeof(bool), DrawBool }
        };

        #endregion

        #region GUI Methods

        public void OnGUI(IValueSource source)
        {
            DrawPrimitive draw;
            if(!DrawMaps.TryGetValue(source.CLRType, out draw))
                return;

            draw(source);
        }

        private static void DrawInt(IValueSource source)
        {
            int value = EditorGUILayout.IntField(source.Name, source.GetValue<int>());
            source.SetValue(value);
        }

        private static void DrawShort(IValueSource source)
        {
            int value = EditorGUILayout.IntField(source.Name, source.GetValue<short>());
            short shortValue;
            try
            {
                shortValue = Convert.ToInt16(value);
            }
            catch (OverflowException)
            {
                shortValue = 0;
            }

            source.SetValue(shortValue);
        }

        private static void DrawLong(IValueSource source)
        {
            long value = EditorGUILayout.LongField(source.Name, source.GetValue<long>());
            source.SetValue(value);
        }

        private static void DrawFloat(IValueSource source)
        {
            float value = EditorGUILayout.FloatField(source.Name, source.GetValue<float>());
            source.SetValue(value);
        }

        private static void DrawDouble(IValueSource source)
        {
            double value = EditorGUILayout.DoubleField(source.Name, source.GetValue<double>());
            source.SetValue(value);
        }

        private static void DrawString(IValueSource source)
        {
            string value = EditorGUILayout.TextField(source.Name, source.GetValue<string>());
            source.SetValue(value);
        }

        private static void DrawByte(IValueSource source)
        {
            int value = EditorGUILayout.IntField(source.Name, source.GetValue<byte>());
            byte byteValue;
            try
            {
                byteValue = Convert.ToByte(value);
            }
            catch (OverflowException)
            {
                byteValue = 0;
            }

            source.SetValue(byteValue);
        }

        private static void DrawBool(IValueSource source)
        {
            int value = EditorGUILayout.Popup((source.GetValue<bool>() ? 0 : 1), BoolValues);
            source.SetValue(value != 0);
        }

        #endregion
    }
}