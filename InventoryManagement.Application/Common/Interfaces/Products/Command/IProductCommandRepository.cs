

using InventoryManagement.Domain.Entities;

namespace Common.Interfaces.Products.Command
{
    public interface IProductCommandRepository
    {
        Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken);
    }
}
