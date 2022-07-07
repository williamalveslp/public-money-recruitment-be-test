using Microsoft.Extensions.DependencyInjection;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.AppServices;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Register the AppServices.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to AppServices.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IRentalsAppService, RentalsAppService>();
            services.AddScoped<ICalendarAppService, CalendarAppService>();
            services.AddScoped<IBookingAppService, BookingAppService>();
        }
    }
}
