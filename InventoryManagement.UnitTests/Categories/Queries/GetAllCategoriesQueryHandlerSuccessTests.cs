using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Queries.GetAllCategories;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandlerSuccessTests
{
    [Fact]
    public async Task Handle_ShouldReturnCategories_WhenCategoriesExist()
    {
        var categories = new List<Category>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Electronics",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Office",
                CreatedAt = DateTime.UtcNow
            }
        };

        var repositoryMock = new Mock<ICategoryQueryRepository>();

        repositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(categories);

        var handler = new GetAllCategoriesQueryHandler(repositoryMock.Object);

        var result = await handler.Handle(
            new GetAllCategoriesQuery(),
            CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Electronics", result[0].Name);
        Assert.Equal("Office", result[1].Name);
    }
}