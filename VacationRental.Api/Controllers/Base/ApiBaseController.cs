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
        /// <summary>
        /// Notifications.
        /// </summary>
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
            if (_notifications.HasNotifications())
                return ResponseBadRequest();

            return Ok(response);
        }

        /// <summary>
        /// Response about that the Model State is invalid.
        /// </summary>
        /// <returns></returns>
        protected ActionResult ResponseInvalidModelState()
        {
            return BadRequest(new ApplicationException("ModelState is invalid."));
        }

        private ActionResult ResponseBadRequest()
        {
            var errors = _notifications.GetNotifications()?.Select(f => f?.Value);
            return BadRequest(new ApplicationException(string.Join(", ", errors)));
        }
    }
}
