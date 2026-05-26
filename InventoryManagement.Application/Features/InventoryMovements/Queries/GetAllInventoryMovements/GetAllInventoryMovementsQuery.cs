using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.InventoryMovements.Queries.GetAllInventoryMovements
{

    public sealed record GetAllInventoryMovementsQuery(): IRequest<List<InventoryMovementResponse>>;
}
