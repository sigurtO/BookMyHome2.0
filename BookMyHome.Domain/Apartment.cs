namespace BookMyHome.Domain
{
    public class Apartment
    {
        public Guid ApartmentID { get; set; } //Pkey
        public Guid UserID { get; set; } //Fkey
        public string Address { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public byte[] Image { get; set; } 
        public bool AvailabiltyStatus { get; set; }



        public User User { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }



}
