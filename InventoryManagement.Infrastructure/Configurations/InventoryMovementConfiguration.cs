

using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Configurations
{
    public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.ToTable("InventoryMovements");

            builder.Property(x => x.ProductId)
                   .IsRequired();
            builder.Property(x => x.Quantity)
                  .IsRequired();
            builder.Property(x => x.MovementType)
                  .IsRequired();

        }

    }
}
