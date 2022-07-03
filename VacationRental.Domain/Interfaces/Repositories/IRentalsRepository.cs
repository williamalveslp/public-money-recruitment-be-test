using System.Collections.Generic;
using VacationRental.Domain.Entities;

namespace VacationRental.Domain.Interfaces.Repositories
{
    public interface IRentalsRepository
    {
        Rental Insert(int units);

        Rental GetById(int rentalId);

        IDictionary<int, Rental> GetAll();
    }
}
