using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Products.Queries;
using InventoryManagement.Application.Features.Categories.Command.DeleteCategory;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Command.Delete;
    public class DeleteCategoryCommandHandlerNotFoundTests
    {
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();
            var productRepoMock = new Mock<IProductQueryRepository>();

            productRepoMock
                .Setup(x => x.ExistsByCategoryIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            repositoryMock
                .Setup(x => x.DeleteAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new DeleteCategoryCommandHandler(
                repositoryMock.Object,
                productRepoMock.Object);

            var command = new DeleteCategoryCommand(Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None));

            Assert.IsType<NotFoundException>(exception);

            productRepoMock.Verify(
                x => x.ExistsByCategoryIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            repositoryMock.Verify(
                x => x.DeleteAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }