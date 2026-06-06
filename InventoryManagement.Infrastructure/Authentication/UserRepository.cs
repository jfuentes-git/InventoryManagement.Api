using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Settings;
using InventoryManagement.Domain.Entities;
using Microsoft.Extensions.Options;

namespace InventoryManagement.Infrastructure.Authentication
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AuthSettings _authSettings;

        public UserRepository(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public  Task<User?> ObtenerPorUserNameAsync(string userName)
        {
            return Task.FromResult( _authSettings.UserName.Equals(userName) ? MapUser() : null);
        }

        private User MapUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = _authSettings.UserName,
                Password = _authSettings.Password,
                IsActive = true
            };
        }
    }
}