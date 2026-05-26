using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record CategoryDetailResponse
    (
        Guid Id,
        string Name,
        bool IsActive,
        DateTime CreatedAt
    );
}
