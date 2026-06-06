

namespace InventoryManagement.Application.Features.Products.Queries.GetAllProducts
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
