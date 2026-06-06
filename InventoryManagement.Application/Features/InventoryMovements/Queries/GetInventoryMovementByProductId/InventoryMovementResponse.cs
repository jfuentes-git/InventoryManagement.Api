

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
