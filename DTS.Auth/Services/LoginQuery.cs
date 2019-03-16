using DTS.Auth.Helpers;
using DAL.Models;
using DAL.Repositories;
using DTS.API.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTS.Auth.Services
{
    public sealed class LoginQuery : IQuery
    {
        public string Login { get; }
        public string Password { get; }
        public ITokenHelper TokenHelper { get; }

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
            string errorMessage = "Account is inactive or Incorrect Login or Password";
            try
            {
                var user = await repository.Users.FindByUserLogin(query.Login);
                if (validatePassword(user, query.Password))
                {
                    if (!user.Status.Name.Equals("Active"))
                    {
                        throw new KeyNotFoundException(errorMessage);
                    }

                    return query.TokenHelper.GetNewToken(user.Id, user.Type.Name);
                }
                throw new KeyNotFoundException(errorMessage);
            } catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(errorMessage);
            }
        }

        private bool validatePassword(User user, string password)
        {
            IHashPassword hashHandler = new BCryptHash();
            return hashHandler.Verify(password, user.Password);
        }
    }
}
