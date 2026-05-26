using Moq;
using Xunit;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.UnitTests.InvetoryMovements.Queries
{
    public class GetInventoryMovementByProductIdQueryHandlerEmptyTests
    {
        [Fact]
        public async Task Handle_Should_Return_Empty_List_When_No_Movements_Exist()
        {
            var repositoryMock = new Mock<IInventoryMovementQueryRepository>();
            var productId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.GetByProductIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<InventoryMovement>());

            var handler = new GetInventoryMovementByProductIdQueryHandler(repositoryMock.Object);

            var query = new GetInventoryMovementByProductIdQuery
            (
                productId
            );

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}