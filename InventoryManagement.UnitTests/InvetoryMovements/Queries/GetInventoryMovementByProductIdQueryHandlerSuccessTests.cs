using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Queries;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.UnitTests.InvetoryMovements.Queries
{
    public class GetInventoryMovementByProductIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Movements_When_Exist()
        {
            var repositoryMock = new Mock<IInventoryMovementQueryRepository>();

            var productId = Guid.NewGuid();

            repositoryMock
                .Setup(x => x.GetByProductIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<InventoryMovement>
                {
                    new InventoryMovement
                    {
                        Id = Guid.NewGuid(),
                        ProductId = productId,
                        MovementType = MovementType.Entry,
                        Quantity = 10,
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var handler = new GetInventoryMovementByProductIdQueryHandler(repositoryMock.Object);

            var query = new GetInventoryMovementByProductIdQuery
            (
                productId
            );

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(productId, result.First().ProductId);
        }
    }
}