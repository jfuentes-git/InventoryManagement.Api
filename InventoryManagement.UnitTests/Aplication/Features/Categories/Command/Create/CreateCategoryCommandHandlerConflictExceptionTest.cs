
using FluentAssertions;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using InventoryManagement.Application.Features.Categories.Command.CreateCategory;
using InventoryManagement.Domain.Entities;
using Moq;
using Xunit;

namespace InventoryManagement.UnitTests.Aplication.Features.Categories.Command.Create;
    public class CreateCategoryCommandHandlerConflictExceptionTest
    {
        [Fact]
        public async Task Should_ThrowConflictException_When_CategoryAlreadyExists()
        {
            var commandRepo = new Mock<ICategoryCommandRepository>();
            var queryRepo = new Mock<ICategoryQueryRepository>();

            queryRepo.Setup(x => x.ExistsByCategoryNameAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new CreateCategoryCommandHandler(commandRepo.Object, queryRepo.Object);

            var command = new CreateCategoryCommand("Herramienta");
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ConflictException>();
        }
    }