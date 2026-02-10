using B2BManagement.DTOs;
using B2BManagement.Models;

namespace B2BManagement.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task<object> SearchHotelsAsync(HotelSearchDto dto);
        Task<object> CreateBookingAsync(int agentId, HotelBookingDto dto);
        Task<object> GetMyBookingsAsync(int agentId);
        Task<HotelBooking?> GetByIdAsync(int bookingId);
    }
}
