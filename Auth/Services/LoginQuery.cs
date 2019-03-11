using DAL.Repositories;
using DTS.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public sealed class LoginQuery : IQuery
    {
        public string Login { get; }
        public string Password { get; }

        public LoginQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    public sealed class LoginQueryHandler : IQueryHandlerAsync<LoginQuery, JwtSecurityToken>
    {
        private readonly RepositoryWrapper repository;

        public LoginQueryHandler(RepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<JwtSecurityToken> HandleAsync(LoginQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
