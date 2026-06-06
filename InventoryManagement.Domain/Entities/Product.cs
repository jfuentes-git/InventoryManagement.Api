
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Exceptions;

namespace InventoryManagement.Domain.Entities

{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; private set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; } = true;

        private void SetStock(int stock)
        {
            if (stock < 0)
                throw new DomainException(
                    "El stock no puede ser negativo.");

            Stock = stock;
        }

        public void ApplyMovement(MovementType movementType, int quantity)
        {
            switch (movementType)
            {
                case MovementType.Entry:
                    IncreaseStock(quantity);
                    break;

                case MovementType.Exit:
                    DecreaseStock(quantity);
                    break;

                case MovementType.Adjustment:
                    AdjustStock(quantity);
                    break;

                default:
                    throw new DomainException("Movimiento Invalido");
            }
        }

        private void IncreaseStock(int quantity)
        {
            SetStock(Stock + quantity);
        }

        private void DecreaseStock(int quantity)
        {
            ValidaStock(quantity);
            SetStock(Stock - quantity);
        }

        private void AdjustStock(int quantity)
        {
            SetStock(Stock + quantity);
        }

        private void ValidaStock(int quantity)
        {
            if (quantity > Stock)
                throw new DomainException(
                    "Stock insuficiente.");
        }
    }
}