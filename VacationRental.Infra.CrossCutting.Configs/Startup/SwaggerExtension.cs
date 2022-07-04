using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VacationRental.Infra.CrossCutting.Configs.Startup
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1",
                        new Info
                        {
                            Title = "Vacation rental information",
                            Version = "v1",
                            Contact = new Contact
                            {
                                Name = "William Goi",
                                Email = "williamalveslp@hotmail.com"
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
    }
}
