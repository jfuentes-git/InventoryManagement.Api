
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId
{
    public sealed class GetInventoryMovementByProductIdQueryHandler
     : IRequestHandler<GetInventoryMovementByProductIdQuery, List<InventoryMovementResponse>>
    {
        private readonly IInventoryMovementQueryRepository _repository;

        public GetInventoryMovementByProductIdQueryHandler(IInventoryMovementQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryMovementResponse>> Handle(GetInventoryMovementByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var movements = await _repository.GetByProductIdAsync(request.ProductId, cancellationToken)
                    ?? new List<InventoryMovement>();

            return movements.Select(x => new InventoryMovementResponse(
                x.Id,
                x.ProductId,
                x.MovementType.ToString(),
                x.Quantity,
                x.CreatedAt
            )).ToList();
        }
    }
}
