using DTS.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IAuthServiceWrapper
    {
        IQueryHandlerAsync<LoginQuery, JwtSecurityToken> Login { get; }
        ICommandHandlerAsync<SignInCommand> SignIn { get; }
    }
}
