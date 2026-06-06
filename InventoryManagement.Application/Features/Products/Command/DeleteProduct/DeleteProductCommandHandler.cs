
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.Products.Command.DeleteProduct
{
    public sealed class DeleteProductCommandHandler
      : IRequestHandler<DeleteProductCommand, OperationResult>
    {
        private readonly IProductCommandRepository _repository;

        public DeleteProductCommandHandler(IProductCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeleteProductCommand request,CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id, cancellationToken);

            return deleted ? 
                        new OperationResult(true,"Producto eliminado exitosamente")
                : throw new NotFoundException("Producto no encontrado.");

        }
    }
}
