using System;
using Microsoft.Extensions.Configuration;
using API.Consumption.Test.Infra.ExternalServices.Contents.Enum;

namespace API.Consumption.Test.Infra.ExternalServices
{
    public class ServiceFactory
    {
        public EService Service { get; private set; }
        public string BaseUri { get; set; }
        public EAuthType AuthType { get; set; }
        private bool NeedAuthentication { get; set; }
        public string Token { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private IConfiguration _configuration;
        private readonly HttpRequestFactory _factory;

        public ServiceFactory(EService service, IConfiguration configuration, HttpRequestFactory factory)
        {
            _factory = factory;
            _configuration = configuration;
            Service = service;
            switch (Service)
            {
                case EService.Api:
                    BaseUri = configuration.GetSection("ApiEndPoind").Value;
                    NeedAuthentication = false;
                    AuthType = EAuthType.Basic;
                    break;
                default:
                    throw new Exception("choose real api");
            }
        }
    }
}
