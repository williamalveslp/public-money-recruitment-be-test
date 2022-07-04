using System;
using System.Net;
using VacationRental.Domain.Core.Events;

namespace VacationRental.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public int Version { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public DomainNotification(string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Value = value;
            StatusCode = HttpStatusCode.BadRequest;
        }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
            StatusCode = HttpStatusCode.BadRequest;
        }

        public DomainNotification(string key, string value, HttpStatusCode statusCode)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
            StatusCode = statusCode;
        }
    }
}
