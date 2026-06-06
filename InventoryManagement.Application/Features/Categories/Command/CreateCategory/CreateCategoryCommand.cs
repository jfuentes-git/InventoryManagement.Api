
using MediatR;
using InventoryManagement.Application.Common.Models;

namespace InventoryManagement.Application.Features.Categories.Command.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name) : IRequest<CreatedResponse>;

}
