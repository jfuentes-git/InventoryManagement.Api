using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Categories.Command.DeleteCategory;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Command.Delete;

    public class DeleteCategoryCommandHandlerUseCaseExceptionTests
    {
        [Fact]
        public async Task Handle_ShouldThrowUseCaseException_WhenCategoryHasProducts()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();
            var productRepoMock = new Mock<IProductQueryRepository>();

            productRepoMock
                .Setup(x => x.ExistsByCategoryIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteCategoryCommandHandler(
                repositoryMock.Object,
                productRepoMock.Object);

            var command = new DeleteCategoryCommand(Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<UseCaseException>(
                () => handler.Handle(command, CancellationToken.None));

            Assert.IsType<UseCaseException>(exception);

            productRepoMock.Verify(
                x => x.ExistsByCategoryIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            repositoryMock.Verify(
                x => x.DeleteAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
