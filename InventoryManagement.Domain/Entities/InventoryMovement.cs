using InventoryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Entities
{
    public class InventoryMovement : BaseEntity
    {
        public Guid ProductId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }       
    }
}
