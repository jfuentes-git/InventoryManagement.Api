using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace InventoryManagement.Infrastructure.Authentication
{
    public sealed class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(string username)
        {
            var claims = new List<Claim>{ new(ClaimTypes.Name, username) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
