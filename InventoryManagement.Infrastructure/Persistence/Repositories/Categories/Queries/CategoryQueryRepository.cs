
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.Categories.Queries
{
    public sealed class CategoryQueryRepository : ICategoryQueryRepository
    {

        private readonly InventoryDbContext _context;

        public CategoryQueryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                         .AsNoTracking().Where(c => c.IsActive).OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsActive , cancellationToken);
        }

        public async Task<bool> ExistsByCategoryNameAsync(Category category, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(x => x.Name.Equals(category.Name) && x.Id != category.Id , cancellationToken);
        }

    }

}
