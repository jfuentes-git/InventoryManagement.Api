using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.Categories.Command.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
                   Guid Id,
                   string Name
        ) : IRequest<OperationResult>;
}
