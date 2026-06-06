
using InventoryManagement.Domain.Entities;


namespace InventoryManagement.Application.Common.Interfaces.Authentication
{
    public interface IUserRepository
    {
        Task<User?> ObtenerPorUserNameAsync(string userName);
    }
}
