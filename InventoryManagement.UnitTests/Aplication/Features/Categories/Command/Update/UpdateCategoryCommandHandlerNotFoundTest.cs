using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Command.UpdateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;


namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Command.Update;

public class UpdateCategoryCommandHandlerNotFoundTest
    {
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUpdateFails()
        {
            var repositoryMock = new Mock<ICategoryCommandRepository>();
            var repositoryQueryMock = new Mock<ICategoryQueryRepository>();

            var categoryId = Guid.NewGuid();

            repositoryQueryMock
                .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = categoryId,
                    Name = "Old Name"
                });

            repositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new UpdateCategoryCommandHandler(
                repositoryMock.Object,
                repositoryQueryMock.Object);

            var command = new UpdateCategoryCommand(
                categoryId,
                "Electronics");

            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None));

            Assert.IsType<NotFoundException>(exception);

            repositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }