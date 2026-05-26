using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.DeleteProduct;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Common.Exceptions;
using Common.Interfaces.Products.Command;

namespace InventoryManagement.UnitTests.Products.Command.Delete
{
    public class DeleteProductCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_Success_When_Product_Is_Deleted()
        {
            var repositoryMock = new Mock<IProductCommandRepository>();

            var productId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.DeleteAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteProductCommandHandler(repositoryMock.Object);

            var command = new DeleteProductCommand
            (
                 productId
            );

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            repositoryMock.Verify(x =>
                x.DeleteAsync(productId, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}