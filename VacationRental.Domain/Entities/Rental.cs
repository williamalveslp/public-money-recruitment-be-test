using VacationRental.Domain.Entities.Base;

namespace VacationRental.Domain.Entities
{
    public class Rental : EntityBase
    {
        public virtual int Units { get; private set; }

        public Rental(int id, int units) : base(id)
        {
            this.Units = units;
        }

        public Rental(int units)
        {
            this.Units = units;
        }
    }
}
