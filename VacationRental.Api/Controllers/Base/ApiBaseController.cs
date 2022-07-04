using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VacationRental.Domain.Core.Notifications;

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
        protected ApiBaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        /// <summary>
        /// Response unificado para os endpoints.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected new ActionResult Response(object result = null)
        {
            var errors = _notifications.GetNotifications()?.Select(f => f?.Value);

            if (result == null)
                return BadRequest(new ApplicationException(string.Join(",", errors)));

            if (!IsValidOperation())
                return BadRequest(new ApplicationException(string.Join(",", errors)));

            return Ok(result);
        }

        private bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }
    }
}
