using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailResponse>;
}
