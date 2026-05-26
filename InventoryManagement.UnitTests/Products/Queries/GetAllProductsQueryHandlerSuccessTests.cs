using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Features.Product.Queries.GetProducts;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_ShouldReturnProducts_WhenProductsExist()
        {
            var repositoryMock = new Mock<IProductQueryRepository>();

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Laptop",
                    Price = 1000,
                    Stock = 5,
                    CategoryId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Price = 50,
                    Stock = 20,
                    CategoryId = Guid.NewGuid()
                }
            };

            repositoryMock
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);

            var query = new GetAllProductsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}