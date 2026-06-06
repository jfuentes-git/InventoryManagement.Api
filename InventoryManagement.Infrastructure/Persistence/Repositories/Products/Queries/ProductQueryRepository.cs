
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.Products.Queries;

public sealed class ProductQueryRepository : IProductQueryRepository
{
    private readonly InventoryDbContext _context;

    public ProductQueryRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().Where(x=>x.IsActive).ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
    }

    public async Task<bool> ExistsByCategoryIdAsync(Guid categoryId,CancellationToken cancellationToken)
    {
             return await _context.Products.AsNoTracking().AnyAsync(x => x.CategoryId == categoryId && x.IsActive, cancellationToken);
    }

}
