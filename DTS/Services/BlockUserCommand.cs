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
        private readonly string _blockUserStatus = "Blocked";

        public BlockUserCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(BlockUserCommand command)
        {
            User user = await repository.Users.FindUserByIDAsync(command.Id);
            UserStatus activeStatus = await repository.UserStatus
                .FindUserStatusByName(_blockUserStatus);

            user.Status = activeStatus;
            await repository.Users.UpdateAsync(user);
        }
    }
}
