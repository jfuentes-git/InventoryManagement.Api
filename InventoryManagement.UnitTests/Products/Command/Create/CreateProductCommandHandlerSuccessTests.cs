using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct;
using InventoryManagement.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.UnitTests.Products.Command.Create
{
    public class CreateProductCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_CreatedResponse_When_Valid_Request()
        {
          
            var repositoryMock = new Mock<IProductCommandRepository>();
            var categoryMock = new Mock<ICategoryQueryRepository>();
            var productQueryMock = new Mock<IProductQueryRepository>();

            var categoryId = Guid.NewGuid();

            categoryMock
                .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category());

            productQueryMock
                .Setup(x => x.ExistsByProductNameAsync(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            repositoryMock
                .Setup(x => x.CreateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateProductCommandHandler(
                repositoryMock.Object,
                categoryMock.Object,
                productQueryMock.Object
            );

            var command = new CreateProductCommand
            (
                "Producto 1",
                100,
                categoryId
            );


            var result = await handler.Handle(command, CancellationToken.None);
       
            Assert.NotNull(result);
            Assert.IsType<CreatedResponse>(result);

            repositoryMock.Verify(x =>
                x.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}