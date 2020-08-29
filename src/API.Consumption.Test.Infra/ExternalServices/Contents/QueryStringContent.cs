using System;
using System.Net.Http;
using System.Web;

namespace API.Consumption.Test.Infra.ExternalServices.Contents
{
    public class QueryStringContent<T> : StringContent
    {
        public QueryStringContent(string content) : base(content)
        {
        }

        public static T Deserialize<T>(string xmlText) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            var dict = HttpUtility.ParseQueryString(xmlText);
            foreach (var property in properties)
            {
                var valueAsString = dict[property.Name];
                var value = Convert.ChangeType(valueAsString, property.PropertyType);

                if (value == null)
                    continue;

                property.SetValue(obj, value, null);
            }
            return obj;
        }
    }
}
