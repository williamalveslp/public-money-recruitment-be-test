using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VacationRental.Domain.Core.Handlers;
using VacationRental.Domain.Core.Requests;

namespace VacationRental.Api.Controllers.Base
{
    /// <summary>
    /// Controller base.
    /// </summary>
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApiBaseController(INotificationHandler<DomainNotificationRequest> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        /// <summary>
        /// Response unificado para os endpoints.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected new ActionResult Response(object response = null)
        {
            var errors = _notifications.GetNotifications()?.Select(f => f?.Value);

            if (response == null)
                return BadRequest(new ApplicationException(string.Join(",", errors)));

            if (!IsValidOperation())
                return BadRequest(new ApplicationException(string.Join(",", errors)));

            return Ok(response);
        }

        /// <summary>
        /// Response about that the Model State is invalid.
        /// </summary>
        /// <returns></returns>
        protected ActionResult InvalidModelState()
        {
            return BadRequest(new ApplicationException("ModelState is invalid."));
        }

        private bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }
    }
}
