using DAL.Models;
using DAL.Repositories;
using DTS.API.Services;
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

        public ChangeUserLoginAndPasswordCommand(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
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
            User user = await repository.Users.FindUserByIDAsync(command.Id);

            user.Login = command.Login;
            user.Password = command.Password;

            await repository.Users.UpdateAsync(user);
        }
    }
}
