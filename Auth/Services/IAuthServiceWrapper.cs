using DTS.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IAuthServiceWrapper
    {
        IQueryHandlerAsync<LoginQuery, SecurityToken> Login { get; }
        ICommandHandlerAsync<SignInCommand> SignIn { get; }
    }
}
