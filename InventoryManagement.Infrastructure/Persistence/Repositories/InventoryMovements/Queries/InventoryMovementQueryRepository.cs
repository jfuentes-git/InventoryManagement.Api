
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.InventoryMovements.Queries
{
    public sealed class InventoryMovementQueryRepository : IInventoryMovementQueryRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryMovementQueryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryMovement>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.InventoryMovements.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InventoryMovement>> GetByProductIdAsync(Guid productId,CancellationToken cancellationToken)
        {
            return await _context.InventoryMovements
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
