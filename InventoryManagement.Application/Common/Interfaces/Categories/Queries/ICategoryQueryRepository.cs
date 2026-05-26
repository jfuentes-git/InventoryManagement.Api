using InventoryManagement.Application.Features.Categories.Commands.CreateCategory;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Common.Interfaces.Categories.Queries
{
    public interface ICategoryQueryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByCategoryNameAsync(CreateCategoryCommand Category, CancellationToken cancellationToken);
    }
}
