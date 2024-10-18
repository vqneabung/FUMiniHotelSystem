using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BookingReservationDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingReservationDAO(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public void AddBookingReservation(BookingReservation bookingReservation)
        {
  
            bookingReservation.BookingReservationId = _context.BookingReservations.Max(b => b.BookingReservationId) + 1;
           _context.BookingReservations.Add(bookingReservation);
            _context.SaveChanges();

        }

        public void DeleteBookingReservation(int id)
        {
            var bookingReservation = GetBookingReservationById(id);
            if (bookingReservation != null)
            {
                _context.BookingReservations.Remove(bookingReservation);
                _context.SaveChanges();
            }
        }

        public List<BookingReservation> GetAllBookingReservations()
        {
            return _context.BookingReservations
                .Include(b => b.Customer)
                .Include(b => b.BookingDetails)
                .ToList();
        }


        public BookingReservation? GetBookingReservationById(int id)
        {
            return _context.BookingReservations
                .Include(b => b.Customer)
                .Include(b => b.BookingDetails)
                .FirstOrDefault(b => b.BookingReservationId == id);
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Update(bookingReservation);
            _context.Entry(bookingReservation).State = EntityState.Modified;
            _context.SaveChanges();
        }



    }
}
