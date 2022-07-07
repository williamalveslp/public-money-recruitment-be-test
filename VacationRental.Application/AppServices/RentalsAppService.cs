using MediatR;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.CommandHandlers;
using VacationRental.Domain.Core.Bus;
using VacationRental.Domain.Core.Notifications;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="IRentalsAppService"/>
    public class RentalsAppService : CommandHandler, IRentalsAppService
    {
        private readonly IRentalsRepository _rentalsRepository;

        public RentalsAppService(
            IMediatorHandlerNormalize bus,
            IRentalsRepository rentalsRepository) : base(bus)
        {
            _rentalsRepository = rentalsRepository;
        }

        public RentalViewModel GetById(int rentalId)
        {
            var rental = _rentalsRepository.GetById(rentalId);

            if (rental == null)
            {
                NotifyValidationErrors("Rental not found");
                return default;
            }

            return new RentalViewModel
            {
                Id = rental.Id,
                Units = rental.Units
            };
        }

        public ResourceIdViewModel Insert(RentalBindingModel viewModel)
        {
            var result = _rentalsRepository.Insert(viewModel.Units);

            return new ResourceIdViewModel
            {
                Id = result.Id
            };
        }
    }
}
