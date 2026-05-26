using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Queries.GetProducts
{
    public sealed record ProductResponse
    (
        Guid Id,
        string Name,
        decimal Price,
        int Stock,
        Guid CategoryId
    );
}
