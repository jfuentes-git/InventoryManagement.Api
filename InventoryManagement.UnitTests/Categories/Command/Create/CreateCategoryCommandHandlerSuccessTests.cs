using FluentAssertions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Commands.CreateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

public class CreateCategoryCommandHandlerSuccessTests
{
    [Fact]
    public async Task Should_CreateCategory_When_NameIsUnique()
    {
        var commandRepo = new Mock<ICategoryCommandRepository>();
        var queryRepo = new Mock<ICategoryQueryRepository>();

        queryRepo
            .Setup(x => x.ExistsByCategoryNameAsync(
                It.IsAny<CreateCategoryCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        commandRepo
            .Setup(x => x.CreateAsync(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.NewGuid());

        var handler = new CreateCategoryCommandHandler(
            commandRepo.Object,
            queryRepo.Object);

        var command = new CreateCategoryCommand("Electronics");

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();

        commandRepo.Verify(x => x.CreateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}