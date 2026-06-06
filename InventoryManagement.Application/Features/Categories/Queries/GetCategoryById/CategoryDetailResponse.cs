

namespace InventoryManagement.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record CategoryDetailResponse
    (
        Guid Id,
        string Name,
        bool IsActive,
        DateTime CreatedAt
    );
}
