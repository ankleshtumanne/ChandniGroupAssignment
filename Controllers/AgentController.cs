using B2BManagement.DTOs;
using B2BManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B2BManagement.Controllers
{
    [ApiController]
    [Route("api/agent")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAgentDto dto)
        {
            var result = await _agentService.RegisterAgentAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _agentService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
