using B2BManagement.DTOs;
using B2BManagement.Repository.Interfaces;
using B2BManagement.Services.Interfaces;

namespace B2BManagement.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBookingRepository _bookingRepository;

        public HotelService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<object> SearchHotelsAsync(HotelSearchDto dto)
        {
            return await _bookingRepository.SearchHotelsAsync(dto);
        }

        public async Task<object> CreateBookingAsync(int agentId, HotelBookingDto dto)
        {
            return await _bookingRepository.CreateBookingAsync(agentId, dto);
        }

        public async Task<object> GetMyBookingsAsync(int agentId)
        {
            return await _bookingRepository.GetMyBookingsAsync(agentId);
        }
    }
}
