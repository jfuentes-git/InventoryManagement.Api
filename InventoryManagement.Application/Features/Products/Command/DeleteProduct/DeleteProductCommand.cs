using InventoryManagement.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.FeaturesCatalog.Command.DeleteProduct
{
    public sealed record DeleteProductCommand
    (
      Guid Id
    ) : IRequest<OperationResult>;
}
