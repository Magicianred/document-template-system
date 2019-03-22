using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public interface IUserService
    {
        ICommandHandlerAsync<ActivateUserCommand> ActivateUserCommand { get;}
        ICommandHandlerAsync<BlockUserCommand> BlockUserCommand { get; }
        ICommandHandlerAsync<ChangeUserPersonalDataCommand> ChangeUserPersonalDataCommand { get; }
        ICommandHandlerAsync<ChangeUserTypeCommand> ChangeUserTypeCommand { get; }
        ICommandHandlerAsync<DeleteUserCommand> DeleteUserCommand { get; }
        IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>> GetUsersByStatusQuery { get; }
        IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>> GetUsersByTypeQuery { get; }
        IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>> GetUsersQuery { get; }
        IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO> GetUserByIdQuery { get; }
        IQueryHandlerAsync<GetUserTypesQuery, IList<string>> GetUserTypesQuery { get; }
        IQueryHandlerAsync<GetUserStatusesQuery, IList<string>> GetUserStatusesQuery { get; }
        IQueryHandlerAsync<GetUserPersonalDataQuery, UserPersonalDataDTO> GetUserPersonalDataQuery { get; }
    }
}
