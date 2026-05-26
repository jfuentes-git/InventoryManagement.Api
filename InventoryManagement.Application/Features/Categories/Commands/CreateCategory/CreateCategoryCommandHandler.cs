
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;


namespace InventoryManagement.Application.Features.Categories.Commands.CreateCategory
{

    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedResponse>
    {
        private readonly ICategoryCommandRepository _repository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public CreateCategoryCommandHandler(ICategoryCommandRepository repository, ICategoryQueryRepository categoryQueryRepository)
        {
            _repository = repository;
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<CreatedResponse> Handle(CreateCategoryCommand request,CancellationToken cancellationToken)
        {

            var existsCategoryName = await _categoryQueryRepository.ExistsByCategoryNameAsync(request.Name, cancellationToken);

            if (existsCategoryName)
                throw new ConflictException("Ya existe una categoria con ese nombre.");

            var category = new Category { Id = Guid.NewGuid(), Name = request.Name,IsActive = true};

            var id = await _repository.CreateAsync(category,cancellationToken);

            return new CreatedResponse(category.Id, "La categoria fue creada exitosamente");
        }
    }

}