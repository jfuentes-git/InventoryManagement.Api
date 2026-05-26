using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Features.Categories.Commands.UpdateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Categories.Command.Update
{
    public class UpdateCategoryCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCategoryIsUpdated()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new UpdateCategoryCommandHandler(repositoryMock.Object);

            var command = new UpdateCategoryCommand(
                Guid.NewGuid(),
                "Electronics",
                true);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            repositoryMock.Verify(
                x => x.UpdateAsync(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}