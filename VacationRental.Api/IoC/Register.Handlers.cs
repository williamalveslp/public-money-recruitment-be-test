using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.Domain.Core.Bus;
using VacationRental.Domain.Core.Notifications;
using VacationRental.Infra.CrossCutting.Bus;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Register the Handlers.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to Handlers.
        /// </summary>
        /// <param name="services"></param>
        public static void AddHandlers(this IServiceCollection services)
        {
            // Mediator - MediatR.Extensions.Microsoft.DependencyInjection Version=9.0.0.0.
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Infra - Bus
            services.AddScoped<IMediatorHandlerNormalize, InMemoryBusNormalize>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
