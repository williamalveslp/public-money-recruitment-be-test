using System;
using VacationRental.Domain.Entities.Base;

namespace VacationRental.Domain.Entities
{
    public class Bookings : EntityBase
    {
        public virtual int RentalId { get; private set; }
        public virtual DateTime Start { get; private set; }
        public virtual int Nights { get; private set; }

        public Bookings(int id, int rentalId, DateTime start, int night) : base(id)
        {
            this.RentalId = rentalId;
            this.Start = start;
            this.Nights = night;
        }
    }
}
