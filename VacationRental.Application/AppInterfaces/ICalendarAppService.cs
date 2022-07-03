using System;
using VacationRental.Application.ViewModels;

namespace VacationRental.Application.AppInterfaces
{
    public interface ICalendarAppService
    {
        CalendarViewModel GetByFilter(int rentalId, DateTime start, int nights);
    }
}
