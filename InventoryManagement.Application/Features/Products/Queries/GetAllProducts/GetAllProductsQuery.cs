
using MediatR;

namespace InventoryManagement.Application.Features.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(): IRequest<List<ProductResponse>>;
}
