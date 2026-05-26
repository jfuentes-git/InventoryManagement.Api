using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId
{
    public sealed record InventoryMovementResponse
  (
      Guid Id,
      Guid ProductId,
      string MovementType,
      int Quantity,
      DateTime CreatedAt
  );
}
