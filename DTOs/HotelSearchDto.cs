namespace B2BManagement.DTOs
{
    public class HotelSearchDto
    {
        public string City { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Guests { get; set; } = 1;
        public int? Rooms { get; set; }
    }
}
