using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Products.Queries.GetProductById;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Queries
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