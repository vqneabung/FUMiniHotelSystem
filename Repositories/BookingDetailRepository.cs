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

        public void DeleteBookingDetail(int bookingReservationId, int roomInformationId)
        {
          _bookingDetailDAO.DeleteBookingDetail(bookingReservationId, roomInformationId);
        }

        public List<BookingDetail> GetAllBookingDetails()
        {
           return _bookingDetailDAO.GetAllBookingDetails();
        }

        public BookingDetail GetBookingDetailByBookingReservationAndRoomInformation(int bookingReservationId, int roomInformationId)
        {
            return _bookingDetailDAO.GetBookingDetailByBookingReservationAndRoomInformation(bookingReservationId, roomInformationId);
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _bookingDetailDAO.UpdateBookingDetail(bookingDetail);
        }
    }
}
