
using FluentAssertions;
using InventoryManagement.Application.Common.Exceptions;
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

public sealed class CreateInventoryMovementCommandHandlerNotFoundTests
{
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
    {

        var command = new CreateInventoryMovementCommand(
            Guid.NewGuid(),
            MovementType.Entry,
            5);

        var productQueryRepository = new Mock<IProductQueryRepository>();

        productQueryRepository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var handler = new CreateInventoryMovementCommandHandler(
            productQueryRepository.Object,
            Mock.Of<IInventoryMovementCommandRepository>(),
            Mock.Of<IUnitOfWork>(),
            Mock.Of<IProductCommandRepository>());


        Func<Task> act = () => handler.Handle(
            command,
            CancellationToken.None);


        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("Producto no encontrado.");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenProductStockUpdateFails()
    {

        var productId = Guid.NewGuid();

        var product = new Product();

        product.ApplyMovement(MovementType.Entry, 10);

        var command = new CreateInventoryMovementCommand(
            productId,
            MovementType.Entry,
            5);

        var productQueryRepository = new Mock<IProductQueryRepository>();
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
            .ReturnsAsync(false);

        unitOfWork
            .Setup(x => x.ExecuteAsync(
                It.IsAny<Func<DbTransaction, Task<Guid>>>(),
                It.IsAny<CancellationToken>()))
            .Returns<Func<DbTransaction, Task<Guid>>, CancellationToken>(
                async (operation, _) => await operation(null!));

        var handler = new CreateInventoryMovementCommandHandler(
            productQueryRepository.Object,
            Mock.Of<IInventoryMovementCommandRepository>(),
            unitOfWork.Object,
            productCommandRepository.Object);


        Func<Task> act = () => handler.Handle(
            command,
            CancellationToken.None);


        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("Producto no encontrado.");
    }
}