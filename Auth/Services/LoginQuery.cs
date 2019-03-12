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
                var user = await repository.Users.FindByUserLogin(query.Login);
                if (validatePassword(user, query.Password))
                {
                    if (!user.Status.Name.Equals("Active"))
                    {
                        return null;
                    }

                    ITokenHelper tokenHandler = new TokenHelper(secret);
                    return tokenHandler.GetNewToken(user.Id, user.Type.Name);
                }
                throw new KeyNotFoundException("Password incorrect");
            } catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("Incorrect Login");
            }
        }

        private bool validatePassword(User user, string password)
        {
            IHashPassword hashHandler = new BCryptHash();
            return hashHandler.Verify(password, user.Password);
        }
    }
}
