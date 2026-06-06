using MediatR;

namespace InventoryManagement.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(Guid Id): IRequest<CategoryDetailResponse>;
}
