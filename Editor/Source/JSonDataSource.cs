using System;

using Newtonsoft.Json;

namespace Orbital.Source
{
    [DataSource(Extensions =  new []{ ".json" })]
    internal sealed class JSonDataSource : BaseDataSource
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