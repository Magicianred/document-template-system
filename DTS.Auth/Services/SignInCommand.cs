using DTS.API.Helpers;
using DAL.Models;
using DAL.Repositories;
using DTS.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTS.Auth.Helpers;

namespace DTS.Auth.Services
{
    public sealed class SignInCommand : ICommand
    {
        public string Login { get; }
        public string Password { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public IHashPassword HashHendler { get; }
        public ICredentialsRestrictionValidation credentialsRestriction;

        public SignInCommand(string login, string password, string name, string surname, string email, IHashPassword hashHendler, ICredentialsRestrictionValidation credentialsRestriction)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            HashHendler = hashHendler;
            this.credentialsRestriction = credentialsRestriction;
        }
    }

    public sealed class SignInCommandHandler : ICommandHandlerAsync<SignInCommand>
    {
        private readonly IRepositoryWrapper repository;

        public SignInCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(SignInCommand command)
        {
            string errorMessage = "Account exist or password is in wrong format";
            int suspendedStatusId = 2;
            int defoultUserTypeId = 3;

            if (command.credentialsRestriction.VerifyPassword(command.Login, command.Password))
            {
                throw new KeyNotFoundException(errorMessage);
            }

            if (await repository.Users.IsExistByLogin(command.Login))
            {
                throw new InvalidOperationException(errorMessage);
            }
            UserStatus status = await repository.UserStatus.FindUserStatusById(suspendedStatusId);
            UserType type = await repository.UserType.FindUserTypeById(defoultUserTypeId); 
            User user = createUser(command, status, type);
            await repository.Users.CreateAsync(user);
        }

        private User createUser(SignInCommand command, UserStatus userStatus, UserType userType)
        {
            return new User()
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                Login = command.Login,
                Password = command.HashHendler.Hash(command.Password),
                Status = userStatus,
                Type = userType
            };
        }
    }
}
