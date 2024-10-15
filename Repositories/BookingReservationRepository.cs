using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public BookingReservationDAO _bookingReservationDAO;
        public CustomerDAO _customerDAO;
        public RoomInformationDAO _roomDAO;

        public BookingReservationRepository(BookingReservationDAO bookingReservationDAO, CustomerDAO customerDAO, RoomInformationDAO roomDAO)
        {
            _bookingReservationDAO = bookingReservationDAO;
            _customerDAO = customerDAO;
            _roomDAO = roomDAO;
        }


        public void AddBookingReservation(BookingReservation bookingReservation)
        {
            _bookingReservationDAO.AddBookingReservation(bookingReservation);
        }

        public void DeleteBookingReservation(int id)
        {
            _bookingReservationDAO.DeleteBookingReservation(id);
        }

        public List<BookingReservation> GetAllBookingReservations()
        {
            return _bookingReservationDAO.GetAllBookingReservations();
        }

        public BookingReservation GetBookingReservationById(int bookingReservationId)
        {
            return _bookingReservationDAO.GetBookingReservationById(bookingReservationId);
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            _bookingReservationDAO.UpdateBookingReservation(bookingReservation);
        }
    }
}
