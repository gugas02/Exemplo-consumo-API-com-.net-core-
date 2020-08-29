using System;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace API.Consumption.Test.Infra.ExternalServices.Contents
{
    public class XmlContent<T> : StringContent
    {
        public XmlContent(T value)
            : base(Serialize(value), Encoding.UTF8, "text/xml")
        {
        }

        public XmlContent(T value, string mediaType)
            : base(Serialize(value), Encoding.UTF8, mediaType)
        {
        }

        public static string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Deserialize<T>(string xmlText)
        {
            try
            {
                var stringReader = new System.IO.StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch
            {
                throw;
            }
        }
    }
}
