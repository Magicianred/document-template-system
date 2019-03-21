using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class DeleteUserCommand : ICommand
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }

    public sealed class DeleteUserCommandHandler
        : ICommandHandlerAsync<DeleteUserCommand>
    {
        private readonly IRepositoryWrapper repository;

        public DeleteUserCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(DeleteUserCommand command)
        {
            string suspendedStatus = "Suspended";
            var user = await repository.Users.FindUserByIDAsync(command.Id);

            if (!user.Status.Name.Equals(suspendedStatus)) 
            {
                throw new InvalidOperationException();       
            }

            await repository.Users.DeleteAsync(user);
        }
    }
}
