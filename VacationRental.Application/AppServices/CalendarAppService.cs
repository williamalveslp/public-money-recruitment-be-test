using FluentValidation;
using System;
using System.Collections.Generic;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Application.ViewModels.Calendars;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Infra.CrossCutting.Configs.Extensions;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="ICalendarAppService"/>
    public class CalendarAppService : ICalendarAppService
    {
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<CalendarGetByFilterViewModel> _calendarValidator;

        public CalendarAppService(
            IRentalsRepository rentalsRepository,
            IBookingRepository bookingRepository,
            IValidator<CalendarGetByFilterViewModel> calendarValidator)
        {
            this._rentalsRepository = rentalsRepository;
            this._bookingRepository = bookingRepository;
            this._calendarValidator = calendarValidator;
        }

        public CalendarViewModel GetByFilter(int rentalId, DateTime start, int nights)
        {
            var viewModel = new CalendarGetByFilterViewModel
            {
                RentalId = rentalId,
                Start = start,
                Nights = nights
            };

            var validator = _calendarValidator.Validate(viewModel);

            if (!validator.IsValid)
                throw new ApplicationException(validator.GetErrors());

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
