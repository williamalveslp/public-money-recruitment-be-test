using Microsoft.Extensions.DependencyInjection;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Infra.DataSource.Repositories;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Register the Repositories.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to Repositories.
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRentalsRepository, RentalsRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}
