using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using InventoryManagement.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Command.Update
{
    public class UpdateProductCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_Success_When_Product_Is_Updated()
        {

            var repositoryMock = new Mock<IProductCommandRepository>();
            var repositoryQueryMockP = new Mock<IProductQueryRepository>();
            var repositoryQueryMockC = new Mock<ICategoryQueryRepository>();

            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            repositoryQueryMockP
                .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Product
                {
                    Id = productId,
                    Name = "Old Name",
                    Price = 100,
                    CategoryId = categoryId
                });

            repositoryQueryMockC
                .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = categoryId,
                    Name = "Category Test"
                });

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new UpdateProductCommandHandler(
                repositoryMock.Object,
                repositoryQueryMockP.Object,
                repositoryQueryMockC.Object);

            var command = new UpdateProductCommand(
                productId,
                "Updated Product",
                200,
                categoryId
            );

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            repositoryMock.Verify(x =>
                x.UpdateAsync(
                    It.Is<Product>(p =>
                        p.Id == productId &&
                        p.Name == "Updated Product" &&
                        p.Price == 200 &&
                        p.CategoryId == categoryId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}