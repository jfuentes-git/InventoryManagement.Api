using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.Products.Command.CreateProduct;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Command.Create
{
    public class CreateProductCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_CreatedResponse_When_Valid_Request()
        {
          

            var categoryMock = new Mock<ICategoryQueryRepository>();
            var repositoryMock = new Mock<IProductCommandRepository>();

            var categoryId = Guid.NewGuid();

            categoryMock.Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Category());

            repositoryMock.Setup(x => x.CreateAsync( It.IsAny<Product>(),It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateProductCommandHandler(repositoryMock.Object,categoryMock.Object);

            var command = new CreateProductCommand
            (
                "Producto 1",
                100,
                categoryId
            );


            var result = await handler.Handle(command, CancellationToken.None);
       
            Assert.NotNull(result);
            Assert.IsType<CreatedResponse>(result);

            repositoryMock.Verify(x =>x.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),Times.Once());
        }
    }
}