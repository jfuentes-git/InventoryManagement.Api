using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryHandler: IRequestHandler<GetCategoryByIdQuery, CategoryDetailResponse>
    {
        private readonly ICategoryQueryRepository _repository;

        public GetCategoryByIdQueryHandler(ICategoryQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<CategoryDetailResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {

            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category is null)
            {
                throw new NotFoundException("Categoria no encontrada.");
            }

            return new CategoryDetailResponse(
                category.Id,
                category.Name,
                category.IsActive,
                category.CreatedAt
            );
        }
    }
}
