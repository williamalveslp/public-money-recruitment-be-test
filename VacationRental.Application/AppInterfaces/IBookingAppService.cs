using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface IBookingAppService
    {
        BookingViewModel GetById(int bookingId);

        ResourceIdViewModel Insert(BookingBindingModel viewModel);
    }
}
