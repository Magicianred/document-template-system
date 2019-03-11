using DTS.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class LogOutCommand : ICommand
    {
        public JwtSecurityToken Token { get; }

        public LogOutCommand(JwtSecurityToken token)
        {
            Token = token;
        }
    }

    public class LogOutCommandHandler : ICommandHandlerAsync<LogOutCommand>
    {
        public Task HandleAsync(LogOutCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
