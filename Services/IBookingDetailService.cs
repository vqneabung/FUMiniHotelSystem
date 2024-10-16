using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingDetailService
    {
        void AddBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int id);
        List<BookingDetail> GetAllBookingDetails();
        BookingDetail GetBookingDetailById(int bookingDetailId);

        void UpdateBookingDetail(BookingDetail bookingDetail);

    }
}
