using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Command.Update
{
    public class UpdateProductCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
            var repositoryMock = new Mock<IProductCommandRepository>();
            var repositoryQueryMockP = new Mock<IProductQueryRepository>();
            var repositoryQueryMockC = new Mock<ICategoryQueryRepository>();

            var productId = Guid.NewGuid();

            repositoryQueryMockP
                .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

            var handler = new UpdateProductCommandHandler(
                repositoryMock.Object,
                repositoryQueryMockP.Object,
                repositoryQueryMockC.Object);

            var command = new UpdateProductCommand(
                productId,
                "Test",
                100,
                Guid.NewGuid()
            );

            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));

            repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}