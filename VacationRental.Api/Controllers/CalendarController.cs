using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Calendar.
    /// </summary>
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rentals"></param>
        /// <param name="bookings"></param>
        public CalendarController(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        /// <summary>
        /// Get calendar by filter.
        /// </summary>
        /// <param name="rentalId"></param>
        /// <param name="start"></param>
        /// <param name="nights"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CalendarViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status400BadRequest)]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var result = new CalendarViewModel 
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>() 
            };
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>()
                };

                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
