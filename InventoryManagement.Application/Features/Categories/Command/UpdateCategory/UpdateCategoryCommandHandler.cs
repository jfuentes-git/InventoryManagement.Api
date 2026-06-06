using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.Features.Categories.Command.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult>
    {

        private readonly ICategoryCommandRepository _repository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public UpdateCategoryCommandHandler(ICategoryCommandRepository repository , ICategoryQueryRepository categoryQueryRepository)
        {
            _repository = repository;
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<OperationResult> Handle(UpdateCategoryCommand request,CancellationToken cancellationToken)
        {

            var category = await _categoryQueryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category is null)
                throw new NotFoundException("La categoria no existe o se encuentra inactiva");

            var categoryUpdate = new Category {Id = request.Id , Name = request.Name};

            var existsCategoryName = await _categoryQueryRepository.ExistsByCategoryNameAsync(categoryUpdate, cancellationToken);

            if (existsCategoryName)
                throw new ConflictException("Ya existe una categoria con ese nombre.");

            var updated = await _repository.UpdateAsync(categoryUpdate, cancellationToken);

            return updated ? new OperationResult(true, "Categoria actualizada correctamente."):
                       throw new NotFoundException("Categoria no existente.");
        }
    }
}
