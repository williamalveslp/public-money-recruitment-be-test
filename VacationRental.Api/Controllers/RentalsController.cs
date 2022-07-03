using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Rentals.
    /// </summary>
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsAppService _rentalsApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rentalsApp"></param>
        public RentalsController(IRentalsAppService rentalsApp)
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
        public RentalViewModel Get(int rentalId)
        {
            return _rentalsApp.GetById(rentalId);
        }

        /// <summary>
        /// Insert the Rental.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResourceIdViewModel), StatusCodes.Status200OK)]
        public ResourceIdViewModel Post(RentalBindingModel viewModel)
        {
            return _rentalsApp.Insert(viewModel);
        }
    }
}
