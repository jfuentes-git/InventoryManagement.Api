using MediatR;


namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId
{
    public sealed record GetInventoryMovementByProductIdQuery(Guid ProductId): IRequest<List<InventoryMovementResponse>>;
}
