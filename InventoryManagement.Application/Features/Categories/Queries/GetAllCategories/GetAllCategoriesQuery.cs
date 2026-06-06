
using MediatR;

namespace InventoryManagement.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery(): IRequest<List<CategoryResponse>>;

}
