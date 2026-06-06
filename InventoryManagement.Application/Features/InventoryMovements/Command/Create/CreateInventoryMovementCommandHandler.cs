
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Common.Interfaces.UnitOfWork;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;


namespace InventoryManagement.Application.Features.InventoryMovements.Command.Create
{
    public sealed class CreateInventoryMovementCommandHandler: IRequestHandler<CreateInventoryMovementCommand, CreatedResponse>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IInventoryMovementCommandRepository _inventoryMovementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCommandRepository _productCommandRepository;

        public CreateInventoryMovementCommandHandler(
            IProductQueryRepository productQueryRepository,
            IInventoryMovementCommandRepository inventoryMovementRepository,
            IUnitOfWork unitOfWork,
            IProductCommandRepository ProductCommandRepository
            )
        {
            _productQueryRepository = productQueryRepository;
            _inventoryMovementRepository = inventoryMovementRepository;
            _unitOfWork = unitOfWork;
            _productCommandRepository = ProductCommandRepository;
        }

        public async Task<CreatedResponse> Handle( CreateInventoryMovementCommand request,
            CancellationToken cancellationToken)

        {
            var product = await _productQueryRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                throw new NotFoundException("Producto no encontrado.");

            product.ApplyMovement(request.MovementType,request.Quantity);

            var inventoryMovement = new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                MovementType = request.MovementType,
                Quantity = request.Quantity
            };

            var movementId = await ProcessInventoryMovementAsync(inventoryMovement, product.Stock, cancellationToken);

            return new CreatedResponse(movementId, "Movimiento de Inventario Creado satisfactoriamente.");
        }

        private async Task<Guid> ProcessInventoryMovementAsync(InventoryMovement inventoryMovement, int newStock , CancellationToken cancellationToken)
        {

            return await _unitOfWork.ExecuteAsync(
                async transaction =>
                {

                    var  productUpdated = await _productCommandRepository.UpdateStockAsync(
                         inventoryMovement.ProductId,
                         newStock,
                         transaction,
                         cancellationToken);


                    if (!productUpdated)
                    {
                        throw new NotFoundException("Producto no encontrado.");
                    }

                    return await _inventoryMovementRepository.InsertMovementAsync(
                          inventoryMovement,    
                          transaction,
                          cancellationToken);
                },cancellationToken);
         
        }

    }
}
