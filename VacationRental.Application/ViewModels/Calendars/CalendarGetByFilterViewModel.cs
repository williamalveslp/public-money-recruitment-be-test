using System;

namespace VacationRental.Application.ViewModels.Calendars
{
    public class CalendarGetByFilterViewModel
    {
        public int RentalId { get; set; }

        public DateTime Start { get; set; }

        public int Nights { get; set; }
    }
}
