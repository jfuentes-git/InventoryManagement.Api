
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Features.InventoryMovements.Command.Create
{
    public sealed record CreateInventoryMovementCommand
    (
      Guid ProductId,
      MovementType MovementType,
      int Quantity
    ) : IRequest<CreatedResponse>;
}
