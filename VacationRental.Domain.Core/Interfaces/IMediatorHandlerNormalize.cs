using VacationRental.Domain.Core.Events;
using System.Threading.Tasks;

namespace VacationRental.Domain.Core.Interfaces
{
    /// <summary>
    /// Interface to normalized classes related to notification handlers.
    /// </summary>
    public interface IMediatorHandlerNormalize
    {
        /// <summary>
        /// It calls Notification Handler.
        /// </summary>
        /// <typeparam name="T">Event to call a handler.</typeparam>
        /// <returns></returns>
        Task RaiseEvent<T>(T eventRequest);
    }
}
