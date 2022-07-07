using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using VacationRental.Domain.Entities;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Add the register to Storages.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to Storages.
        /// </summary>
        /// <param name="services"></param>
        public static void AddStorages(this IServiceCollection services)
        {
            services.AddSingleton<IDictionary<int, Rental>>(new Dictionary<int, Rental>());
            services.AddSingleton<IDictionary<int, Bookings>>(new Dictionary<int, Bookings>());
            services.AddSingleton<IDictionary<int, Calendar>>(new Dictionary<int, Calendar>());
        }
    }
}
