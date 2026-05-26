using Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Command.Update
{
    public class UpdateProductCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_Success_When_Product_Is_Updated()
        {
            var repositoryMock = new Mock<IProductCommandRepository>();

            var productId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Domain.Entities.Product>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new UpdateProductCommandHandler(repositoryMock.Object);

            var command = new UpdateProductCommand
            (
                productId,
                "Updated Product",
                200,
                Guid.NewGuid(),
                true
            );

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            repositoryMock.Verify(x =>
                x.UpdateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}