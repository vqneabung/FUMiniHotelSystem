using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingDetailService
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;

        public BookingDetailService(IBookingDetailRepository bookingDetailRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
        }

        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            _bookingDetailRepository.AddBookingDetail(bookingDetail);
        }

        public void DeleteBookingDetail(int id)
        {
            _bookingDetailRepository.DeleteBookingDetail(id);
        }

        public List<BookingDetail> GetAllBookingDetails()
        {
            return _bookingDetailRepository.GetAllBookingDetails();
        }

        public BookingDetail GetBookingDetailById(int booking)
        {
            return _bookingDetailRepository.GetBookingDetailById(booking);
        }
    }
}
