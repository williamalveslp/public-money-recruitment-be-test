using System;
using System.Collections.Generic;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.Validations;
using VacationRental.Application.ViewModels;
using VacationRental.Application.ViewModels.Calendars;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Infra.CrossCutting.Configs.Extensions;

namespace VacationRental.Application.AppServices
{
    public class CalendarAppService : ICalendarAppService
    {
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IBookingRepository _bookingRepository;

        public CalendarAppService(
            IRentalsRepository rentalsRepository,
            IBookingRepository bookingRepository)
        {
            this._rentalsRepository = rentalsRepository;
            this._bookingRepository = bookingRepository;
        }

        public CalendarViewModel GetByFilter(int rentalId, DateTime start, int nights)
        {
            var viewModel = new CalendarGetByFilterViewModel
            {
                RentalId = rentalId,
                Start = start,
                Nights = nights
            };

            // TODO: Needs refactor to validate by Dependecy Injection.
            var validator = new CalendarGetByFilterValidator().Validate(viewModel);

            if (!validator.IsValid)
                throw new ApplicationException(validator.GetFirstOrDefaultError());

            var rentals = _rentalsRepository.GetAll();

            if (!rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var bookings = _bookingRepository.GetAll();

            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>()
                };

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
