
using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.Products.Command.DeleteProduct
{
    public sealed record DeleteProductCommand
    (
      Guid Id
    ) : IRequest<OperationResult>;
}
