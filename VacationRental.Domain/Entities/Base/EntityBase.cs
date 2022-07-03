namespace VacationRental.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        protected virtual int Id { get; private set; }

        protected EntityBase(int id)
        {
            this.Id = id;
        }
    }
}
