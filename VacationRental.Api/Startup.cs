using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.AppServices;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Infra.CrossCutting.Configs.Startup;
using VacationRental.Infra.DataSource.Repositories;

namespace VacationRental.Api
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger.
            services.AddSwaggerExtension(Configuration);
           
            services.AddSingleton<IDictionary<int, Rental>>(new Dictionary<int, Rental>());
            services.AddSingleton<IDictionary<int, Bookings>>(new Dictionary<int, Bookings>());
            services.AddSingleton<IDictionary<int, Calendar>>(new Dictionary<int, Calendar>());

            DependencyInjections(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));
        }

        private void DependencyInjections(IServiceCollection services)
        {
            services.AddScoped<IRentalsAppService, RentalsAppService>();
            services.AddScoped<ICalendarAppService, CalendarAppService>();
            services.AddScoped<IBookingAppService, BookingAppService>();

            services.AddScoped<IRentalsRepository, RentalsRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}
