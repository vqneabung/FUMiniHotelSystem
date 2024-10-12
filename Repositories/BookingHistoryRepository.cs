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
        BookingHistoryDAO BookingHistoryDAO = new BookingHistoryDAO();

        public void AddBookingHistory(BookingHistory bookingHistory)
        {
            BookingHistoryDAO.AddBookingHistory(bookingHistory);
        }

        public void DeleteBookingHistory(int id)
        {
            BookingHistoryDAO.DeleteBookingHistory(id);
        }

        public List<BookingHistory> GetAllBookingHistories()
        {
            foreach (var bookingHistory in BookingHistoryDAO.GetAllBookingHistories())
            {
                bookingHistory.Customer = CustomerRepository.CustomerDAO.GetCustomerById(bookingHistory.CustomerID);
                bookingHistory.Room = RoomRepository.RoomDAO.GetRoomById(bookingHistory.RoomID);
            }

            return BookingHistoryDAO.GetAllBookingHistories();
        }

        public BookingHistory GetBookingHistoryById(int bookingHistoryId)
        {
            return BookingHistoryDAO.GetBookingHistoryById(bookingHistoryId);
        }

        public void UpdateBookingHistory(BookingHistory bookingHistory)
        {
            BookingHistoryDAO.UpdateBookingHistory(bookingHistory);
        }
    }
}
