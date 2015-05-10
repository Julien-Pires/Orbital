using System;
using System.IO;
using System.Xml.Serialization;

namespace Orbital.Source
{
    [DataSource(Extensions = new[] { ".xml" })]
    internal sealed class XmlDataSource : BaseDataSource
    {
        #region IDataSource implementation

        public override object GetData(string filename, Type type)
        {
            string text = GetText(filename);
            if (text == null)
                return null;

            using (TextReader reader = new StringReader(text))
            {
                XmlSerializer serializer = new XmlSerializer(type);

                return serializer.Deserialize(reader);
            }
        }

        #endregion
    }
}