using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Command.UpdateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;


namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Command.Update;

public class UpdateCategoryCommandHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCategoryIsUpdated()
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
                .ReturnsAsync(true);

            var handler = new UpdateCategoryCommandHandler(
                repositoryMock.Object,
                repositoryQueryMock.Object);

            var command = new UpdateCategoryCommand(
                categoryId,
                "Electronics");

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);

            repositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }