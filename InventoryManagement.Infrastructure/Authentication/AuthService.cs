using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Settings;
using InventoryManagement.Application.Features.Authentication.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Authentication
{
    public sealed class AuthService : IAuthService
    {
        private readonly AuthSettings _authSettings;

        public AuthService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public Task<bool> ValidateCredentials(UsuarioDTO Usuario)
        {

            if (_authSettings.UserName != Usuario.UsserName && _authSettings.Password != Usuario.Password)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);

        }
    }
}
    