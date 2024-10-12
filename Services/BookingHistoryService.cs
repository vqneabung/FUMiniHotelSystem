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
        public readonly IBookingHistoryRepository _bookingHistoryRepository;

        public BookingHistoryService(IBookingHistoryRepository bookingHistoryRepository)
        {
            _bookingHistoryRepository = bookingHistoryRepository;
        }

        public void AddBookingHistory(BookingHistory bookingHistory)
        {
            _bookingHistoryRepository.AddBookingHistory(bookingHistory);

        }

        public void DeleteBookingHistory(int id)
        {
            _bookingHistoryRepository.DeleteBookingHistory(id);
        }

        public List<BookingHistory> GetAllBookingHistories()
        {
            return _bookingHistoryRepository.GetAllBookingHistories();
        }

        public BookingHistory GetBookingHistoryById(int bookingHistoryId)
        {
            return _bookingHistoryRepository.GetBookingHistoryById(bookingHistoryId);
        }

        public void UpdateBookingHistory(BookingHistory bookingHistory)
        {
            _bookingHistoryRepository.UpdateBookingHistory(bookingHistory);
        }
    }
}
