using System;
using System.Collections.Generic;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Queries;

namespace VacationRental.Domain.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Bookings Insert(int id, int rentalId, DateTime start, int nights);

        Bookings Insert(int rentalId, DateTime start, int nights);

        Bookings GetById(int bookingId);

        IDictionary<int, Bookings> GetAll();

        int GetNextId();

        bool IsAvailable(int rentalId, DateTime start, int nights, IDictionary<int, Rental> rentals);

        CalendarQuery CalendarDatesNormalized(int rentalId, int nights, DateTime start);
    }
}
