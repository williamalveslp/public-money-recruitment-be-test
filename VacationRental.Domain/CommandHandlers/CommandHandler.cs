using System.Collections.Generic;
using System.Net;
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

        protected void NotifyValidationErrors(string errorMessage, HttpStatusCode httpStatusCode)
        {
            _bus.RaiseEvent(new DomainNotification(errorMessage, httpStatusCode));
        }

        protected void NotifyValidationErrors(IList<string> errorMessages, HttpStatusCode httpStatusCode)
        {
            if (errorMessages == null)
                return;

            foreach (var item in errorMessages)
            {
                NotifyValidationErrors(item, httpStatusCode);
            }
        }
    }
}
