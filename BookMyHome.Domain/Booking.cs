namespace BookMyHome.Domain
{
    public class Booking
    {
        public Guid BookingID { get; set; } //Pkey
        public Guid ApartmentID { get; set; } //Fkey
        public Guid UserID { get; set; } //Fkey
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Apartment Apartment { get; set; }
        public User User { get; set; }
    }



}
