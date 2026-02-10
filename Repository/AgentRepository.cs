using B2BManagement.Constant;
using B2BManagement.Data;
using B2BManagement.DTOs;
using B2BManagement.Models;
using B2BManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B2BManagement.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly AppDbContext _context;

        public AgentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Agent?> GetByEmailAsync(string email)
        {
            return await _context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Agent?> GetByIdAsync(int agentId)
        {
            return await _context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AgentID == agentId);
        }

        public async Task<object> RegisterAsync(RegisterAgentDto dto)
        {
            var existing = await _context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (existing != null)
                return new { success = false, message = AppConstant.AlreadyExist };

            var agent = new Agent
            {
                CompanyName = dto.CompanyName,
                ContactPerson = dto.ContactPerson,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                ApiKey = Guid.NewGuid().ToString("N"),
                RegisteredOn = DateTime.UtcNow
            };
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();
            return new { success = true, message = AppConstant.AgentCreated, agentId = agent.AgentID };
        }

        public async Task<Agent?> TryValidateLoginAsync(string email, string password)
        {
            var agent = await _context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email);
            if (agent == null || !BCrypt.Net.BCrypt.Verify(password, agent.PasswordHash))
                return null;
            return agent;
        }
    }
}
