using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Orbital.Source
{
    internal sealed class DataSourceManager : IDataSourceManager
    {
        #region Fields

        private readonly Dictionary<string, IDataSource> _sources = new Dictionary<string, IDataSource>();

        #endregion

        #region Constructors

        internal DataSourceManager()
        {
            FindDataSource();
        }

        #endregion

        #region Reflection Methods

        private void FindDataSource()
        {
            Assembly asm = typeof (IDataSource).Assembly;
            var types = asm.GetTypes().Where(c => c.GetInterfaces().Any(d => d == typeof (IDataSource)))
                                      .Where(c => !c.IsAbstract && !c.IsInterface);
            foreach (Type type in types)
            {
                IDataSource dataSource = (IDataSource)Activator.CreateInstance(type);
                var attrs = type.GetCustomAttributes(typeof(DataSourceAttribute), true).Cast<DataSourceAttribute>();
                foreach (DataSourceAttribute dataSourceAttribute in attrs)
                {
                    if(dataSourceAttribute == null)
                        continue;

                    for (int i = 0; i < dataSourceAttribute.Extensions.Length; i++)
                        AddDataSource(dataSourceAttribute.Extensions[i], dataSource);
                }
            }
        }

        #endregion

        #region Data Source Management Methods

        public void AddDataSource(string extension, IDataSource source)
        {
            if(string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("extension");

            if(source == null)
                throw new ArgumentNullException("source");

            _sources[extension] = source;
        }

        public IDataSource GetDataSource(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;

            IDataSource dataSource;
            _sources.TryGetValue(extension, out dataSource);

            return dataSource;
        }

        #endregion
    }
}