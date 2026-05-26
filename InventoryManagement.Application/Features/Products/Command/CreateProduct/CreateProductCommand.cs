using InventoryManagement.Application.Common.Models;
using MediatR;

namespace InventoryManagement.Application.Features.FeaturesCatalog.Command.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        decimal Price,
        Guid CategoryId
        ) : IRequest<CreatedResponse>;
}
