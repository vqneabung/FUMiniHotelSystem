using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public  class BookingDetailDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingDetailDAO(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            _context.SaveChanges();
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Update(bookingDetail);
            _context.Entry(bookingDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBookingDetail(int bookingDetailId)
        {
            var bookingDetail = GetBookingDetailById(bookingDetailId);
            if (bookingDetail != null)
            {
                _context.BookingDetails.Remove(bookingDetail);
                _context.SaveChanges();
            }
        }

        public BookingDetail? GetBookingDetailById(int booking)
        {
            return _context.BookingDetails.Find(booking);

        }

        public List<BookingDetail> GetAllBookingDetails()
        {
            return _context.BookingDetails.ToList();

        }
    }
}
