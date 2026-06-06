
using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.Products.Command.CreateProduct
{
    public sealed record CreateProductCommand(string Name,decimal Price,Guid CategoryId) : IRequest<CreatedResponse>;

}
