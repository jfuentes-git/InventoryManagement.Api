
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using MediatR;

namespace InventoryManagement.Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, ProductDetailResponse>
    {
        private readonly IProductQueryRepository _repository;

        public GetProductByIdQueryHandler(IProductQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDetailResponse> Handle( GetProductByIdQuery request,CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync( request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("Producto no encontrado.");
            }

            return new ProductDetailResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CategoryId
            );
        }
    }
}
