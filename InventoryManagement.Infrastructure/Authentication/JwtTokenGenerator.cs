
using InventoryManagement.Application.Common.Authentication;
using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Settings;
using InventoryManagement.Domain.Entities;
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
        public Task<AuthResponse> GenerateToken(User Usuario)
        {

                var claims = new List<Claim>
            {
                 new(ClaimTypes.Name, Usuario.UserName),
                 new(ClaimTypes.NameIdentifier, Usuario.Id.ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);

                var tokenSecurity = new JwtSecurityToken
                        (
                        issuer: _settings.Issuer,
                        audience: _settings.Audience,
                        claims: claims,
                        expires: expiration,
                        signingCredentials: credentials
                        );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);

                return Task.FromResult(new AuthResponse(token, expiration));
        }
    }

}
