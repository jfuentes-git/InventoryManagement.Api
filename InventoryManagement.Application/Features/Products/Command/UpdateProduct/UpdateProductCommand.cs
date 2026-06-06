
using InventoryManagement.Application.Common.Models;
using MediatR;


namespace InventoryManagement.Application.Features.Products.Command.UpdateProduct
{
    public sealed record UpdateProductCommand
   (
       Guid Id,
       string Name,
       decimal Price,
       Guid CategoryId
   ) : IRequest<OperationResult>;
}
