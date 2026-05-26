using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Commands.CreateCategory;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.Queries
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
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Categories .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByCategoryNameAsync(CreateCategoryCommand Category, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(x => x.Name.Equals(Category.Name), cancellationToken);
        }

    }

}
