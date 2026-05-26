using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Application.Features.InventoryMovements.Command;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using System;
using Moq;
using Xunit;


namespace InventoryManagement.UnitTests.InvetoryMovements.Command.Create
{
    public class CreateInventoryMovementCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_Product_Does_Not_Exist()
        {
            var productMock = new Mock<IProductQueryRepository>();
            var movementRepoMock = new Mock<IInventoryMovementCommandRepository>();
            var stockCalculatorMock = new Mock<IInventoryStockCalculator>();

            var productId = Guid.NewGuid();

            productMock
                .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            var handler = new CreateInventoryMovementCommandHandler(
                productMock.Object,
                movementRepoMock.Object,
                stockCalculatorMock.Object
            );

            var command = new CreateInventoryMovementCommand(
                productId,
                MovementType.Entry,
                5
            );

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));

            movementRepoMock.Verify(x =>
                x.ProcessInventoryMovementAsync(
                    It.IsAny<InventoryMovement>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}