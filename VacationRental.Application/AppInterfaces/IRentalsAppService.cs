using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface IRentalsAppService
    {
        RentalViewModel GetById(int rentalId);

        ResourceIdViewModel Insert(RentalBindingModel viewModel);
    }
}
