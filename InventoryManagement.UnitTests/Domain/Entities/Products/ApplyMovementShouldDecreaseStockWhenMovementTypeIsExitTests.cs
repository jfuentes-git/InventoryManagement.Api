using FluentAssertions;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Entities;

using Xunit;

namespace InventoryManagement.UnitTests.Domain.Entities.Products;

public sealed class ApplyMovementShouldDecreaseStockWhenMovementTypeIsExitTests
{
    [Fact]
    public void Execute()
    {

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);
        product.ApplyMovement(MovementType.Exit, 3);

        product.Stock.Should().Be(7);
    }
}