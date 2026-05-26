using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using InventoryManagement.Application.Features.InventoryMovements.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.UnitTests.InvetoryMovements.Command.Create
{
    public class CreateInventoryMovementCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Create_Inventory_Movement_Successfully()
        {
            var productMock = new Mock<IProductQueryRepository>();
            var movementRepoMock = new Mock<IInventoryMovementCommandRepository>();
            var stockCalculatorMock = new Mock<IInventoryStockCalculator>();

            var productId = Guid.NewGuid();

            productMock
                .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Product
                {
                    Id = productId,
                    Stock = 10
                });

            stockCalculatorMock
                .Setup(x => x.Calculate(10, MovementType.Entry, 5))
                .Returns(15);

            movementRepoMock
                .Setup(x => x.ProcessInventoryMovementAsync(
                    It.IsAny<InventoryMovement>(),
                    15,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

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

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);

            movementRepoMock.Verify(x =>
                x.ProcessInventoryMovementAsync(It.IsAny<InventoryMovement>(), 15, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}