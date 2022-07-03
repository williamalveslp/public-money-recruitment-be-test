using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Rentals.
    /// </summary>
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rentals"></param>
        public RentalsController(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
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
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        /// <summary>
        /// Insert the Rental.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResourceIdViewModel), StatusCodes.Status200OK)]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = model.Units
            });

            return key;
        }
    }
}
