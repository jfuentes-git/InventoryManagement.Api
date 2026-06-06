
    using Dapper;
    using InventoryManagement.Application.Common.Interfaces.Products.Command;
    using InventoryManagement.Domain.Entities;
    using System.Data.Common;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.Products.Command;

    public sealed class ProductCommandRepository : IProductCommandRepository
    {
        private readonly DbConnection _connection;

        public ProductCommandRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> CreateAsync(Product product,CancellationToken cancellationToken)
        {
            var sql = " INSERT INTO Products(Id,Name,Price,Stock,CategoryId,IsActive,CreatedAt) "
                     +" VALUES( @Id,@Name,@Price,@Stock,@CategoryId,@IsActive,@CreatedAt)";
            var command = new CommandDefinition(
                sql,
                product,
                cancellationToken: cancellationToken);
            await _connection.ExecuteAsync(command);

            return product.Id;
        }

        public async Task<bool> UpdateAsync(Product product,CancellationToken cancellationToken)
        {
            var sql = " UPDATE Products SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id ";

            var command = new CommandDefinition( sql, product, cancellationToken: cancellationToken);
            var rowsAffected = await _connection.ExecuteAsync(command);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var sql = "UPDATE Products SET IsActive = 0 WHERE Id = @Id AND IsActive = 1 ";
            var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
            var rowsAffected = await _connection.ExecuteAsync(command);

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateStockAsync(Guid productId, int newStock, DbTransaction transaction,
           CancellationToken cancellationToken)
        {
            const string sql = """
                                   UPDATE Products
                                   SET Stock = @Stock
                                   WHERE Id = @ProductId
                                   AND IsActive = 1
                                   """;

            var command = new CommandDefinition(sql,
                new { ProductId = productId, Stock = newStock }, transaction: transaction, cancellationToken: cancellationToken);

            var rowsAffected = await _connection.ExecuteAsync(command);
            return rowsAffected > 0;
        }
    }