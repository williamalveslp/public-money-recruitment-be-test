using System.Collections.Generic;
using VacationRental.Domain.Core.Bus;
using VacationRental.Domain.Core.Notifications;

namespace VacationRental.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandlerNormalize _bus;

        public CommandHandler(IMediatorHandlerNormalize bus)
        {
            _bus = bus;
        }

        protected void NotifyValidationErrors(string errorMessage)
        {
            _bus.RaiseEvent(new DomainNotification(errorMessage));
        }

        protected void NotifyValidationErrors(List<string> errorMessages)
        {
            if (errorMessages == null)
                return;

            foreach (var item in errorMessages)
            {
                _bus.RaiseEvent(new DomainNotification(item));
            }
        }
    }
}
