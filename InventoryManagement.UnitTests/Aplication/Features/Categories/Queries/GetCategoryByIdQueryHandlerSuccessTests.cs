using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Queries.GetCategoryById;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Queries;

public class GetCategoryByIdQueryHandlerSuccessTests
{
    [Fact]
    public async Task Handle_ShouldReturnCategory_WhenCategoryExists()
    {

        var categoryId = Guid.NewGuid();

        var category = new Category
        {
            Id = categoryId,
            Name = "Electronics",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var repositoryMock = new Mock<ICategoryQueryRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var handler = new GetCategoryByIdQueryHandler(repositoryMock.Object);

        var query = new GetCategoryByIdQuery(categoryId);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        Assert.Equal(category.Name, result.Name);
        Assert.Equal(category.IsActive, result.IsActive);
    }
}