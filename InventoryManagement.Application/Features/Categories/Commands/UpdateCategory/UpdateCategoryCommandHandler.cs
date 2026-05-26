using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult>
    {

        private readonly ICategoryCommandRepository _repository;

        public UpdateCategoryCommandHandler(ICategoryCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(UpdateCategoryCommand request,CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = request.Id,
                Name = request.Name,
                IsActive = request.IsActive
            };

            var updated = await _repository.UpdateAsync( category,cancellationToken);

            if (!updated)
            {
                throw new NotFoundException("Categoria no encontrada para efectura su actualización.");
            }

            return new OperationResult( true,"Categoria actualizada satisfactoriamente.");
        }
    }
}
