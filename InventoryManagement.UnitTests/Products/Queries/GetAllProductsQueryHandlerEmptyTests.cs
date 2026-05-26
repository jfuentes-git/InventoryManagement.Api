using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Features.Product.Queries.GetProducts;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandlerEmptyTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            var repositoryMock = new Mock<IProductQueryRepository>();

            repositoryMock
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Product>());

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);

            var query = new GetAllProductsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}