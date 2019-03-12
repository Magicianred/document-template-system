using DAL.Models;
using DAL.Repositories;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class BlockUserCommand : ICommand
    {
        public int Id { get; }

        public BlockUserCommand(int id)
        {
            Id = id;
        }
    }

    public sealed class BlockUserCommandHandler : ICommandHandlerAsync<BlockUserCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly int _activeUserStatusId = 3;

        public BlockUserCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(BlockUserCommand command)
        {
            User user = await repository.Users.FindUserByIDAsync(command.Id);
            UserStatus activeStatus = await repository.UserStatus
                .FindStatusById(_activeUserStatusId);

            user.Status = activeStatus;
            await repository.Users.UpdateAsync(user);
        }
    }
}
