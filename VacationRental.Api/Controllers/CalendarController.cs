using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Calendar.
    /// </summary>
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarAppService _calendarApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="calendarApp"></param>
        public CalendarController(
            ICalendarAppService calendarApp)
        {
            this._calendarApp = calendarApp;
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
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status500InternalServerError)]
        public ActionResult<CalendarViewModel> Get(int rentalId, DateTime start, int nights)
        {
            return _calendarApp.GetByFilter(rentalId, start, nights);
        }
    }
}
