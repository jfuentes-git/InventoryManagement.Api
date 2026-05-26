using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Queries.GetAllCategories;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandlerEmptyTests
{
    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCategoriesExist()
    {
        var repositoryMock = new Mock<ICategoryQueryRepository>();

        repositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Category>());

        var handler = new GetAllCategoriesQueryHandler(repositoryMock.Object);

        var result = await handler.Handle(
            new GetAllCategoriesQuery(),
            CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}