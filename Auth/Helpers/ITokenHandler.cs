using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Helpers
{
    interface ITokenHandler
    {
        JwtSecurityToken GetNewToken(int userId, string userRole);
        void DisposeToken(JwtSecurityToken token);
    }
}
