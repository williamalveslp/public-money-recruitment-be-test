using System.Collections.Generic;
using System.Linq;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Infra.DataSource.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public int GetNextId(IDictionary<int, T> keyValuePairs)
        {
            if (keyValuePairs == null)
                return default;

            if (!keyValuePairs.Any())
                return 1;

            return keyValuePairs.Keys.LastOrDefault() + 1;
        }
    }
}
