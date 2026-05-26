using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Authentication.DTO
{
    public class UsuarioDTO
    {

        public required string UsserName { get; set; }
        public required string Password { get; set; }

    }
}
