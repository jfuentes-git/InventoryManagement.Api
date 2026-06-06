
using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.Categories.Command.DeleteCategory
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<OperationResult>;
}
