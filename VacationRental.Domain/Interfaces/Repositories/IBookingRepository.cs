using System;
using System.Collections.Generic;
using VacationRental.Domain.Entities;

namespace VacationRental.Domain.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Bookings Insert(int id, int rentalId, DateTime start, int nights);

        Bookings Insert(int rentalId, DateTime start, int nights);

        Bookings GetById(int bookingId);

        IDictionary<int, Bookings> GetAll();

        int GetNextId();
    }
}
