using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        public BookingHistoryDAO _bookingHistoryDAO;
        public CustomerDAO _customerDAO;
        public RoomDAO _roomDAO;

        public BookingHistoryRepository(BookingHistoryDAO bookingHistoryDAO,CustomerDAO customerDAO, RoomDAO roomDAO)
        {
            _bookingHistoryDAO = bookingHistoryDAO;
            _customerDAO = customerDAO;
            _roomDAO = roomDAO;
        }


        public void AddBookingHistory(BookingHistory bookingHistory)
        {
            _bookingHistoryDAO.AddBookingHistory(bookingHistory);
        }

        public void DeleteBookingHistory(int id)
        {
            _bookingHistoryDAO.DeleteBookingHistory(id);
        }

        public List<BookingHistory> GetAllBookingHistories()
        {
            foreach (var bookingHistory in _bookingHistoryDAO.GetAllBookingHistories())
            {
                bookingHistory.Customer = _customerDAO.GetCustomerById(bookingHistory.CustomerID);
                bookingHistory.Room = _roomDAO.GetRoomById(bookingHistory.RoomID);
            }

            return _bookingHistoryDAO.GetAllBookingHistories();
        }

        public BookingHistory GetBookingHistoryById(int bookingHistoryId)
        {
            return _bookingHistoryDAO.GetBookingHistoryById(bookingHistoryId);
        }

        public void UpdateBookingHistory(BookingHistory bookingHistory)
        {
            _bookingHistoryDAO.UpdateBookingHistory(bookingHistory);
        }
    }
}
