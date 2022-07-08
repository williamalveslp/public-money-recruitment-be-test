using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Controllers.Base;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.Core.Requests;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Rentals.
    /// </summary>
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ApiBaseController
    {
        private readonly IRentalsAppService _rentalsApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="rentalsApp"></param>
        public RentalsController(
            INotificationHandler<DomainNotificationRequest> notifications,
            IRentalsAppService rentalsApp) : base(notifications)
        {
            this._rentalsApp = rentalsApp;
        }

        /// <summary>
        /// Get Rental by Id.
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{rentalId:int}")]
        [ProducesResponseType(typeof(RentalViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status500InternalServerError)]
        public ActionResult<RentalViewModel> Get(int rentalId)
        {
            return Response(_rentalsApp.GetById(rentalId));
        }

        /// <summary>
        /// Insert the Rental.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResourceIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status500InternalServerError)]
        public ActionResult<ResourceIdViewModel> Post(RentalBindingModel viewModel)
        {
            if (!ModelState.IsValid)
                return ResponseInvalidModelState();

            return Response(_rentalsApp.Insert(viewModel));
        }
    }
}
