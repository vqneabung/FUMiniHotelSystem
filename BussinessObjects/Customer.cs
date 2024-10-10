namespace BussinessObjects
{
    public class Customer()
    {
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public int CustomerStatus { get; set; }
        public string Password { get; set; }
        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
    }
}
