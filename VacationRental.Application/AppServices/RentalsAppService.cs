using System;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Application.AppServices
{
    ///<inheritdoc cref="IRentalsAppService"/>
    public class RentalsAppService : IRentalsAppService
    {
        private readonly IRentalsRepository _rentalsRepository;

        public RentalsAppService(IRentalsRepository rentalsRepository)
        {
            _rentalsRepository = rentalsRepository;
        }

        public RentalViewModel GetById(int rentalId)
        {
            var rental = _rentalsRepository.GetById(rentalId);

            if (rental == null)
                throw new ApplicationException("Rental not found");

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
