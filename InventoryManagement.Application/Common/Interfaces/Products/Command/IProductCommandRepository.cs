

using InventoryManagement.Domain.Entities;
using System.Data.Common;

namespace InventoryManagement.Application.Common.Interfaces.Products.Command
{
    public interface IProductCommandRepository
    {
        Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken);
        Task<bool> UpdateStockAsync(Guid productId, int newStock, DbTransaction transaction, CancellationToken cancellationToken);
    }
}
