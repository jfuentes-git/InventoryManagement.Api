using Common.Interfaces.Products.Query;
using Dapper;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;
using System.Data;


namespace InventoryManagement.Application.Features.InventoryMovements.Command
{

    public sealed class CreateInventoryMovementCommandHandler: IRequestHandler<CreateInventoryMovementCommand, CreatedResponse>
    {
        private readonly IInventoryStockCalculator _inventoryStockCalculator;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IInventoryMovementCommandRepository _inventoryMovementRepository;

        public CreateInventoryMovementCommandHandler( 
            IProductQueryRepository productQueryRepository,
            IInventoryMovementCommandRepository inventoryMovementRepository,
            IInventoryStockCalculator inventoryStockCalculator ) 
        {
            _productQueryRepository = productQueryRepository;
            _inventoryMovementRepository = inventoryMovementRepository;
            _inventoryStockCalculator = inventoryStockCalculator;
        }

        public async Task<CreatedResponse> Handle( CreateInventoryMovementCommand request,
            CancellationToken cancellationToken)

        {
            var product = await _productQueryRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
                throw new KeyNotFoundException("Producto no encontrado.");

            var newStock = _inventoryStockCalculator.Calculate(product.Stock,request.MovementType, request.Quantity);
            var inventoryMovement = new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                MovementType = request.MovementType,
                Quantity = request.Quantity
            };
            var movementId = await _inventoryMovementRepository.ProcessInventoryMovementAsync(inventoryMovement,newStock,
                    cancellationToken);

            return new CreatedResponse( movementId,"Movimiento de Inventario Creado satisfactoriamente.");
        }
    }
}
