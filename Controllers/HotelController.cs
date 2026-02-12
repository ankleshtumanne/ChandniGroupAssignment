using B2BManagement.DTOs;
using B2BManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("search")]
        
        public async Task<IActionResult> Search([FromQuery] HotelSearchDto dto)
        {
            var result = await _hotelService.SearchHotelsAsync(dto);
            return Ok(result);
        }

        [HttpPost("book")]
        [Authorize]
        public async Task<IActionResult> Book([FromBody] HotelBookingDto dto)
        {
            var agentIdClaim = User.FindFirst("AgentID")?.Value;
            if (string.IsNullOrEmpty(agentIdClaim) || !int.TryParse(agentIdClaim, out var agentId))
                return Unauthorized(new { success = false, message = "Invalid or missing agent token." });
            var result = await _hotelService.CreateBookingAsync(agentId, dto);
            return Ok(result);
        }

        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> MyBookings()
        {
            var agentIdClaim = User.FindFirst("AgentID")?.Value;
            if (string.IsNullOrEmpty(agentIdClaim) || !int.TryParse(agentIdClaim, out var agentId))
                return Unauthorized(new { success = false, message = "Invalid or missing agent token." });
            var result = await _hotelService.GetMyBookingsAsync(agentId);
            return Ok(result);
        }
    }
}
