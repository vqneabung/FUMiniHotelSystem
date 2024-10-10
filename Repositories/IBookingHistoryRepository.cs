using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingHistoryRepository
    {
        void AddBookingHistory(BookingHistory bookingHistory);
        void DeleteBookingHistory(int id);
        BookingHistory GetBookingHistoryById(int bookingHistoryId);
        List<BookingHistory> GetAllBookingHistories();
    }
}
