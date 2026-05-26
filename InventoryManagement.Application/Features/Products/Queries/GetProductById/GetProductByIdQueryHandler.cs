using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Queries.GetProductById
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
