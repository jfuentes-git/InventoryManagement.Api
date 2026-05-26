using InventoryManagement.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct
{
    public sealed record UpdateProductCommand
   (
       Guid Id,
       string Name,
       decimal Price,
       Guid CategoryId,
       bool IsActive
   ) : IRequest<OperationResult>;
}
