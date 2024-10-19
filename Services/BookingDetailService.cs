using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingDetailService : IBookingDetailService
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

        public void DeleteBookingDetail(int bookingReservationId, int roomInformationId)
        {
            _bookingDetailRepository.DeleteBookingDetail(bookingReservationId, roomInformationId);
        }

        public List<BookingDetail> GetAllBookingDetails()
        {
            return _bookingDetailRepository.GetAllBookingDetails();
        }

        public BookingDetail GetBookingDetailByBookingReservationAndRoomInformation(int bookingReservationId, int roomInformationId)
        {
            return _bookingDetailRepository.GetBookingDetailByBookingReservationAndRoomInformation(bookingReservationId, roomInformationId);
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _bookingDetailRepository.UpdateBookingDetail(bookingDetail);
        }
    }
}
