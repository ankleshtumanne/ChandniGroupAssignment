using B2BManagement.DTOs;
using B2BManagement.Models;

namespace B2BManagement.Repository.Interfaces
{
    public interface IAgentRepository
    {
        Task<object> RegisterAsync(RegisterAgentDto dto);
        Task<Agent?> TryValidateLoginAsync(string email, string password);
        Task<Agent?> GetByEmailAsync(string email);
        Task<Agent?> GetByIdAsync(int agentId);
    }
}
