using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using MediatR;


namespace InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct
{
    public sealed class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, CreatedResponse>
    {
        private readonly IProductCommandRepository _repository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        public CreateProductCommandHandler(IProductCommandRepository repository , ICategoryQueryRepository categoryQueryRepository
            ,IProductQueryRepository productCommandRepository)
        {
            _repository = repository;
            _categoryQueryRepository = categoryQueryRepository;
            _productQueryRepository = productCommandRepository;
        }
        public async Task<CreatedResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var category = await _categoryQueryRepository.GetByIdAsync(request.CategoryId,cancellationToken);

            if (category is null)
            {
                throw new NotFoundException("La categoria no existe.");
            }

            var existsProductName = await _productQueryRepository.ExistsByProductNameAsync(request.Name, cancellationToken);

            if (existsProductName)
            {
                throw new ConflictException("Ya existe un producto con ese nombre.");
            }

            var product = new InventoryManagement.Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Stock = 0,
                CategoryId = request.CategoryId,
                IsActive = true
            };

            var id = await _repository.CreateAsync(product, cancellationToken);

            return new CreatedResponse(id, "Producto creado Exitosamente.");
        }
    }
}
