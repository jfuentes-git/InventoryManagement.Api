
using InventoryManagement.Application.Common.Authentication;
using InventoryManagement.Domain.Entities;


namespace InventoryManagement.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<AuthResponse> GenerateToken(User Usuario);
    }
}
