using VacationRental.Domain.Core.Commands;
using VacationRental.Domain.Core.Events;
using System.Threading.Tasks;

namespace VacationRental.Domain.Core.Bus
{
    public interface IMediatorHandlerNormalize
    {
        Task SendCommand<T>(T command) where T : Command;

        Task<TResult> SendCommand<T, TResult>(T command) where T : ResultedCommand<TResult>;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
