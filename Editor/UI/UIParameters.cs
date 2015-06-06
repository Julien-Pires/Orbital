using System.Collections.Generic;

namespace Orbital.UI
{
    public sealed partial class UIParameters
    {
        #region Fields

        private readonly Dictionary<string, object> _internalParameters = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        #endregion

        #region Parameters Methods

        public void SetValue<T>(string key, T value)
        {
            SetValueToMap(key, value, _parameters);
        }

        internal void InternalSetValue<T>(string key, T value)
        {
            SetValueToMap(key, value, _internalParameters);
        }

        private void SetValueToMap<T>(string key, T value, Dictionary<string, object> parameters)
        {
            object parameter;
            if (!parameters.TryGetValue(key, out parameter))
            {
                parameter = new UIParameterValue<T>();
                parameters[key] = parameter;
            }

            ((UIParameterValue<T>)parameter).Value = value;
        }

        public T GetValue<T>(string key)
        {
            return GetValueFromMap<T>(key, _parameters);
        }

        internal T InternalGetValue<T>(string key)
        {
            return GetValueFromMap<T>(key, _internalParameters);
        }

        private T GetValueFromMap<T>(string key, Dictionary<string, object> parameters)
        {
            object parameter;
            if (!parameters.TryGetValue(key, out parameter))
                return default(T);

            return ((UIParameterValue<T>)parameter).Value;
        }

        #endregion
    }
}