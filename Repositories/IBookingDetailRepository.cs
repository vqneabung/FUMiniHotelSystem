using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        void AddBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int bookingReservationId, int roomInformationId);
        List<BookingDetail> GetAllBookingDetails();
        BookingDetail GetBookingDetailByBookingReservationAndRoomInformation(int bookingReservationId, int roomId);

        void UpdateBookingDetail(BookingDetail bookingDetail);
    }
}
