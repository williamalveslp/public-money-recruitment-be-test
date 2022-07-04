using FluentValidation;
using MediatR;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.CommandHandlers;
using VacationRental.Domain.Core.Bus;
using VacationRental.Domain.Core.Notifications;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Infra.CrossCutting.Configs.Extensions;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="IBookingAppService"/>
    public class BookingAppService : CommandHandler, IBookingAppService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IValidator<BookingBindingModel> _userValidator;

        public BookingAppService(
            IBookingRepository bookingRepository,
            IRentalsRepository rentalsRepository,
            IValidator<BookingBindingModel> userValidator,
            IMediatorHandlerNormalize bus,
            INotificationHandler<DomainNotification> notifications) : base(bus, notifications)
        {
            this._bookingRepository = bookingRepository;
            this._rentalsRepository = rentalsRepository;
            this._userValidator = userValidator;
        }

        public BookingViewModel GetById(int bookingId)
        {
            var result = _bookingRepository.GetById(bookingId);

            if (result == null)
            {
                NotifyValidationErrors("Booking not found");
                return default;
            }

            return new BookingViewModel
            {
                Id = result.Id,
                Nights = result.Nights,
                RentalId = result.RentalId,
                Start = result.Start
            };
        }

        public ResourceIdViewModel Insert(BookingBindingModel viewModel)
        {
            var validator = _userValidator.Validate(viewModel);

            if (!validator.IsValid)
            {
                NotifyValidationErrors(validator.GetErrors());
                return default;
            }

            var rentals = _rentalsRepository.GetAll();

            if (!rentals.ContainsKey(viewModel.RentalId))
            {
                NotifyValidationErrors("Rental not found");
                return default;
            }

            var bookings = _bookingRepository.GetAll();

            for (var i = 0; i < viewModel.Nights; i++)
            {
                var count = 0;

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == viewModel.RentalId
                        && (booking.Start <= viewModel.Start.Date && booking.Start.AddDays(booking.Nights) > viewModel.Start.Date)
                        || (booking.Start < viewModel.Start.AddDays(viewModel.Nights) && booking.Start.AddDays(booking.Nights) >= viewModel.Start.AddDays(viewModel.Nights))
                        || (booking.Start > viewModel.Start && booking.Start.AddDays(booking.Nights) < viewModel.Start.AddDays(viewModel.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= rentals[viewModel.RentalId].Units)
                {
                    NotifyValidationErrors("Not available");
                    return default;
                }
            }

            var newBookingId = _bookingRepository.GetNextId();

            _bookingRepository.Insert(newBookingId, viewModel.RentalId, viewModel.Start.Date, viewModel.Nights);

            var key = new ResourceIdViewModel { Id = newBookingId };
            return key;
        }
    }
}
