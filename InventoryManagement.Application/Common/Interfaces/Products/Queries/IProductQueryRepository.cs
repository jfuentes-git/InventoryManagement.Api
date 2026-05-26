

using InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct;
using InventoryManagement.Domain.Entities;

namespace Common.Interfaces.Products.Query
{
    public interface IProductQueryRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
        Task<bool> ExistsByProductNameAsync(string Name, CancellationToken cancellationToken);
    }
}
