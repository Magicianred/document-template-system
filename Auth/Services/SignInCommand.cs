using DAL.Repositories;
using DTS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class SignInCommand : ICommand
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

    public class SignInCommandHandler : ICommandHandlerAsync<SignInCommand>
    {
        private RepositoryWrapper repository;

        public SignInCommandHandler(RepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(SignInCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
