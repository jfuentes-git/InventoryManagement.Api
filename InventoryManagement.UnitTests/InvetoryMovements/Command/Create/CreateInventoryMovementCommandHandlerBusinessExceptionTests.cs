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
    public class CreateInventoryMovementCommandHandlerBusinessExceptionTests
    {
        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_Business_Rule_Fails()
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
        }
    }
}