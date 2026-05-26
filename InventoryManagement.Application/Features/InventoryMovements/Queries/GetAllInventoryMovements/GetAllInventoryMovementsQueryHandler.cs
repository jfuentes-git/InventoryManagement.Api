using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using MediatR;

namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetAllInventoryMovements
{

    public sealed class GetAllInventoryMovementsQueryHandler : IRequestHandler<GetAllInventoryMovementsQuery, List<InventoryMovementResponse>>
    {
        private readonly IInventoryMovementQueryRepository _repository;

        public GetAllInventoryMovementsQueryHandler(IInventoryMovementQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryMovementResponse>> Handle(GetAllInventoryMovementsQuery request,
            CancellationToken cancellationToken)
        {
            var movements = await _repository.GetAllAsync(cancellationToken);

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
