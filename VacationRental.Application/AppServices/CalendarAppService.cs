using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Application.ViewModels.Calendars;
using VacationRental.Domain.CommandHandlers;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Domain.Queries;
using VacationRental.Infra.CrossCutting.Configs.Extensions;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="ICalendarAppService"/>
    public class CalendarAppService : CommandHandler, ICalendarAppService
    {
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<CalendarGetByFilterViewModel> _calendarValidator;

        public CalendarAppService(
            IRentalsRepository rentalsRepository,
            IBookingRepository bookingRepository,
            IValidator<CalendarGetByFilterViewModel> calendarValidator,
            IMediator mediator) : base(mediator)
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
            {
                NotifyValidationErrors(validator.GetErrors());
                return default;
            }

            var rentals = _rentalsRepository.GetAll();

            if (!rentals.ContainsKey(rentalId))
            {
                NotifyValidationErrors("Rental not found");
                return default;
            }

            var calendarNormalizedQuery = _bookingRepository.CalendarDatesNormalized(rentalId, nights, start);

            return MappingCalendarQueryToViewModel(calendarNormalizedQuery);
        }

        private CalendarViewModel MappingCalendarQueryToViewModel(CalendarQuery calendarNormalizedQuery)
        {
            if (calendarNormalizedQuery == null)
                return default;

            var resultViewModel = new CalendarViewModel
            {
                RentalId = calendarNormalizedQuery.RentalId,
                Dates = new List<CalendarDateViewModel>()
            };

            foreach (var item in calendarNormalizedQuery?.Dates)
            {
                var bookigItem = item?.Bookings?.Select(f => new CalendarBookingViewModel
                {
                    Id = f.Id
                });

                if (bookigItem == null)
                    continue;

                resultViewModel.Dates.Add(new CalendarDateViewModel
                {
                    Bookings = bookigItem.ToList(),
                    Date = item.Date
                });
            }

            return resultViewModel;
        }
    }
}
