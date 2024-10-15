using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        void AddBookingReservation(BookingReservation bookingHistory);
        void DeleteBookingReservation(int id);
        List<BookingReservation> GetAllBookingReservations();
        BookingReservation GetBookingReservationById(int booking);

        void UpdateBookingReservation(BookingReservation bookingHistory);

    }
}
