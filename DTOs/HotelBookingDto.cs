namespace B2BManagement.DTOs
{
    public class HotelBookingDto
    {
        public string HotelID { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Guests { get; set; } = 1;
        public decimal TotalPrice { get; set; }
    }
}
