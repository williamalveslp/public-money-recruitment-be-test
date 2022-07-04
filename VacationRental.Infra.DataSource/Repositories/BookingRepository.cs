using System;
using System.Collections.Generic;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Interfaces.Repositories;

namespace VacationRental.Infra.DataSource.Repositories
{
    public class BookingRepository : RepositoryBase<Bookings>, IBookingRepository
    {
        private readonly IDictionary<int, Bookings> _bookings;

        public BookingRepository(IDictionary<int, Bookings> bookings)
        {
            this._bookings = bookings;
        }

        public IDictionary<int, Bookings> GetAll()
        {
            return _bookings;
        }

        public Bookings GetById(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                return null;

            return _bookings[bookingId];
        }

        public Bookings Insert(int id, int rentalId, DateTime start, int nights)
        {
            var entity = new Bookings(id, rentalId, start, nights);

            _bookings.Add(id, entity);

            return entity;
        }

        public Bookings Insert(int rentalId, DateTime start, int nights)
        {
            int newId = GetNextId();

            var entity = new Bookings(newId, rentalId, start, nights);

            _bookings.Add(newId, entity);

            return entity;
        }

        public int GetNextId()
        {
            return GetNextId(_bookings);
        }
    }
}
