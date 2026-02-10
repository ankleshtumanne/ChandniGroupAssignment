using B2BManagement.DTOs;

namespace B2BManagement.Services.Interfaces
{
    public interface IHotelService
    {
        Task<object> SearchHotelsAsync(HotelSearchDto dto);
        Task<object> CreateBookingAsync(int agentId, HotelBookingDto dto);
        Task<object> GetMyBookingsAsync(int agentId);
    }
}
