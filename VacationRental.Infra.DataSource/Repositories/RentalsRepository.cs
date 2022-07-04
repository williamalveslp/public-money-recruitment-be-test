using System.Collections.Generic;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Infra.DataSource.Repositories
{
    public class RentalsRepository : RepositoryBase<Rental>, IRentalsRepository
    {
        private readonly IDictionary<int, Rental> _rentals;

        public RentalsRepository(IDictionary<int, Rental> rentals)
        {
            this._rentals = rentals;
        }

        public IDictionary<int, Rental> GetAll()
        {
            return _rentals;
        }

        public Rental GetById(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                return null;

            return _rentals[rentalId];
        }

        public Rental Insert(int units)
        {
            int newId = GetNextId(_rentals);

            var entity = new Rental(newId, units);

            _rentals.Add(newId, entity);

            return entity;
        }
    }
}
