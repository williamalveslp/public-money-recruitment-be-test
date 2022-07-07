using MediatR;
using System;

namespace VacationRental.Domain.Core.Events
{
    /// <summary>
    /// Class responsible for storage data as an audit.
    /// </summary>
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Class responsible as IRequest.
    /// </summary>
    public abstract class Message : IRequest
    {
        public string MessageType { get; protected set; }

        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
