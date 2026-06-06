using FluentAssertions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Xunit;

namespace InventoryManagement.UnitTests.Domain.Entities.Products;

public sealed class ApplyMovementShouldDecreaseStockWhenAdjustmentQuantityIsNegativeTests
{
    [Fact]
    public void Execute()
    {

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);
        product.ApplyMovement(MovementType.Adjustment, -3);

        product.Stock.Should().Be(7);
    }
}