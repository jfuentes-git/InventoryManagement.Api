
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using MediatR;


namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetAllInventoryMovements
{

    public sealed record GetAllInventoryMovementsQuery(): IRequest<List<InventoryMovementResponse>>;
}
