using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingReservationService
    {
        void AddBookingReservation(BookingReservation bookingHistory);
        void DeleteBookingReservation(int id);
        BookingReservation GetBookingReservationById(int bookingHistoryId);
        List<BookingReservation> GetAllBookingReservations();

        void UpdateBookingReservation(BookingReservation bookingHistory);

    }
}
