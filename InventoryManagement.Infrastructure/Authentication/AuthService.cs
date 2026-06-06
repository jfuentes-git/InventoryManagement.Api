
using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Domain.Entities;


namespace InventoryManagement.Infrastructure.Authentication
{
    public sealed class AuthService : IAuthService
    {
        public Task<bool> ValidateCredentials(User storedUser, string inputPassword)
        {
            return Task.FromResult(storedUser.Password == inputPassword);
        }
    }
}
    