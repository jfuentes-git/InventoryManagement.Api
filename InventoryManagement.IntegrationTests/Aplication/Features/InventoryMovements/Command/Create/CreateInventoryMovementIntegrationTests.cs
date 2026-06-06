
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.IntegrationTests.Infraestructure;
using InventoryManagement.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Application.Features.InventoryMovements.Command.Create;
using InventoryManagement.Infrastructure.Persistence.Repositories.Products.Command;
using InventoryManagement.Infrastructure.Persistence.Repositories.Products.Queries;
using InventoryManagement.Infrastructure.Persistence.Repositories.InventoryMovements.Command;


namespace InventoryManagement.IntegrationTests.Aplication.Features.InventoryMovements.Command.Create;

public sealed class CreateInventoryMovementIntegrationTests : SqliteIntegrationTestBase
{

    [Fact]
    public async Task Handle_ShouldCreateInventoryMovementAndUpdateProductStock_WhenMovementIsEntry()
    {

        var categoryCommandRepository = new Infrastructure.Persistence.Repositories.Categories.Command.CategoryCommandRepository(Connection);
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

        var command = new CreateInventoryMovementCommand(product.Id, MovementType.Entry, 5);

        await handler.Handle(command, CancellationToken.None);

        var updatedProduct = await Context.Products.AsNoTracking().FirstAsync(x => x.Id == product.Id);

        Assert.Equal(15, updatedProduct.Stock);

        var movement = await Context.InventoryMovements.AsNoTracking().FirstAsync();

        Assert.Equal(product.Id, movement.ProductId);
        Assert.Equal(5, movement.Quantity);
        Assert.Equal(MovementType.Entry, movement.MovementType);
    }
}