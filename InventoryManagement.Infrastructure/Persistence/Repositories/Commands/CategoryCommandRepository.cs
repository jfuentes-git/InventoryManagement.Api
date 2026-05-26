using Dapper;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Domain.Entities;
using System.Data;

public class CategoryCommandRepository : ICategoryCommandRepository
{
    private readonly IDbConnection _connection;

    public CategoryCommandRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        var sql = """
            INSERT INTO Categories
            (
                Id,
                Name,
                IsActive,
                CreatedAt
            )
            VALUES
            (
                @Id,
                @Name,
                @IsActive,
                @CreatedAt
            )
            """;
        await _connection.ExecuteAsync(sql, category);

        return category.Id;
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
    {

        var sql = """
            UPDATE Categories
            SET
                Name = @Name,
                IsActive = @IsActive
            WHERE Id = @Id
            """;
        var rowsAffected = await _connection.ExecuteAsync(sql,category);

        return rowsAffected > 0;
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sql = """
        UPDATE Categories
        SET IsActive = 0
        WHERE Id = @Id
          AND IsActive = 1
        """;

        var command = new CommandDefinition( sql, new { Id = id }, cancellationToken: cancellationToken);
        var rowsAffected = await _connection.ExecuteAsync(command);

        return rowsAffected > 0;
    }
}