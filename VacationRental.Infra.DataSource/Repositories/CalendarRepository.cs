using System;
using System.Collections.Generic;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Infra.DataSource.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly IDictionary<int, Calendar> _calendar;

        public CalendarRepository(IDictionary<int, Calendar> calendar)
        {
            this._calendar = calendar;
        }

        public IDictionary<int, Calendar> GetAll()
        {
            return _calendar;
        }
    }
}
