using System;

using Newtonsoft.Json;

namespace Orbital.Serializer
{
    [Serializer(Extensions =  new []{ ".json" })]
    internal sealed class JsonDataSerializer : BaseSerializer
    {
        #region IDataSource implementation

        public override object GetData(string filename, Type type)
        {
            string text = GetText(filename);
            if (text == null)
                return null;

            return JsonConvert.DeserializeObject(text, type);
        }

        #endregion
    }
}