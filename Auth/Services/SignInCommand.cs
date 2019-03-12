using Auth.Helpers;
using DAL.Models;
using DAL.Repositories;
using DTS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public sealed class SignInCommand : ICommand
    {
        public string Login { get; }
        public string Password { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }

        public SignInCommand(string login, string password, string name, string surname, string email)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
        }
    }

    public sealed class SignInCommandHandler : ICommandHandlerAsync<SignInCommand>
    {
        private readonly RepositoryWrapper repository;

        public SignInCommandHandler(RepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(SignInCommand command)
        {
            int suspendedStatusId = 2;
            int defoultUserTypeId = 3;

            if (await repository.Users.IsExistByLogin(command.Login))
            {
                throw new InvalidOperationException("Login already exist");
            }
            UserStatus status = await repository.UserStatus.FindStatusById(suspendedStatusId);
            UserType type = await repository.UserType.FindTypeById(defoultUserTypeId); 
            User user = createUser(command, status, type);
            await repository.Users.CreateAsync(user);
        }

        private User createUser(SignInCommand command, UserStatus userStatus, UserType userType)
        {
            IHashPassword hashHendler = new BCryptHash();
            return new User()
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                Login = command.Login,
                Password = hashHendler.Hash(command.Password),
                Status = userStatus,
                Type = userType
            };
        }
    }
}
