using Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Products.Command.UpdateProduct
{
    public sealed class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, OperationResult>
    {
        private readonly IProductCommandRepository _repository;

        public UpdateProductCommandHandler(
            IProductCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new InventoryManagement.Domain.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                IsActive = request.IsActive
            };

            var updated = await _repository.UpdateAsync(product,cancellationToken);

            return updated
                ? new OperationResult(true, "Product updated successfully")
                : throw new NotFoundException("Product not found");
        }
    }
}
