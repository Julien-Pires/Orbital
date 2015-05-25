using System.Collections.Generic;

namespace Orbital.UI
{
    public sealed partial class UIParameters
    {
        #region Fields

        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        #endregion

        #region Parameters Methods

        public void SetValue<T>(string key, T value)
        {
            object parameter;
            if (!_parameters.TryGetValue(key, out parameter))
            {
                parameter = new UIParameterValue<T>();
                _parameters[key] = parameter;
            }

            ((UIParameterValue<T>) parameter).Value = value;
        }

        public T GetValue<T>(string key)
        {
            object parameter;
            if (!_parameters.TryGetValue(key, out parameter))
                return default(T);

            return ((UIParameterValue<T>) parameter).Value;
        }

        #endregion
    }
}