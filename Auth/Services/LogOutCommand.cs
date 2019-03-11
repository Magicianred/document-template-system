using DAL.Repositories;
using DTS.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public sealed class LogOutCommand : ICommand
    {
        public JwtSecurityToken Token { get; }

        public LogOutCommand(JwtSecurityToken token)
        {
            Token = token;
        }
    }

    public sealed class LogOutCommandHandler : ICommandHandlerAsync<LogOutCommand>
    {
        private readonly RepositoryWrapper repository;

        public LogOutCommandHandler(RepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public Task HandleAsync(LogOutCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
