using System.Collections.Generic;

namespace VacationRental.Domain.Queries
{
    public class CalendarQuery
    {
        public int RentalId { get; set; }
        public List<CalendarDateQuery> Dates { get; set; }
    }
}
