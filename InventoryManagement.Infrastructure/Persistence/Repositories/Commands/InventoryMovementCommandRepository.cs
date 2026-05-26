using Dapper;
using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands
{
    public sealed class InventoryMovementCommandRepository : IInventoryMovementCommandRepository
    {
        private readonly IDbConnection _connection;

        public InventoryMovementCommandRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> ProcessInventoryMovementAsync(InventoryMovement inventoryMovement, int newStock,
    CancellationToken cancellationToken)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await ((dynamic)_connection).OpenAsync(cancellationToken);
            }
            using var transaction = _connection.BeginTransaction();
            try
            {
                const string updateStockSql = """
                UPDATE Products
                SET Stock = @Stock
                WHERE Id = @ProductId
                """;
                var updateCommand = new CommandDefinition(
                    updateStockSql,
                    new
                    {
                        ProductId = inventoryMovement.ProductId,
                        Stock = newStock
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);

                await _connection.ExecuteAsync(updateCommand);

                const string insertMovementSql = """
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

                var insertCommand = new CommandDefinition(
                    insertMovementSql,
                    inventoryMovement,
                    transaction: transaction,
                    cancellationToken: cancellationToken);

                await _connection.ExecuteAsync(insertCommand);
                transaction.Commit();

                return inventoryMovement.Id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}


