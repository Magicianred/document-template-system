using Auth.Helpers;
using DAL.Models;
using DAL.Repositories;
using DTS.Services;
using Microsoft.IdentityModel.Tokens;
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

    public sealed class LoginQueryHandler : IQueryHandlerAsync<LoginQuery, SecurityToken>
    {
        private readonly RepositoryWrapper repository;
        private readonly string secret;

        public LoginQueryHandler(RepositoryWrapper repository, string secret)
        {
            this.repository = repository;
            this.secret = secret;
        }

        public async Task<SecurityToken> HandleAsync(LoginQuery query)
        {
            try
            {
                Authorization auth = await repository.Authorizations.FindByUserLogin(query.Login);
                if (validatePassword(auth, query.Password))
                {
                    ITokenHelper tokenHandler = new TokenHelper(secret);
                    return tokenHandler.GetNewToken(auth.user.Id, auth.user.Type.Name);
                }
                throw new KeyNotFoundException("Password incorrect");
            } catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("Incorrect Login");
            }
        }

        private bool validatePassword(Authorization auth, string password)
        {
            IHashPassword hashHandler = new BCryptHash();
            return hashHandler.Verify(password, auth.Pasword);
        }
    }
}
