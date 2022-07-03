using System.Collections.Generic;

namespace VacationRental.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T>
    {
        int GetNextId(IDictionary<int, T> keyValuePairs);
    }
}
