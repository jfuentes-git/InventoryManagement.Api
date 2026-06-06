using Dapper;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Domain.Entities;
using System.Data.Common;

namespace InventoryManagement.Infrastructure.Persistence.Repositories.Categories.Command
{
    public class CategoryCommandRepository : ICategoryCommandRepository
    {
        private readonly DbConnection _connection;

        public CategoryCommandRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            var sql = " INSERT INTO Categories(Id,Name,IsActive,CreatedAt) "
                    + " VALUES ( @Id, @Name, @IsActive, @CreatedAt ) ";

            var command = new CommandDefinition(
                sql,
                category,
                cancellationToken: cancellationToken);

            await _connection.ExecuteAsync(command);
            return category.Id;

        }

        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            var sql = " UPDATE Categories SET Name = @Name WHERE Id = @Id ";

            var command = new CommandDefinition(
                sql,
                category,
                cancellationToken: cancellationToken);

            var rowsAffected = await _connection.ExecuteAsync(command);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var sql = " UPDATE Categories SET IsActive = 0 WHERE Id = @Id AND IsActive = 1 ";

            var command = new CommandDefinition(
                sql,
                new { Id = id },
                cancellationToken: cancellationToken);

            var rowsAffected = await _connection.ExecuteAsync(command);
            return rowsAffected > 0;

        }
    }

}