using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence.Repositories.Queries;
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

            repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}