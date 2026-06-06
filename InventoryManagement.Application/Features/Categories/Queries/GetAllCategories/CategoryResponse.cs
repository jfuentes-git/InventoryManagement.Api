
namespace InventoryManagement.Application.Features.Categories.Queries.GetAllCategories
{
     public sealed record CategoryResponse(
      Guid Id,
      string Name,
      DateTime CreatedAt
     );
}
