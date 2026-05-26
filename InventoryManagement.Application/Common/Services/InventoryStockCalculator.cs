using InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command;
using InventoryManagement.Domain.Enums;


namespace InventoryManagement.Application.Common.Services
{
    public sealed class InventoryStockCalculator : IInventoryStockCalculator
    {
        public int Calculate(int currentStock, MovementType movementType, int quantity)
        {

            return movementType switch
            {
                MovementType.Entry => currentStock + quantity,

                MovementType.Exit => ValidateExit(currentStock, quantity),

                MovementType.Adjustment => currentStock + quantity,

                _ => throw new ArgumentOutOfRangeException(nameof(movementType), "Tipo de movimiento incorrecto.")
            };
        }
            
        private static int ValidateExit(int currentStock, int quantity)
        {
            if (quantity > currentStock)
                throw new InvalidOperationException("Stock Insuficiente");

            return currentStock - quantity;
        }
    }
}
   