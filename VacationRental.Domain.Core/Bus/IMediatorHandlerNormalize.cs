using VacationRental.Domain.Core.Events;
using System.Threading.Tasks;

namespace VacationRental.Domain.Core.Bus
{
    public interface IMediatorHandlerNormalize
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
