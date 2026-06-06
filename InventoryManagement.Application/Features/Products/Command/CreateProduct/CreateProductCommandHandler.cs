
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;


namespace InventoryManagement.Application.Features.Products.Command.CreateProduct
{
    public sealed class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, CreatedResponse>
    {
        private readonly IProductCommandRepository _repository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        public CreateProductCommandHandler(IProductCommandRepository repository , ICategoryQueryRepository categoryQueryRepository)
        {
            _repository = repository;
            _categoryQueryRepository = categoryQueryRepository;
        }
        public async Task<CreatedResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var category = await _categoryQueryRepository.GetByIdAsync(request.CategoryId,cancellationToken);

            if (category is null)
            {
                throw new NotFoundException("La categoria no existe o se encuentra inactiva.");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                IsActive = true
            };

            var id = await _repository.CreateAsync(product, cancellationToken);

            return new CreatedResponse(id, "Producto creado Exitosamente.");
        }
    }
}
