using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Common.Models
{
    public sealed class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
    }

}