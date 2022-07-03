using System;
using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface ICalendarAppService
    {
        /// <summary>
        /// Get a calendar by filter.
        /// </summary>
        /// <param name="rentalId"></param>
        /// <param name="start"></param>
        /// <param name="nights"></param>
        /// <returns></returns>
        CalendarViewModel GetByFilter(int rentalId, DateTime start, int nights);
    }
}
