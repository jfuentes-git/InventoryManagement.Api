using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Products.Queries.GetProductById;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Queries
{
    public class GetProductByIdQueryHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductExists()
        {

            var repositoryMock = new Mock<IProductQueryRepository>();

            var productId = Guid.NewGuid();

            var product = new Product
            {
                Id = productId,
                Name = "Laptop",
                Price = 1200,
                CategoryId = Guid.NewGuid()
            };

            repositoryMock
                .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var handler = new GetProductByIdQueryHandler(repositoryMock.Object);

            var query = new GetProductByIdQuery(productId);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(product.Name, result.Name);
        }
    }
}