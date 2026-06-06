using FluentAssertions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Xunit;

namespace InventoryManagement.UnitTests.Domain.Entities.Products;

public sealed class ApplyMovementShouldThrowInvalidOperationExceptionWhenAdjustmentResultsInNegativeStockTests
{
    [Fact]
    public void Execute()
    {

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);
        Action act = () =>product.ApplyMovement(MovementType.Adjustment, -20);
       
        act.Should().Throw<InvalidOperationException>().WithMessage("El stock no puede ser negativo.");
    }
}