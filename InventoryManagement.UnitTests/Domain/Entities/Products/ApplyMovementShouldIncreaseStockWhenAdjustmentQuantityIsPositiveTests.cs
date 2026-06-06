using FluentAssertions;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Entities;
using Xunit;

namespace InventoryManagement.UnitTests.Domain.Entities.Products;
public sealed class ApplyMovementShouldIncreaseStockWhenAdjustmentQuantityIsPositiveTests
{
    [Fact]
    public void Execute()
    {

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);
        product.ApplyMovement(MovementType.Adjustment, 5);

        product.Stock.Should().Be(15);
    }
}