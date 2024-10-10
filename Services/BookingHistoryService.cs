using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        public readonly IBookingHistoryRepository bookingHistoryRepository;

        public BookingHistoryService()
        {
            bookingHistoryRepository = new BookingHistoryRepository();
        }

        public void AddBookingHistory(BookingHistory bookingHistory)
        {
            bookingHistoryRepository.AddBookingHistory(bookingHistory);

        }

        public void DeleteBookingHistory(int id)
        {
            bookingHistoryRepository.DeleteBookingHistory(id);
        }

        public List<BookingHistory> GetAllBookingHistories()
        {
            return bookingHistoryRepository.GetAllBookingHistories();
        }

        public BookingHistory GetBookingHistoryById(int bookingHistoryId)
        {
            return bookingHistoryRepository.GetBookingHistoryById(bookingHistoryId);
        }
    }
}
