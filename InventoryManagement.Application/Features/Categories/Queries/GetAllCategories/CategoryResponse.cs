using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Queries.GetAllCategories
{
     public sealed record CategoryResponse(
      Guid Id,
      string Name,
      DateTime CreatedAt
     );
}
