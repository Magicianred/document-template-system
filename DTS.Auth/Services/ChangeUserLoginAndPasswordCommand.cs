using DAL.Models;
using DAL.Repositories;
using DTS.API.Services;
using DTS.Auth.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Services
{
    public sealed class ChangeUserLoginAndPasswordCommand : ICommand
    {
        public int Id { get; }
        public string Login { get; }
        public string Password { get; }
        public ICredentialsRestrictionValidation credentialsRestriction;

        public ChangeUserLoginAndPasswordCommand(int id, string login, string password, ICredentialsRestrictionValidation credentialsRestriction)
        {
            Id = id;
            Login = login;
            Password = password;
            this.credentialsRestriction = credentialsRestriction;
        }
    }

    public sealed class ChangeUserLoginAndPasswordCommandHandler
        : ICommandHandlerAsync<ChangeUserLoginAndPasswordCommand>
    {
        private readonly IRepositoryWrapper repository;

        public ChangeUserLoginAndPasswordCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ChangeUserLoginAndPasswordCommand command)
        {
            if (command.credentialsRestriction.VerifyPassword(command.Login, command.Password))
            {
                throw new InvalidOperationException("Password is in wrong format");
            }

            User user = await repository.Users.FindUserByIDAsync(command.Id);
            if (command.Login != null)
            {
                user.Login = command.Login;
            }
            if (command.Password != null)
            {
                user.Password = command.Password;
            }

            await repository.Users.UpdateAsync(user);
        }
    }
}
