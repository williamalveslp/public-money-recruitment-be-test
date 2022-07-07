using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.Application.Validations;

namespace VacationRental.Api.IoC
{
    /// <summary>
    /// Register the Validators.
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// Add the register to Validators.
        /// </summary>
        /// <param name="services"></param>
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BookingInsertValidator>();
            services.AddValidatorsFromAssemblyContaining<CalendarGetByFilterValidator>();
        }
    }
}
