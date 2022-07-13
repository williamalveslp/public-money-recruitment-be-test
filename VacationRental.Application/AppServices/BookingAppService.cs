using MediatR;
using System.Net;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.CommandHandlers;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="IBookingAppService"/>
    public class BookingAppService : CommandHandler, IBookingAppService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalsRepository _rentalsRepository;

        public BookingAppService(
            IBookingRepository bookingRepository,
            IRentalsRepository rentalsRepository,
            IMediator mediator) : base(mediator)
        {
            this._bookingRepository = bookingRepository;
            this._rentalsRepository = rentalsRepository;
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
            if (!viewModel.Validator().IsValid)
            {
                NotifyValidationErrors(viewModel.ErrorsMessage, HttpStatusCode.BadRequest);
                return default;
            }

            var rentals = _rentalsRepository.GetAll();

            if (!rentals.ContainsKey(viewModel.RentalId))
            {
                NotifyValidationErrors("Rental not found");
                return default;
            }

            var isAvailable = _bookingRepository.IsAvailable(viewModel.RentalId, viewModel.Start, viewModel.Nights, rentals);

            if (!isAvailable)
            {
                NotifyValidationErrors("Not available");
                return default;
            }

            var newBookingId = _bookingRepository.GetNextId();

            var result = _bookingRepository.Insert(newBookingId, viewModel.RentalId, viewModel.Start.Date, viewModel.Nights);

            return new ResourceIdViewModel { Id = result.Id };
        }
    }
}
