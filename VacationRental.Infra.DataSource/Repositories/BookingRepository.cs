using System;
using System.Collections.Generic;
using VacationRental.Domain.Entities;
using VacationRental.Domain.Interfaces.Repositories;
using VacationRental.Domain.Queries;

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

        public bool IsAvailable(int rentalId, DateTime start, int nights, IDictionary<int, Rental> rentals)
        {
            var bookings = GetAll();

            for (var i = 0; i < nights; i++)
            {
                var count = 0;

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && (booking.Start <= start.Date && booking.Start.AddDays(booking.Nights) > start.Date)
                        || (booking.Start < start.AddDays(nights) && booking.Start.AddDays(booking.Nights) >= start.AddDays(nights))
                        || (booking.Start > start && booking.Start.AddDays(booking.Nights) < start.AddDays(nights)))
                    {
                        count++;
                    }
                }
                if (count >= rentals[rentalId].Units)
                {
                    return false;
                }
            }

            return true;
        }

        public CalendarQuery CalendarDatesNormalized(int rentalId, int nights, DateTime start)
        {
            var bookings = GetAll();

            var result = new CalendarQuery
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateQuery>()
            };

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateQuery
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingQuery>()
                };

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingQuery { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        } 
    }
}
