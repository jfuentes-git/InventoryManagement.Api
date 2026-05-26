using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Features.Product.Queries.GetProductById;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {

            var repositoryMock = new Mock<IProductQueryRepository>();

            repositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            var handler = new GetProductByIdQueryHandler(repositoryMock.Object);

            var query = new GetProductByIdQuery(Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(query, CancellationToken.None));
        }
    }
}