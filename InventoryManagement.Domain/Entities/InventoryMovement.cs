using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities
{
    public class InventoryMovement : BaseEntity
    {
        public Guid ProductId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
    }
}
