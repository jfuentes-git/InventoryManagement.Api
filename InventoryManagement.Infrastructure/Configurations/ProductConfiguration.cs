using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InventoryManagement.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
        }
    }
}
