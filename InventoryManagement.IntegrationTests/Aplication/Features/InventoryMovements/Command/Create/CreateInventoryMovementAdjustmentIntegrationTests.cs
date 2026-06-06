
using InventoryManagement.Application.Features.InventoryMovements.Command.Create;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Infrastructure.Persistence.Repositories.Categories.Command;
using InventoryManagement.Infrastructure.Persistence.Repositories.InventoryMovements.Command;
using InventoryManagement.Infrastructure.Persistence.Repositories.Products.Command;
using InventoryManagement.Infrastructure.Persistence.Repositories.Products.Queries;
using InventoryManagement.Infrastructure.Persistence.UnitOfWork;
using InventoryManagement.IntegrationTests.Infraestructure;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagement.IntegrationTests.Aplication.Features.InventoryMovements.Command.Create;
public sealed class CreateInventoryMovementAdjustmentIntegrationTests : SqliteIntegrationTestBase 
{

    [Fact]
    public async Task Handle_ShouldCreateInventoryMovementAndUpdateProductStock_WhenMovementIsNegativeAdjustment()
    {

        var categoryCommandRepository = new CategoryCommandRepository(Connection);
        var productCommandRepository = new ProductCommandRepository(Connection);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics"
        };

        await categoryCommandRepository.CreateAsync(category, CancellationToken.None);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Price = 1000,
            CategoryId = category.Id
        };

        product.ApplyMovement(MovementType.Entry, 10);

        await productCommandRepository.CreateAsync(product, CancellationToken.None);

        var handler = new CreateInventoryMovementCommandHandler(
            new ProductQueryRepository(Context),
            new InventoryMovementCommandRepository(Connection),
            new UnitOfWork(Connection),
            new ProductCommandRepository(Connection));

        var command = new CreateInventoryMovementCommand(product.Id, MovementType.Adjustment,-2);

        await handler.Handle(command, CancellationToken.None);

        var updatedProduct = await Context.Products.AsNoTracking().FirstAsync(x => x.Id == product.Id);

        Assert.Equal(8, updatedProduct.Stock);

        var movement = await Context.InventoryMovements.AsNoTracking().FirstAsync();

        Assert.Equal(product.Id, movement.ProductId);
        Assert.Equal(-2, movement.Quantity);
        Assert.Equal(MovementType.Adjustment, movement.MovementType);
    }
}