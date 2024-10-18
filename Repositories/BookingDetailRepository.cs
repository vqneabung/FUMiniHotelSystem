using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly BookingDetailDAO _bookingDetailDAO;

        public BookingDetailRepository(BookingDetailDAO bookingDetailDAO)
        {
            _bookingDetailDAO = bookingDetailDAO;
        }


        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            _bookingDetailDAO.AddBookingDetail(bookingDetail);
        }

        public void DeleteBookingDetail(int id)
        {
          _bookingDetailDAO.DeleteBookingDetail(id);
        }

        public List<BookingDetail> GetAllBookingDetails()
        {
           return _bookingDetailDAO.GetAllBookingDetails();
        }

        public BookingDetail GetBookingDetailByBookingReserveId(int bookingDetailId)
        {
            return _bookingDetailDAO.GetBookingDetailById(bookingDetailId);
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _bookingDetailDAO.UpdateBookingDetail(bookingDetail);
        }
    }
}
