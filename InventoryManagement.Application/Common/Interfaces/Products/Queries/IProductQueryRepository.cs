

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Common.Interfaces.Products.Queries
{
    public interface IProductQueryRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}
