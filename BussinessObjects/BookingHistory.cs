using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class BookingHistory
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public int BookingStatus { get; set; }
        public double TotalPrice { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }

    }
}
