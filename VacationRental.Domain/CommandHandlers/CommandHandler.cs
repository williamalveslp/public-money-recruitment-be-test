using MediatR;
using System.Collections.Generic;
using System.Net;
using VacationRental.Domain.Core.Requests;

namespace VacationRental.Domain.CommandHandlers
{
    /// <summary>
    /// Abstract class related to Handler to interaction patterns.
    /// </summary>
    public abstract class CommandHandler
    {
        /// <summary>
        /// Encapsule Request/Response and publishing the interaction patterns.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        protected CommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Errors message.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            PublishNotificationHandler(new DomainNotificationRequest(errorMessage, httpStatusCode));
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessages">List of the errors message.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(IEnumerable<string> errorMessages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
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
        }
    }
}
