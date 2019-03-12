using DTS.Helpers;
using DAL.Models;
using DAL.Repositories;
using DTS.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Services
{
    public sealed class LoginQuery : IQuery
    {
        public string Login { get; }
        public string Password { get; }
        internal ITokenHelper TokenHelper { get; }

        public LoginQuery(string login, string password, ITokenHelper tokenHelper)
        {
            Login = login;
            Password = password;
            TokenHelper = tokenHelper;
        }
    }

    public sealed class LoginQueryHandler : IQueryHandlerAsync<LoginQuery, SecurityToken>
    {
        private readonly IRepositoryWrapper repository;

        public LoginQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
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

                    return query.TokenHelper.GetNewToken(user.Id, user.Type.Name);
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
