
using Moq;
using Xunit;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Features.Products.Command.DeleteProduct;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Command.Delete
{
    public class DeleteProductCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
     
            var repositoryMock = new Mock<IProductCommandRepository>();

            var productId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.DeleteAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new DeleteProductCommandHandler(repositoryMock.Object);

            var command = new DeleteProductCommand
            (
                 productId
            );

          
            await Assert.ThrowsAsync<NotFoundException>(() =>handler.Handle(command, CancellationToken.None));
            repositoryMock.Verify(x => x.DeleteAsync(productId, It.IsAny<CancellationToken>()),Times.Once);

        }
    }
}