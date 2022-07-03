using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Handle(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
