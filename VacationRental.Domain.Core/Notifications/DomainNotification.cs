using System;
using System.Net;
using VacationRental.Domain.Core.Events;

namespace VacationRental.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        /// <summary>
        /// Identifier of notification.
        /// </summary>
        public Guid DomainNotificationId { get; private set; }

        /// <summary>
        /// Value of notification. Can be a error message, as example.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// API version.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// HttpStatusCode from request.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public DomainNotification(string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Value = value;
            StatusCode = HttpStatusCode.BadRequest;
        }

        public DomainNotification(string value, HttpStatusCode statusCode)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Value = value;
            StatusCode = statusCode;
        }
    }
}
