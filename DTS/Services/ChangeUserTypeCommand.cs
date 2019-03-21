using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class ChangeUserTypeCommand : ICommand
    {
        public int Id { get; }
        public string Type { get; }

        public ChangeUserTypeCommand(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }

    public sealed class ChangeUserTypeCommandHandler
        : ICommandHandlerAsync<ChangeUserTypeCommand>
    {
        private readonly IRepositoryWrapper repository;

        public ChangeUserTypeCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ChangeUserTypeCommand command)
        {
            UserType userType = await repository.UserType.FindUserTypeByName(command.Type);
            User user = await repository.Users.FindUserByIDAsync(command.Id);

            user.Type = userType;

            await repository.Users.UpdateAsync(user);
        }
    }
}
