using API.Consumption.test.Domain.Queries.Output.Api;
using API.Consumption.test.Domain.Repositories;
using API.Consumption.Test.Infra.ExternalServices.Contents.Enum;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Consumption.Test.Infra.ExternalServices.Services.Api
{
    public class ApiService : ServiceFactory, IApiRepository
    {
        private readonly HttpRequestFactory _factory;
        public ApiService(IConfiguration configuration, HttpRequestFactory factory) : base(EService.Api, configuration, factory)
        {
            _factory = factory;
        }
        
        public async Task<List<GetBrandsQueryResult>> GetBrands()
        {
            var requestUri = $"{BaseUri}/Make";

            var response = await _factory.Get(Service, requestUri, Token);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.ContentAsType<List<GetBrandsQueryResult>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GetModelQueryResult>> GetModel(string brandId)
        {
            var requestUri = $"{BaseUri}/Model?MakeID={brandId}";

            var response = await _factory.Get(Service, requestUri, Token);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.ContentAsType<List<GetModelQueryResult>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GetVehiclesQueryResult>> GetVehicles(string modelId, string querystring)
        {
            var requestUri = $"{BaseUri}/Version?ModelID={modelId}";

            if (!string.IsNullOrEmpty(querystring))
            {
                requestUri += querystring;
            }

            var response = await _factory.Get(Service, requestUri, Token);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.ContentAsType<List<GetVehiclesQueryResult>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GetVersionQueryResult>> GetVersion(string page)
        {
            var requestUri = $"{BaseUri}/Vehicles?Page={page}";

            var response = await _factory.Get(Service, requestUri, Token);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.ContentAsType<List<GetVersionQueryResult>>();
            }
            else
            {
                return null;
            }
        }
    }
}
