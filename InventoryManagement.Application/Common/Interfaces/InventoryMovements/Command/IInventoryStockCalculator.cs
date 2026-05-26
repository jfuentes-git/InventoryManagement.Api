using InventoryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Common.Interfaces.InventoryMovements.Command
{
    public interface IInventoryStockCalculator
    {
    int Calculate(int currentStock, MovementType movementType, int quantity);

    }
}
