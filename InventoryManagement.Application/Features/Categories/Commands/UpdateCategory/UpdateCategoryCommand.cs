using InventoryManagement.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
                   Guid Id,
                   string Name
        ) : IRequest<OperationResult>;
}
