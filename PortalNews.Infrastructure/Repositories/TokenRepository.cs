using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalNews.Domain.Entities;
using PortalNews.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortalNews.Infrastructure.Repositories
{

    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Journalist journalist)
        {

            var keyCredentials = new SigningCredentials(new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value)), 
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(journalist),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = keyCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(Journalist journalist)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, journalist.Id.ToString()));

            return ci;
        }
    }
}
