
using FluentAssertions;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Common.Interfaces.UnitOfWork;
using InventoryManagement.Application.Features.InventoryMovements.Command.Create;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Moq;
using System.Data.Common;
using Xunit;


namespace InventoryManagement.UnitTests.Aplication.Features.InvetoryMovements.Command.Create;

public sealed class CreateInventoryMovementCommandHandlerSuccessTests
{
    [Fact]
    public async Task Handle_ShouldCreateInventoryMovementSuccessfully()
    {

        var productId = Guid.NewGuid();
        var movementId = Guid.NewGuid();

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);

        var command = new CreateInventoryMovementCommand(
            productId,
            MovementType.Entry,
            5);

        var productQueryRepository = new Mock<IProductQueryRepository>();
        var inventoryMovementRepository = new Mock<IInventoryMovementCommandRepository>();
        var productCommandRepository = new Mock<IProductCommandRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        productQueryRepository
            .Setup(x => x.GetByIdAsync(
                productId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        productCommandRepository
            .Setup(x => x.UpdateStockAsync(
                productId,
                It.IsAny<int>(),
                It.IsAny<DbTransaction>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        inventoryMovementRepository
            .Setup(x => x.InsertMovementAsync(
                It.IsAny<InventoryMovement>(),
                It.IsAny<DbTransaction>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(movementId);

        unitOfWork
            .Setup(x => x.ExecuteAsync(
                It.IsAny<Func<DbTransaction, Task<Guid>>>(),
                It.IsAny<CancellationToken>()))
            .Returns<Func<DbTransaction, Task<Guid>>, CancellationToken>(
                async (operation, _) => await operation(null!));

        var handler = new CreateInventoryMovementCommandHandler(
            productQueryRepository.Object,
            inventoryMovementRepository.Object,
            unitOfWork.Object,
            productCommandRepository.Object);


        var result = await handler.Handle(
            command,
            CancellationToken.None);


        result.Should().NotBeNull();
        result.Id.Should().Be(movementId);

        productCommandRepository.Verify(
            x => x.UpdateStockAsync(
                productId,
                It.IsAny<int>(),
                It.IsAny<DbTransaction>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        inventoryMovementRepository.Verify(
            x => x.InsertMovementAsync(
                It.IsAny<InventoryMovement>(),
                It.IsAny<DbTransaction>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}