using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Consumption.test.Infra.Repositories;
using API.Consumption.test.Domain.Repositories;
using API.Consumption.Test.Infra.ExternalServices.Services.Api;
using API.Consumption.test.Domain.Command.Advertisement;
using Teste.Domain.Handlers;
using API.Consumption.test.Domain.Handlers;
using API.Consumption.Test.Infra.ExternalServices;

namespace API.Consumption.Test.Shared.Config
{
    public static class DependencyRegister
    {
        public static void AddScoped(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configurations 
            
            #endregion

            #region Command Handlers
            services.AddScoped<IHandler<EditAdvertisementCommand>, AdvertisementHandler>();
            services.AddScoped<IHandler<CreateAdvertisementCommand>, AdvertisementHandler>();
            services.AddScoped<IHandler<DeleteAdvertisementCommand>, AdvertisementHandler>();
            #endregion

            #region Query Handlers

            #endregion

            #region Repositories
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            #endregion

            #region Services
            services.AddScoped<HttpRequestFactory>();
            services.AddScoped<IApiRepository, ApiService>();
            #endregion
        }

        public static void AddSingleton(this IServiceCollection services, string Env, IConfiguration Configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
