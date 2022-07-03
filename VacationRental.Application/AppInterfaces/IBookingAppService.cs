using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface IBookingAppService
    {
        /// <summary>
        /// Get the Booking by Id.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        BookingViewModel GetById(int bookingId);

        /// <summary>
        /// Insert a new booking.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        ResourceIdViewModel Insert(BookingBindingModel viewModel);
    }
}
