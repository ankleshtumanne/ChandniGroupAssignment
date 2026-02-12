using B2BManagement.Constant;
using B2BManagement.Data;
using B2BManagement.DTOs;
using B2BManagement.Models;
using B2BManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using B2BManagement.Helpers;
using System.Text.Json;
namespace B2BManagement.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public BookingRepository(AppDbContext context, HttpClient httpClientFactory, IConfiguration config)
        {
            _context = context;
            _httpClient = httpClientFactory;
            _config = config;
        }

        public async Task<HotelBooking?> GetByIdAsync(int bookingId)
        {
            return await _context.HotelBookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingID == bookingId);
        }
        public async Task<object> SearchHotelsAsync(HotelSearchDto dto)
        {
            if (dto == null)
                return new { success = false, message = AppConstant.InvalidBookingRequest };

            if (dto.CheckIn <= DateTime.UtcNow.Date)
                return new { success = false, message = AppConstant.CheckingIn };

            if (dto.CheckOut <= dto.CheckIn)
                return new { success = false, message = AppConstant.CheckingOut };

            if (dto.Guests < 1)
                return new { success = false, message = AppConstant.GuestRequired };

            var apiKey = _config["HotelBeds:ApiKey"];
            var secret = _config["HotelBeds:Secret"];
            var baseUrl = _config["HotelBeds:BaseUrl"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(baseUrl))
                return new { success = false, message = AppConstant.HotelConfigurationMissing };

            try
            {   
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                var signatureRaw = apiKey + secret + timestamp;
                using var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureRaw));
                var signature = Convert.ToHexString(hash).ToLower();
                var destinationCode = CityHelper.GetDestinationCode(dto.City);
                if (string.IsNullOrEmpty(destinationCode))
                    return new { success = false, message = "Invalid city provided." };
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Api-key", apiKey);
                _httpClient.DefaultRequestHeaders.Add("X-Signature", signature);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var body = new
                {
                    stay = new
                    {
                        checkIn = dto.CheckIn.ToString("yyyy-MM-dd"),
                        checkOut = dto.CheckOut.ToString("yyyy-MM-dd")
                    },
                    occupancies = new[]
                    {
                        new
                        {
                            rooms = 1,
                            adults = dto.Guests,
                            children = 0
                        }
                    },
                    destination = new
                    {
                        code = destinationCode
                    }
                };
                var content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json"
                );
                var response = await _httpClient.PostAsync(
                    $"{baseUrl}/hotel-api/1.0/hotels",
                    content
                );
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new
                    {
                        success = false,
                        statusCode = response.StatusCode,
                        error = result
                    };
                }
                var jsonResponse = JsonSerializer.Deserialize<object>(result);

                return new
                {
                    success = true,
                    data = jsonResponse
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = AppConstant.ErrorOcured,
                    error = ex.Message
                };
            }
        }

        public async Task<object> CreateBookingAsync(int agentId, HotelBookingDto dto)
        {
            if (dto.CheckOut <= dto.CheckIn)
                return new { success = false, message = AppConstant.CheckingOut };
            if (dto.TotalPrice < 0)
                return new { success = false, message = AppConstant.InvalidTotalPrice };

            var booking = new HotelBooking
            {
                AgentID = agentId,
                HotelID = dto.HotelID,
                HotelName = dto.HotelName,
                City = dto.City,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Guests = dto.Guests,
                TotalPrice = dto.TotalPrice,
                BookingStatus = "Confirmed",
                APIResponse = AppConstant.BookingCreated,
                CreatedOn = DateTime.UtcNow
            };
            _context.HotelBookings.Add(booking);
            await _context.SaveChangesAsync();
            return new { success = true, message = AppConstant.BookingCreated, bookingId = booking.BookingID };
        }

        public async Task<object> GetMyBookingsAsync(int agentId)
        {
            var bookings = await _context.HotelBookings
                .AsNoTracking()
                .Where(b => b.AgentID == agentId)
                .OrderByDescending(b => b.CreatedOn)
                .ToListAsync();
            var list = bookings.Select(b => new
            {
                b.BookingID,
                b.HotelID,
                b.HotelName,
                b.City,
                b.CheckIn,
                b.CheckOut,
                b.Guests,
                b.TotalPrice,
                b.BookingStatus,
                b.CreatedOn
            }).ToList();
            return new { success = true, bookings = list };
        }
    }
}
