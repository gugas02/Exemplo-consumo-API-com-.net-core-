using API.Consumption.Test.Infra.ExternalServices.Contents;
using Newtonsoft.Json;
using System.Net.Http;

namespace API.Consumption.Test.Infra.ExternalServices
{
    public static class HttpResponseExtensions
    {
        public static T ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ?
                    default :
                    JsonConvert.DeserializeObject<T>(data);
        }

        public static string ContentAsJson(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.SerializeObject(data);
        }

        public static string ContentAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        public static T XmlAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ?
                            default :
                            XmlContent<T>.Deserialize<T>(data);
        }

        public static T ContentQueryStringAsType<T>(this HttpResponseMessage response) where T : new()
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ?
                            default :
                            QueryStringContent<T>.Deserialize<T>(data);
        }
    }
}
