using InventoryManagement.Domain.Entities;
using System.Data.Common;

namespace InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command
{
    public interface IInventoryMovementCommandRepository
    {
       Task<Guid> InsertMovementAsync(InventoryMovement inventoryMovement, DbTransaction transaction, CancellationToken cancellationToken);
    }

}
    