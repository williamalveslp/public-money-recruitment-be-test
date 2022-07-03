using System;
using System.Collections.Generic;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppServices
{
    public class RentalsAppService : IRentalsAppService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;

        public RentalsAppService(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
        }

        public RentalViewModel GetById(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        public ResourceIdViewModel Insert(RentalBindingModel viewModel)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = viewModel.Units
            });

            return key;
        }
    }
}
