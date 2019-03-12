using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace DTS.Auth.Helpers
{
    public interface ITokenHelper
    {
        SecurityToken GetNewToken(int userId, string userRole);
        SecurityToken ParseToken(string tokenString);
        string WriteToken(SecurityToken token);
    }
}
