using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Bookings.
    /// </summary>
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingAppService _bookingApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bookingApp"></param>
        public BookingsController(
            IBookingAppService bookingApp)
        {
            _bookingApp = bookingApp;
        }

        /// <summary>
        /// Get the Booking by Id.
        /// </summary>
        /// <param name="bookingId">Booking Id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{bookingId:int}")]
        [ProducesResponseType(typeof(BookingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status400BadRequest)]
        public BookingViewModel Get(int bookingId)
        {
            return _bookingApp.GetById(bookingId);
        }

        /// <summary>
        /// Insert the Booking.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResourceIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status400BadRequest)]
        public ResourceIdViewModel Post(BookingBindingModel viewModel)
        {
            return _bookingApp.Insert(viewModel);
        }
    }
}
