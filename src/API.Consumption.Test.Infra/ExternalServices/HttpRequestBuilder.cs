using API.Consumption.Test.Domain.Shared;
using API.Consumption.Test.Infra.ExternalServices.Contents.Enum;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Consumption.Test.Infra.ExternalServices
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string token = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 0, 30);
        private EAuthType authType = EAuthType.Token;
        private string serviceDercription = "";

        public HttpRequestBuilder()
        {
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            var a = content.ReadAsStringAsync().Result;
            return this;
        }

        public HttpRequestBuilder AddServiceDescription(string serviceDercription)
        {
            this.serviceDercription = serviceDercription;
            return this;
        }

        public HttpRequestBuilder AddContentFormUrlEncoded(Dictionary<string, string> content)
        {
            this.content = new FormUrlEncodedContent(content);
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            token = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddBasicAuth(string user, string password)
        {
            token = $"{user}:{password}";
            authType = EAuthType.Basic;
            return this;
        }

        protected string NormalizeParameters(SortedDictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var parameter in parameters)
            {
                if (parameter.Value != "")
                {
                    if (i > 0)
                        sb.Append("&");

                    sb.AppendFormat("{0}={1}", parameter.Key, parameter.Value);
                    i++;
                }
            }

            return sb.ToString();
        }

        public HttpRequestBuilder AddOauthAuth(string method, string url, string consumerKey, string consumerSecret, string accessToken, string tokenSecret, Dictionary<string, string> additionalParams = null)
        {
            var timeStamp = DateUtils.GetUnixTimestampString();
            var nounce = StringUtils.RandomString(11);

            var parameters = new SortedDictionary<string, string>
            {
                {"oauth_consumer_key", consumerKey},
                {"oauth_nonce", nounce },
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", timeStamp},
                {"oauth_token", accessToken },
                {"oauth_version", "1.0"}
            };

            if (url.IndexOf('?') != -1)
            {
                var urlParams = url.Substring(url.IndexOf('?')).Replace("?", "");
                var queryParams = urlParams.Split("&");
                if (queryParams != null)
                {
                    foreach (var parameter in queryParams)
                    {
                        var aux = parameter.Split('=');
                        parameters.Add(aux[0], aux[1]);
                    }
                }
                url = url.Substring(0, url.IndexOf('?'));
            }


            if (additionalParams != null)
            {
                foreach (var parameter in additionalParams)
                {
                    parameters.Add(parameter.Key, parameter.Value);
                }
            }

            var sb = new StringBuilder();
            sb.Append(method).Append("&" + Uri.EscapeDataString(url)).Append("&" + Uri.EscapeDataString(NormalizeParameters(parameters)));
            var signatureKey = string.Format("{0}&{1}", consumerSecret, tokenSecret);
            string oauthSignature = StringUtils.EncryptSha1(sb.ToString(), signatureKey);

            var accessTokenHelper = accessToken != "" ? $"oauth_token=\"{Uri.EscapeDataString(accessToken)}\"," : "";

            token = $"oauth_consumer_key=\"{consumerKey}\"," +
                         $"oauth_signature_method=\"HMAC-SHA1\"," +
                         $"oauth_timestamp=\"{timeStamp}\"," +
                         $"oauth_nonce=\"{nounce}\"," +
                         $"{accessTokenHelper}" +
                         $"oauth_version=\"1.0\"," +
                         $"oauth_signature=\"{Uri.EscapeDataString(oauthSignature)}\"";

            authType = EAuthType.Oauth;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            //EnsureArguments();

            // Setup request
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri),
            };
            request.SetTimeout(timeout);

            if (content != null)
                request.Content = content;

            if (!string.IsNullOrEmpty(token))
            {
                if (authType == EAuthType.Basic)
                {
                    var byteArray = Encoding.ASCII.GetBytes(token);
                    request.Headers.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (authType == EAuthType.Token)
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    request.Headers.Authorization =
                          new AuthenticationHeaderValue("OAuth", token);
                }
            }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
                request.Headers.Accept.Add(
                   new MediaTypeWithQualityHeaderValue(acceptHeader));

            // Setup client

            var handler = new TimeoutHandler
            {
                InnerHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                }
            };
            var client = new HttpClient(handler);
            client.Timeout = Timeout.InfiniteTimeSpan;

            var stringContent = "";

            if (content != null)
            {
                stringContent = await content.ReadAsStringAsync();
            }


            try
            {
                var response = await client.SendAsync(request);
                return response;
            }
            catch (TimeoutException ex)
            {
                throw new Exception("A requisição deu timeout");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
