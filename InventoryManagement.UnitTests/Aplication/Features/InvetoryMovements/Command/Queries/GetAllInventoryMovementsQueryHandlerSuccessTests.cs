using Moq;
using Xunit;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetAllInventoryMovements;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;


namespace InventoryManagement.UnitTests.Aplication.Features.InvetoryMovements.Command.Queries
{
    public class GetAllInventoryMovementsQueryHandlerSuccessTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Inventory_Movements()
        {
            var repositoryMock = new Mock<IInventoryMovementQueryRepository>();

            repositoryMock
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<InventoryMovement>
                {
                    new InventoryMovement
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        MovementType = MovementType.Entry,
                        Quantity = 5,
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var handler = new GetAllInventoryMovementsQueryHandler(repositoryMock.Object);

            var query = new GetAllInventoryMovementsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Entry", result.First().MovementType);
        }
    }
}