using DAL.Models;
using DAL.Repositories;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class ActivateUserCommand : ICommand
    {
        public int Id { get; }

        public ActivateUserCommand(int id)
        {
            Id = id;
        }
    }

    public sealed class ActivateUserCommandHandler : ICommandHandlerAsync<ActivateUserCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _activeUserStatus = "Active";

        public ActivateUserCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ActivateUserCommand command)
        {
            User user = await repository.Users.FindUserByIDAsync(command.Id);
            UserStatus activeStatus = await repository.UserStatus
                .FindUserStatusByName(_activeUserStatus);

            user.Status = activeStatus;
            await repository.Users.UpdateAsync(user);
        }
    }
}
