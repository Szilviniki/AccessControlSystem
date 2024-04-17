using ACS_Backend.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ACS_Backend.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(Personnel user)
        {
            var identity = new ClaimsIdentity(new[] {
                new Claim("ID", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            });

            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Program.TokenEncryptionKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

