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
        public IHashPassword HashHandler { get; }
        public IRequestMonitor RequestMonitor { get; }

        public LoginQuery(string login, string password, IHashPassword hashHandler, IRequestMonitor requestMonitor)
        {
            Login = login;
            Password = password;
            HashHandler = hashHandler;
            RequestMonitor = requestMonitor;
        }
    }

    public sealed class LoginQueryHandler : IQueryHandlerAsync<LoginQuery, User>
    {
        private readonly IRepositoryWrapper repository;

        public LoginQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<User> HandleAsync(LoginQuery query)
        {
            string errorMessage = "Account is inactive or Incorrect Login or Password";
            try
            {
                var user = await repository.Users.FindByUserLogin(query.Login);

                if (query.RequestMonitor.IsReachedLoginAttemptsLimit(query.Login))
                {
                    throw new KeyNotFoundException(errorMessage);
                }

                if (validatePassword(user, query))
                {
                    query.RequestMonitor.ResetLoginAttempts(query.Login);
                    if (!user.Status.Name.Equals("Active"))
                    {
                        throw new KeyNotFoundException(errorMessage);
                    }

                    return user;
                }
                throw new KeyNotFoundException(errorMessage);
            } catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(errorMessage);
            }
        }

        private bool validatePassword(User user, LoginQuery query)
        {
            return query.HashHandler.Verify(query.Password, user.Password);
        }
    }
}
