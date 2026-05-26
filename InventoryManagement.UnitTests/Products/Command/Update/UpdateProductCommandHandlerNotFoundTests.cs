using Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Command.Update
{
    public class UpdateProductCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
            var repositoryMock = new Mock<IProductCommandRepository>();

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Domain.Entities.Product>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new UpdateProductCommandHandler(repositoryMock.Object);

            var command = new UpdateProductCommand
            (
                Guid.NewGuid(),
                "Test",
                100,
                Guid.NewGuid(),
                true
            );

            
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            repositoryMock.Verify(x =>
                x.UpdateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}