﻿using BussinessObjects;
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
        void DeleteBookingDetail(int bookingReservationId, int roomInformationId);
        List<BookingDetail> GetAllBookingDetails();
        BookingDetail GetBookingDetailByBookingReservationAndRoomInformation(int bookingReservationId, int roomInformationId);

        void UpdateBookingDetail(BookingDetail bookingDetail);

    }
}
