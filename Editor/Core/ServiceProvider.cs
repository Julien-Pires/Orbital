using System;
using System.Collections.Generic;

namespace Orbital.Core
{
    internal sealed class ServiceProvider : IDisposable
    {
        #region Fields

        private static ServiceProvider _currentProvider;

        private bool _isDisposed;
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        #endregion

        #region Properties

        public static ServiceProvider Current
        {
            get { return _currentProvider; }
            set
            {
                if(_currentProvider == value)
                    return;

                if(_currentProvider != null)
                    _currentProvider.Dispose();

                _currentProvider = value;
            }
        }

        #endregion

        #region Dispose Methods

        public void Dispose()
        {
            if (_isDisposed)
                return;

            foreach (object service in _services.Values)
            {
                IDisposable disposable = service as IDisposable;
                if(disposable == null)
                    continue;

                disposable.Dispose();
            }

            _services.Clear();
            _isDisposed = true;
        }

        #endregion

        #region Provider Methods

        public void RegisterService<T>(T service) where T : class 
        {
            if(service == null)
                throw new ArgumentNullException("service");

            _services[typeof (T)] = service;
        }

        public T GetService<T>() where T : class
        {
            object service;
            if (!_services.TryGetValue(typeof (T), out service))
                return default(T);

            return (T) service;
        }

        #endregion
    }
}