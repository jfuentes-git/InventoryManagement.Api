using MediatR;

namespace InventoryManagement.Application.Features.Products.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailResponse>;
}
