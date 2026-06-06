using Moq;
using Xunit;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Common.Interfaces.Products.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Products.Command.CreateProduct;

namespace InventoryManagement.UnitTests.Aplication.Features.Products.Command.Create
{
    public class CreateProductCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_Should_Throw_NotFoundException_When_Category_Does_Not_Exist()
        {

            var repositoryMock = new Mock<IProductCommandRepository>();
            var categoryMock = new Mock<ICategoryQueryRepository>();


            var categoryId = Guid.NewGuid();

            categoryMock
                .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync((Category?)null);

            var handler = new CreateProductCommandHandler(
                repositoryMock.Object,
                categoryMock.Object
            );

            var command = new CreateProductCommand
            (
                "Producto Test",
                100,
                categoryId
            );

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            repositoryMock.Verify(x =>
                x.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}