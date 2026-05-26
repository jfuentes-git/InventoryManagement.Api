using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Common.Interfaces.Categories.Command
{
    public interface ICategoryCommandRepository
    {
        Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}
