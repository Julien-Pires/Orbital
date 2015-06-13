using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Orbital.Serializer
{
    internal sealed class DataSerializerManager : IDataSerializerManager
    {
        #region Fields

        private readonly Dictionary<string, ISerializer> _serializers = new Dictionary<string, ISerializer>();

        #endregion

        #region Constructors

        internal DataSerializerManager()
        {
            FindSerializer();
        }

        #endregion

        #region Reflection Methods

        private void FindSerializer()
        {
            Assembly asm = typeof (ISerializer).Assembly;
            var types = asm.GetTypes().Where(c => c.GetInterfaces().Any(d => d == typeof (ISerializer)))
                                      .Where(c => !c.IsAbstract && !c.IsInterface);
            foreach (Type type in types)
            {
                ISerializer serializer = (ISerializer)Activator.CreateInstance(type);
                var attrs = type.GetCustomAttributes(typeof(SerializerAttribute), true).Cast<SerializerAttribute>();
                foreach (SerializerAttribute dataSourceAttribute in attrs)
                {
                    if(dataSourceAttribute == null)
                        continue;

                    for (int i = 0; i < dataSourceAttribute.Extensions.Length; i++)
                        AddSerializer(dataSourceAttribute.Extensions[i], serializer);
                }
            }
        }

        #endregion

        #region Data Source Management Methods

        public void AddSerializer(string extension, ISerializer source)
        {
            if(string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("extension");

            if(source == null)
                throw new ArgumentNullException("source");

            _serializers[extension] = source;
        }

        public ISerializer GetSerializer(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;

            ISerializer serializer;
            _serializers.TryGetValue(extension, out serializer);

            return serializer;
        }

        #endregion
    }
}