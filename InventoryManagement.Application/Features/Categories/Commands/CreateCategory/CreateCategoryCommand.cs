using MediatR;
using InventoryManagement.Application.Common.Models;

namespace InventoryManagement.Application.Features.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name) : IRequest<CreatedResponse>;

}
