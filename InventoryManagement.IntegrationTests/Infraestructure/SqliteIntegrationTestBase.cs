using InventoryManagement.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.IntegrationTests.Infraestructure;

public abstract class SqliteIntegrationTestBase : IDisposable
{
    protected SqliteConnection Connection { get; private set; }
    protected readonly InventoryDbContext Context;

    protected SqliteIntegrationTestBase()
    {

        Connection = new SqliteConnection("Data Source=:memory:");

        Connection.Open();

        var options = new DbContextOptionsBuilder<InventoryDbContext>().UseSqlite(Connection).Options;

        Context = new InventoryDbContext(options);

        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
        Connection.Dispose();
    }
}