using Moq;
using Xunit;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct;
using InventoryManagement.Application.Common.Exceptions;
using Common.Interfaces.Products.Command;
using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;

namespace InventoryManagement.UnitTests.Products.Command.Create
{
    public class CreateProductCommandHandlerBusinessExceptionTests
    {
        [Fact]
        public async Task Handle_Should_Throw_ConflictException_When_Product_Name_Already_Exists()
        {
            // Arrange
            var repositoryMock = new Mock<IProductCommandRepository>();
            var categoryMock = new Mock<ICategoryQueryRepository>();
            var productQueryMock = new Mock<IProductQueryRepository>();

            var categoryId = Guid.NewGuid();
            categoryMock
                .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Category
                {
                    Id = categoryId,
                    Name = "Category Test"
                });

            productQueryMock
                .Setup(x => x.ExistsByProductNameAsync(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(
                repositoryMock.Object,
                categoryMock.Object,
                productQueryMock.Object
            );

            var command = new CreateProductCommand
            (
                "Duplicate",
                100,
                categoryId
            );


            await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));

            repositoryMock.Verify(x =>
                x.CreateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}