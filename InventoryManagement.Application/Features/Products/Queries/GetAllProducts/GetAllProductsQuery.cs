using InventoryManagement.Application.Features.Product.Queries.GetProductById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Queries.GetProducts
{
    public sealed record GetAllProductsQuery(): IRequest<List<ProductResponse>>;
}
