using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authentication
{
     public sealed record LoginResponse(
            string AccessToken, 
            DateTime ExpiresAt
            );
}
