using B2BManagement.Constant;
using B2BManagement.Data;
using B2BManagement.DTOs;
using B2BManagement.Models;
using B2BManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B2BManagement.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HotelBooking?> GetByIdAsync(int bookingId)
        {
            return await _context.HotelBookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingID == bookingId);
        }

        public async Task<object> SearchHotelsAsync(HotelSearchDto dto)
        {
            if (dto.CheckOut <= dto.CheckIn)
                return new { success = false, message = AppConstant.CheckingOut };
            if (dto.Guests < 1)
                return new { success = false, message = AppConstant.GuestRequired };

            var mockHotels = new[]
            {
                new { hotelId = "HTL001", name = AppConstant.Hotel1, city = dto.City, pricePerNight = 120m, available = true },
                new { hotelId = "HTL002", name = AppConstant.Hotel2, city = dto.City, pricePerNight = 85m, available = true },
                new { hotelId = "HTL003", name = AppConstant.Hotel3, city = dto.City, pricePerNight = 150m, available = true }
            };
            var nights = (int)(dto.CheckOut - dto.CheckIn).TotalDays;
            var results = mockHotels.Select(h => new
            {
                h.hotelId,
                h.name,
                h.city,
                checkIn = dto.CheckIn,
                checkOut = dto.CheckOut,
                guests = dto.Guests,
                nights,
                totalPrice = h.pricePerNight * nights
            }).ToList();

            return await Task.FromResult(new { success = true, results });
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
                APIResponse = "{}",
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
