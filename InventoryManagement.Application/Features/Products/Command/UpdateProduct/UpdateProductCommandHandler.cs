using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using MediatR;

namespace InventoryManagement.Application.Features.Products.Command.UpdateProduct
{
    public sealed class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, OperationResult>
    {
        private readonly IProductCommandRepository _repository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public UpdateProductCommandHandler(
            IProductCommandRepository repository,
            IProductQueryRepository productQueryRepository,
            ICategoryQueryRepository categoryQueryRepository)
        {
            _repository = repository;
            _productQueryRepository = productQueryRepository;
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<OperationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _productQueryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                throw new NotFoundException("El producto no existe o se encuentra inactivo.");

            var category = await _categoryQueryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if (category is null)
                throw new NotFoundException("La categoria no existe.");

            var existsProductName = await _productQueryRepository.ExistsByProductNameAsync(request.Name, cancellationToken);

            if (existsProductName)
                throw new ConflictException("Ya existe un producto con ese nombre.");

            var productUpdate = new InventoryManagement.Domain.Entities.Product
            {
                Id = request.Id,Name = request.Name, Price = request.Price, CategoryId = request.CategoryId,
            };

            var updated = await _repository.UpdateAsync(productUpdate,cancellationToken);

            return updated? new OperationResult(true, "Product updated successfully")
: throw new NotFoundException("Product not found");
        }
    }
}
