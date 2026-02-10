using B2BManagement.Constant;
using B2BManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace B2BManagement.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }


        public string GenerateToken(Agent agent)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, agent.Email),
            new Claim("AgentID", agent.AgentID.ToString())
            };


            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? AppConstant.DefaultSignInKey);
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
