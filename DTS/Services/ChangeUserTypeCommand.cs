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
        public string role { get; }

        public ChangeUserTypeCommand(int id, string role)
        {
            Id = id;
            this.role = role;
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

        public Task HandleAsync(ChangeUserTypeCommand command)
        {
            
        }

        private async Task<UserType>
    }
}
