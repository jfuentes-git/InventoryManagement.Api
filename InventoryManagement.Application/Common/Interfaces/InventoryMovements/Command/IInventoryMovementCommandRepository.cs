using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command
{
    public interface IInventoryMovementCommandRepository
    {
        Task<Guid> ProcessInventoryMovementAsync(InventoryMovement inventoryMovement,int newStock,
                   CancellationToken cancellationToken);
    }

}
    