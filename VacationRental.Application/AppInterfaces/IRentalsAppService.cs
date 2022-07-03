using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface IRentalsAppService
    {
        /// <summary>
        /// Get the Rental by Id.
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        RentalViewModel GetById(int rentalId);

        /// <summary>
        /// Insert a new Rental.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        ResourceIdViewModel Insert(RentalBindingModel viewModel);
    }
}
