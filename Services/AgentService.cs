using B2BManagement.Constant;
using B2BManagement.DTOs;
using B2BManagement.Repository.Interfaces;
using B2BManagement.Services.Interfaces;

namespace B2BManagement.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly JwtService _jwtService;

        public AgentService(IAgentRepository agentRepository, JwtService jwtService)
        {
            _agentRepository = agentRepository;
            _jwtService = jwtService;
        }

        public async Task<object> RegisterAgentAsync(RegisterAgentDto dto)
        {
            return await _agentRepository.RegisterAsync(dto);
        }

        public async Task<object> LoginAsync(LoginDto dto)
        {
            var agent = await _agentRepository.TryValidateLoginAsync(dto.Email, dto.Password);
            if (agent == null)
                return new { success = false, message = AppConstant.InvalidEmailPass };
            var token = _jwtService.GenerateToken(agent);
            return new { success = true, token, agentId = agent.AgentID };
        }
    }
}
