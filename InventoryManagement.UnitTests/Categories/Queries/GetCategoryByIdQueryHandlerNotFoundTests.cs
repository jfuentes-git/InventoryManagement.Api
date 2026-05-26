using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Queries.GetCategoryById;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.Application.UnitTests.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandlerNotFoundTests
{
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
    {
        var categoryId = Guid.NewGuid();

        var repositoryMock = new Mock<ICategoryQueryRepository>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        var handler = new GetCategoryByIdQueryHandler(repositoryMock.Object);

        var query = new GetCategoryByIdQuery(categoryId);


        var exception =
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));

        Assert.IsType<NotFoundException>(exception);
    }
}