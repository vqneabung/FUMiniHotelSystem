using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingReservationService : IBookingReservationService
    {
        public readonly IBookingReservationRepository _bookingReservationRepository;

        public BookingReservationService(IBookingReservationRepository bookingReservationRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
        }

        public void AddBookingReservation(BookingReservation bookingReservation)
        {
            _bookingReservationRepository.AddBookingReservation(bookingReservation);

        }

        public void DeleteBookingReservation(int id)
        {
            _bookingReservationRepository.DeleteBookingReservation(id);
        }

        public List<BookingReservation> GetAllBookingReservations()
        {
            return _bookingReservationRepository.GetAllBookingReservations();
        }

        public BookingReservation GetBookingReservationById(int bookingReservationId)
        {
            return _bookingReservationRepository.GetBookingReservationById(bookingReservationId);
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            _bookingReservationRepository.UpdateBookingReservation(bookingReservation);
        }
    }
}
