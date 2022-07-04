using MediatR;
using VacationRental.Domain.Core.Bus;
using VacationRental.Domain.Core.Commands;
using VacationRental.Domain.Core.Notifications;

namespace VacationRental.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandlerNormalize _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IMediatorHandlerNormalize bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(string messageType, string errorMessage)
        {
            _bus.RaiseEvent(new DomainNotification(messageType, errorMessage));
        }

        protected void NotifyValidationErrors(string errorMessage)
        {
            _bus.RaiseEvent(new DomainNotification(errorMessage));
        }

        protected void NotifyValidationErrors(ResultedCommand<long> message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications())
                return false;

            //if (_uow.Commit())
            //    return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "Issues to save the data."));
            return false;
        }
    }
}
