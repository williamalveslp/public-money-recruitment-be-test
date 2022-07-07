using VacationRental.Domain.Core.Events;
using System.Threading.Tasks;

namespace VacationRental.Domain.Core.Bus
{
    public interface IMediatorHandlerNormalize
    {
        /// <summary>
        /// It calls the event handler.
        /// </summary>
        /// <typeparam name="T">Event to call a handler.</typeparam>
        /// <param name="event">The generic object will extends these properties as well when will be sent to handler.</param>
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
