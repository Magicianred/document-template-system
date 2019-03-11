using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Helpers
{
    interface ITokenHelper
    {
        SecurityToken GetNewToken(int userId, string userRole);
        SecurityToken parseToken(string tokenString);
    }
}
