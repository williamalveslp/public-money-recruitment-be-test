using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VacationRental.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        private IList<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public virtual IList<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _notifications = null;
            GC.SuppressFinalize(this);
        }
    }
}
