using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using InventoryManagement.Application.Features.Categories.Commands.DeleteCategory;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using Common.Interfaces.Products.Query;

namespace InventoryManagement.UnitTests.Categories.Command.Delete
{
    public class DeleteCategoryCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCategoryIsDeleted()
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
                .ReturnsAsync(true);

            var handler = new DeleteCategoryCommandHandler(
                repositoryMock.Object,
                productRepoMock.Object);

            var command = new DeleteCategoryCommand(Guid.NewGuid());

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

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
}