namespace BookMyHome.Domain
{
    public class User
    {
        public Guid UserID { get; set; } //Pkey
        public string UserName { get; set; }
        public string AccountType { get; set; } //host/customer
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }



}
