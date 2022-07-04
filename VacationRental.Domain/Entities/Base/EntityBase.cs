namespace VacationRental.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public virtual int Id { get; private set; }

        protected EntityBase() { }

        protected EntityBase(int id)
        {
            this.Id = id;
        }
    }
}
