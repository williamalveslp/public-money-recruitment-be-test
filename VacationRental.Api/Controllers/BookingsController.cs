﻿using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Controllers.Base;
using VacationRental.Application.AppInterfaces;
using VacationRental.Application.ViewModels;
using VacationRental.Domain.Core.Notifications;

namespace VacationRental.Api.Controllers
{
    /// <summary>
    /// Controller for Bookings.
    /// </summary>
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ApiBaseController
    {
        private readonly IBookingAppService _bookingApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="bookingApp"></param>
        public BookingsController(
            INotificationHandler<DomainNotification> notifications,
            IBookingAppService bookingApp) : base(notifications)
        {
            _bookingApp = bookingApp;
        }

        /// <summary>
        /// Get the Booking by Id.
        /// </summary>
        /// <param name="bookingId">Booking Id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{bookingId:int}")]
        [ProducesResponseType(typeof(BookingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status500InternalServerError)]
        public ActionResult<BookingViewModel> Get(int bookingId)
        {
            return Response(_bookingApp.GetById(bookingId));
        }

        /// <summary>
        /// Insert the Booking.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResourceIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationException), StatusCodes.Status500InternalServerError)]
        public ActionResult<ResourceIdViewModel> Post(BookingBindingModel viewModel)
        {
            return Response(_bookingApp.Insert(viewModel));
        }
    }
}
