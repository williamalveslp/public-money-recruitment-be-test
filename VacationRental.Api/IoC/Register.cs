using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.Infra.CrossCutting.Configs.Startup;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Register general layers.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Register general layers.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterGeneralLayers(this IServiceCollection services, IConfiguration configuration)
        {
            // Swagger.
            services.AddSwaggerExtension(configuration);

            // Storages.
            services.AddStorages();

            // Validators.
            services.AddValidators();

            // Handlers (MediarR, Notifications, etc).
            services.AddHandlers();

            // AppServices.
            services.AddAppServices();

            // Repositories.
            services.AddRepositories();
        }
    }
}
