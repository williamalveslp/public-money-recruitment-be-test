using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Domain.Core.Requests;

namespace VacationRental.Domain.Core.Handlers
{
    /// <summary>
    /// Notification handler.
    /// </summary>
    public class DomainNotificationHandler : INotificationHandler<DomainNotificationRequest>, IDisposable
    {
        /// <summary>
        /// List of notifications.
        /// </summary>
        private IList<DomainNotificationRequest> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotificationRequest>();
        }

        /// <summary>
        /// Handler to storage the notification in memory.
        /// </summary>
        /// <param name="notification">Notification request.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        public Task Handle(DomainNotificationRequest notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        /// <summary>
        /// List all notifications.
        /// </summary>
        /// <returns></returns>
        public virtual IList<DomainNotificationRequest> GetNotifications()
        {
            return _notifications;
        }

        /// <summary>
        /// Check is there are notifications or not.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = null;
            GC.SuppressFinalize(this);
        }
    }
}
