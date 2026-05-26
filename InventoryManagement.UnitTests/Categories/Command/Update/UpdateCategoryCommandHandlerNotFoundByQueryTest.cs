using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Commands.UpdateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Categories.Command.Update
{
    public class UpdateCategoryCommandHandlerNotFoundByQueryTest
    {
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();
            var repositoryQueryMock = new Mock<ICategoryQueryRepository>();

            var categoryId = Guid.NewGuid();

            repositoryQueryMock.Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category?)null);

            var handler = new UpdateCategoryCommandHandler(
                repositoryMock.Object,
                repositoryQueryMock.Object);

            var command = new UpdateCategoryCommand(
                categoryId,
                "Electronics");

            await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None));

            repositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}