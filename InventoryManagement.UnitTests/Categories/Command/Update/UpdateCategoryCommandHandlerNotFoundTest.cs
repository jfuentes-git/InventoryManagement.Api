using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Features.Categories.Commands.UpdateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Categories.Command.Update
{
    public class UpdateCategoryCommandHandlerNotFoundTest
    {
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUpdateFails()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new UpdateCategoryCommandHandler(
                repositoryMock.Object);

            var command = new UpdateCategoryCommand(
                Guid.NewGuid(),
                "Electronics",
                true);

            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None));

            Assert.IsType<NotFoundException>(exception);

            repositoryMock.Verify(
                x => x.UpdateAsync(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
