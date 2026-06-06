using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Features.Products.Command.DeleteProduct;
using Moq;
using Xunit;


namespace InventoryManagement.UnitTests.Aplication.Features.Products.Command.Delete
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