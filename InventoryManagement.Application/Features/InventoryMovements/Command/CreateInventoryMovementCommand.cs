using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.InventoryMovements.Command
{
    public sealed record CreateInventoryMovementCommand
    (
      Guid ProductId,
      MovementType MovementType,
      int Quantity
    ) : IRequest<CreatedResponse>;
}
