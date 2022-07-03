using System;
using System.Collections.Generic;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.Validations;
using VacationRental.Application.ViewModels;
using VacationRental.Infra.CrossCutting.Configs.Extensions;

namespace VacationRental.Application.AppServices
{
    public class BookingAppService : IBookingAppService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public BookingAppService(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }
        public BookingViewModel GetById(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        public ResourceIdViewModel Insert(BookingBindingModel viewModel)
        {
            // TODO: Needs refactor to validate by Dependecy Injection.
            var validator = new BookingInsertValidator().Validate(viewModel);

            if (!validator.IsValid)
                throw new ApplicationException(validator.GetFirstOrDefaultError());

            if (!_rentals.ContainsKey(viewModel.RentalId))
                throw new ApplicationException("Rental not found");

            for (var i = 0; i < viewModel.Nights; i++)
            {
                var count = 0;
                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == viewModel.RentalId
                        && (booking.Start <= viewModel.Start.Date && booking.Start.AddDays(booking.Nights) > viewModel.Start.Date)
                        || (booking.Start < viewModel.Start.AddDays(viewModel.Nights) && booking.Start.AddDays(booking.Nights) >= viewModel.Start.AddDays(viewModel.Nights))
                        || (booking.Start > viewModel.Start && booking.Start.AddDays(booking.Nights) < viewModel.Start.AddDays(viewModel.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= _rentals[viewModel.RentalId].Units)
                    throw new ApplicationException("Not available");
            }

            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new BookingViewModel
            {
                Id = key.Id,
                Nights = viewModel.Nights,
                RentalId = viewModel.RentalId,
                Start = viewModel.Start.Date
            });

            return key;
        }
    }
}
