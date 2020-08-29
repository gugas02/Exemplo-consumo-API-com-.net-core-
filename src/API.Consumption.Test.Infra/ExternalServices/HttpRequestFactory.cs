using API.Consumption.Test.Domain.Shared;
using API.Consumption.Test.Infra.ExternalServices.Contents;
using API.Consumption.Test.Infra.ExternalServices.Contents.Enum;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Consumption.Test.Infra.ExternalServices
{
    public class HttpRequestFactory
    {
        public HttpRequestFactory()
        {
        }
        public async Task<HttpResponseMessage> Get(EService service, string requestUri, string token = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(token)
                                .AddServiceDescription(service.GetDescription());

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Get(EService service, string requestUri, string user, string password)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBasicAuth(user, password)
                                .AddServiceDescription(service.GetDescription());

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(
           EService service, string requestUri, object value, string token = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(token)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> PostOauth(
           EService service, string requestUri, object value, string consumerKey, string consumerSecret, string accessToken, string tokenSecret)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddOauthAuth("POST", requestUri, consumerKey, consumerSecret, accessToken, tokenSecret)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> GetOauth(EService service, string requestUri, string consumerKey, string consumerSecret, string accessToken, string tokenSecret)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddOauthAuth("GET", requestUri, consumerKey, consumerSecret, accessToken, tokenSecret)
                                .AddServiceDescription(service.GetDescription());

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> PostOauthFormUrlEncoded(
           EService service, string requestUri, Dictionary<string, string> value, string consumerKey, string consumerSecret, string accessToken, string tokenSecret)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContentFormUrlEncoded(value)
                                .AddOauthAuth("POST", requestUri, consumerKey, consumerSecret, accessToken, tokenSecret, value)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> Post(
           EService service, string requestUri, object value, string user, string password)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBasicAuth(user, password)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> PostFormUrlEncoded(
           EService service, string requestUri, Dictionary<string, string> value, string user, string password)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContentFormUrlEncoded(value)
                                .AddBasicAuth(user, password)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> Put(
           EService service, string requestUri, object value, string token = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(token)
                                .AddServiceDescription(service.GetDescription());


            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> Put(
           EService service, string requestUri, object value, string user, string password)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBasicAuth(user, password)
                                .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> Patch(
           EService service, string requestUri, object value, string token = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value))
                                .AddBearerToken(token)
                                .AddServiceDescription(service.GetDescription());

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Delete(EService service, string requestUri, string token = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(token)
                                .AddServiceDescription(service.GetDescription());


            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> Delete(
               EService service, string requestUri, object value, string user, string password)
        {
            var builder = new HttpRequestBuilder()
                           .AddMethod(HttpMethod.Delete)
                           .AddRequestUri(requestUri)
                           .AddContent(new JsonContent(value))
                           .AddBasicAuth(user, password)
                           .AddServiceDescription(service.GetDescription());

            var response = await builder.SendAsync();

            return response;
        }

        public async Task<HttpResponseMessage> PostXml<T>(
           EService service, string requestUri, T value, string userName, string password)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddAcceptHeader("text/xml")
                                .AddContent(new XmlContent<T>(value))
                                .AddBasicAuth(userName, password)
                                .AddServiceDescription(service.GetDescription());


            var response = await builder.SendAsync();

            return response;
        }


    }
}
