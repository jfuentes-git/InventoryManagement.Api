

namespace InventoryManagement.Application.Common.Authentication
{
     public sealed record AuthResponse
            (
            string AccessToken, 
            DateTime ExpiresAt
            );
}
