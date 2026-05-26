using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Common.Models
{
    public sealed record CreatedResponse(Guid Id, string Message);

}
