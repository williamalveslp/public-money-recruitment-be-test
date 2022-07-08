using System;
using MediatR;

namespace VacationRental.Domain.Core.Events
{
    /// <summary>
    /// Abstract class as event using MediatR.
    /// </summary>
    public abstract class Event : INotification //, IRequest
    {
        /// <summary>
        /// Timestamp of the notification.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Type of class being sending.
        /// </summary>
        public string MessageType { get; protected set; }

        public Guid AggregateId { get; protected set; }

        protected Event()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
