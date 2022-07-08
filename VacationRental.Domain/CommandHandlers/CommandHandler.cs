using MediatR;
using System.Collections.Generic;
using System.Net;
using VacationRental.Domain.Core.Interfaces;
using VacationRental.Domain.Core.Requests;

namespace VacationRental.Domain.CommandHandlers
{
    public class CommandHandler
    {
        /// <summary>
        /// Mediator Handler to calls Bus handler.
        /// </summary>
        private readonly IMediatorHandlerNormalize _mediatorHandler;

        private readonly IMediator _mediator;

        public CommandHandler(IMediatorHandlerNormalize mediatorHandler, IMediator mediator)
        {
            _mediatorHandler = mediatorHandler;
            _mediator = mediator;
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Error message.</param>
        protected void NotifyValidationErrors(string errorMessage)
        {
            PublishNotificationHandler(new DomainNotificationRequest(errorMessage));
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Errors message.</param>
        /// <param name="httpStatusCode">HTTPStatusCode related to notification.</param>
        protected void NotifyValidationErrors(string errorMessage, HttpStatusCode httpStatusCode)
        {
            PublishNotificationHandler(new DomainNotificationRequest(errorMessage, httpStatusCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages">List of the errors message.</param>
        /// <param name="httpStatusCode">HTTPStatusCode related to notification.</param>
        protected void NotifyValidationErrors(IList<string> errorMessages, HttpStatusCode httpStatusCode)
        {
            if (errorMessages == null)
                return;

            foreach (var item in errorMessages)
            {
                NotifyValidationErrors(item, httpStatusCode);
            }
        }

        private void PublishNotificationHandler(DomainNotificationRequest request)
        {
            _mediator.Publish(request);

           // _mediatorHandler.RaiseEvent(request);
        }
    }
}
