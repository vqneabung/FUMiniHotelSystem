using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BookingHistoryDAO
    {
        public List<BookingHistory> listBookingHistories = new List<BookingHistory>();

        public BookingHistoryDAO()
        {
            var bookingHistory1 = new BookingHistory
            {
                BookingID = 1,
                BookingStatus = 1,
                CheckInDate = new DateTime(2021, 1, 1),
                CheckOutDate = new DateTime(2021, 1, 5),
                CustomerID = 1,
                NumberOfPeople = 2,
                RoomID = 1,
                TotalPrice = 1000
            };
            listBookingHistories.Add(bookingHistory1);

        }

        public void AddBookingHistory(BookingHistory bookingHistory)
        {
            if (listBookingHistories.Count == 0)
            {
                bookingHistory.BookingID = 1;
            }
            else
            {
                bookingHistory.BookingID = listBookingHistories[listBookingHistories.Count - 1].BookingID + 1;
            }

            listBookingHistories.Add(bookingHistory);
        }

        public void DeleteBookingHistory(int id)
        {
            for (int i = 0; i < listBookingHistories.Count; i++)
            {
                if (listBookingHistories[i].BookingID == id)
                {
                    listBookingHistories.RemoveAt(i);
                    break;
                }
            }
        }

        public List<BookingHistory> GetAllBookingHistories()
        {
            return listBookingHistories;
        }


        public BookingHistory GetBookingHistoryById(int id)
        {
            for (int i = 0; i < listBookingHistories.Count; i++)
            {
                if (listBookingHistories[i].BookingID == id)
                {
                    return listBookingHistories[i];
                }
            }
            return null;
        }

        public void UpdateBookingHistory(BookingHistory bookingHistory)
        {
            for (int i = 0; i < listBookingHistories.Count; i++)
            {
                if (listBookingHistories[i].BookingID == bookingHistory.BookingID)
                {
                    listBookingHistories[i] = bookingHistory;
                    break;
                }
            }
        }



    }
}
