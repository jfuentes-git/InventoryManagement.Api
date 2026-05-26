using Common.Interfaces.Products.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Queries.GetProducts
{

    public sealed class GetAllProductsQueryHandler: IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IProductQueryRepository _repository;

        public GetAllProductsQueryHandler(IProductQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponse>> Handle( GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);

            return products
                .Select(x => new ProductResponse(
                    x.Id,
                    x.Name,
                    x.Price,
                    x.Stock,
                    x.CategoryId
                )).ToList();
        }
    }
}
