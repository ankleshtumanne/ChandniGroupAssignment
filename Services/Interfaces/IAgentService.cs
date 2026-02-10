using B2BManagement.DTOs;

namespace B2BManagement.Services.Interfaces
{
    public interface IAgentService
    {
        Task<object> RegisterAgentAsync(RegisterAgentDto dto);
        Task<object> LoginAsync(LoginDto dto);
    }
}
