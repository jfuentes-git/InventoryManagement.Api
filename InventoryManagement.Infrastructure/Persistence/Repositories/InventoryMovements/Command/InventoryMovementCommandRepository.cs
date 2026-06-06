

using Dapper;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Domain.Entities;
using System.Data.Common;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.InventoryMovements.Command
{
    public sealed class InventoryMovementCommandRepository : IInventoryMovementCommandRepository
    {
        private readonly DbConnection _connection;
        public InventoryMovementCommandRepository( DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> InsertMovementAsync(InventoryMovement inventoryMovement, DbTransaction transaction,
            CancellationToken cancellationToken)
        {
            const string sql = """
                               INSERT INTO InventoryMovements
                               (
                                Id,
                                ProductId,
                                MovementType,
                                Quantity,
                                CreatedAt
                               )
                                 VALUES
                               (
                               @Id,
                               @ProductId,
                               @MovementType,
                               @Quantity,
                               @CreatedAt
                               )
                              """;

            var command = new CommandDefinition(sql,inventoryMovement, transaction: transaction,cancellationToken: cancellationToken);
            await _connection.ExecuteAsync(command);

            return inventoryMovement.Id;
        }

    }
}


