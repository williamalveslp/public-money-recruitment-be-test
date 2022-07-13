using System;
using System.Collections.Generic;

namespace VacationRental.Domain.Queries
{
    public class CalendarDateQuery
    {
        public DateTime Date { get; set; }

        public List<CalendarBookingQuery> Bookings { get; set; }
    }
}
