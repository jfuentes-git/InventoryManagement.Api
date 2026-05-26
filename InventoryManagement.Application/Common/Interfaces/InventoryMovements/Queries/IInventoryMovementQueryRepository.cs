using InventoryManagement.Domain.Entities;
using System.Data;

namespace   InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries
{
    public interface IInventoryMovementQueryRepository
    {
        Task<List<InventoryMovement>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<InventoryMovement>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
