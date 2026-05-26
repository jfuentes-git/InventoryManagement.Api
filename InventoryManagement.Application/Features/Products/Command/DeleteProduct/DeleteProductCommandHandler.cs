using Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.FeaturesCatalog.Command.DeleteProduct
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
