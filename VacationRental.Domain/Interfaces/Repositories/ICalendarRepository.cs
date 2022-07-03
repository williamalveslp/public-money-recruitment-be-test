using System.Collections.Generic;
using VacationRental.Domain.Entities;

namespace VacationRental.Domain.Interfaces.Repositories
{
    public interface ICalendarRepository
    {
        IDictionary<int, Calendar> GetAll();
    }
}
