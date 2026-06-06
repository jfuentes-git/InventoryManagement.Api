

namespace InventoryManagement.Application.Features.Products.Queries.GetProductById
{
    /// <summary>
    /// Product detail response.
    /// </summary>
    /// <param name="Id">Unique product identifier.</param>
    /// <param name="Name">Product name.</param>
    /// <param name="Price">Current product price.</param>
    /// <param name="Stock">Available stock.</param>
    public sealed record ProductDetailResponse
  (
      Guid Id,
      string Name,
      decimal Price,
      int Stock,
      Guid CategoryId
  );
}
