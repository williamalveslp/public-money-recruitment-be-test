using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VacationRental.Infra.CrossCutting.Configs.Startup
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerData = ConfigurationTransfer.GetObject<SwaggerSettings>(configuration);
            var serviceCollectionData = swaggerData?.ServiceCollection;

            _ = services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc(swaggerData.ServiceCollection.Version,
                    new OpenApiInfo
                    {
                        Title = serviceCollectionData?.Title,
                        Version = serviceCollectionData?.Version,
                        Description = serviceCollectionData?.Description,
                        Contact = new OpenApiContact
                        {
                            Name = serviceCollectionData?.Contact?.Name,
                            Email = serviceCollectionData?.Contact?.Email,
                            Url = new Uri(serviceCollectionData?.Contact?.Url)
                        }
                    });

                var rootFullDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var rootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(rootFullDirectory).FullName).FullName).FullName;

                var projectStartupName = $"{rootDirectory.Split(Path.DirectorySeparatorChar).Last()}.xml";

                var xmlPath = Path.Combine(rootFullDirectory, projectStartupName);

                if (!File.Exists(xmlPath))
                    return;

                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerData = ConfigurationTransfer.GetObject<SwaggerSettings>(configuration);
            var serviceCollectionData = swaggerData?.ServiceCollection;
            var appBuilderData = swaggerData?.AppBuilder;

            string title = serviceCollectionData?.Title;
            string version = serviceCollectionData?.Version;

            string urlRouteSwagger = appBuilderData?.UrlSwagger;
            urlRouteSwagger = string.Format(urlRouteSwagger, version);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // Example: "/swagger/{0}/swagger.json".
                c.SwaggerEndpoint(urlRouteSwagger, title);
            });
        }
    }

    internal class SwaggerSettings
    {
        public AppBuilderDto AppBuilder { get; set; }
        public ServiceCollectionDto ServiceCollection { get; set; }

        public class ServiceCollectionDto
        {
            public string Title { get; set; }

            public string Version { get; set; }

            public string Description { get; set; }

            public ContactDto Contact { get; set; }

            public class ContactDto
            {
                public string Name { get; set; }
                public string Email { get; set; }
                public string Url { get; set; }
            }
        }

        public class AppBuilderDto
        {
            public string UrlSwagger { get; set; }
        }
    }
}
