using Common.Interfaces.Products.Query;
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
    public sealed class ProductQueryRepository : IProductQueryRepository
    {
        private readonly InventoryDbContext _context;

        public ProductQueryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        }

        public async Task<bool> ExistsByCategoryIdAsync(Guid categoryId,CancellationToken cancellationToken)
        {
                 return await _context.Products.AsNoTracking().AnyAsync(x => x.CategoryId == categoryId, cancellationToken);
        }

        public async Task<bool> ExistsByProductNameAsync(CreateProductCommand Product, CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().AnyAsync(x => x.Name.Equals(Product.Name), cancellationToken);
        }

    }
}
