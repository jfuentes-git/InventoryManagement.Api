
using InventoryManagement.Domain.Entities;


namespace InventoryManagement.Application.Common.Interfaces.Authentication
{
    public interface IAuthService
    {
       Task<bool> ValidateCredentials(User storedUser, string inputPassword);
    }
}
