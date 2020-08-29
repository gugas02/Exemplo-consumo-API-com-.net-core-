﻿using API.Consumption.test.Infra.DataContexts;
using API.Consumption.Test.Shared.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace API.Consumption.Test.Api.Config
{
    public static class ConfigureServices
    {
        public static void ConfigureErrorHandling(this IServiceCollection services)
        {

        }

        public static void ConfigureDi(this IServiceCollection services, IWebHostEnvironment Env, IConfiguration Configuration)
        {
            DependencyRegister.AddScoped(services, Configuration);

            //build service provider for resolving dependencies from di container
            var serviceProvider = services.BuildServiceProvider();
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<TesteDataContext, TesteDataContext>();
        }

        public static void ConfigureAuth(this IServiceCollection services, IConfiguration Configuration)
        {

        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Net Motors API",
                    Description = "Net Motors",
                    Contact = new OpenApiContact()
                    {
                        Name = "Net Motors",
                        Url = new Uri("http://netmotors.com.br/")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddCors();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
    }
}
