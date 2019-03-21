using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class UserService : IUserService
    {
        private ICommandHandlerAsync<ActivateUserCommand> _activateUserCommand;
        private ICommandHandlerAsync<BlockUserCommand> _blockUserCommand;
        private ICommandHandlerAsync<ChangeUserPersonalDataCommand> _changeUserPersonalDataCommand;
        private ICommandHandlerAsync<ChangeUserTypeCommand> _changeUserTypeCommand;
        private ICommandHandlerAsync<DeleteUserCommand> _deleteUserCommand;
        private IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>> _getUsersByStatusQuery;
        private IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>> _getUsersByTypeQuery;
        private IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>> _getUsersQuery;
        private IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO> _getUserByIdQuery;
        private IQueryHandlerAsync<GetUserTypesQuery, IList<string>> _getUserTypesQuery;
        private IQueryHandlerAsync<GetUserStatusesQuery, IList<string>> _getUserStatusesQuery;
        private IQueryHandlerAsync<GetUserPersonalDataQuery, UserPersonalData> _getUserPersonalDataQuery;
        private IRepositoryWrapper repository;


        public UserService(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public ICommandHandlerAsync<ActivateUserCommand> ActivateUserCommand
        {
            get
            {
                _activateUserCommand = _activateUserCommand ?? new ActivateUserCommandHandler(repository);

                return _activateUserCommand;
            }
        }


        public ICommandHandlerAsync<BlockUserCommand> BlockUserCommand
        {
            get
            {
                _blockUserCommand = _blockUserCommand ?? new BlockUserCommandHandler(repository);

                return _blockUserCommand;
            }
        }


        public ICommandHandlerAsync<ChangeUserPersonalDataCommand> ChangeUserPersonalDataCommand
        {
            get
            {
                _changeUserPersonalDataCommand = _changeUserPersonalDataCommand ?? new ChangeUserPersonalDataCommandHandler(repository);

                return _changeUserPersonalDataCommand;
            }
        }


        public ICommandHandlerAsync<ChangeUserTypeCommand> ChangeUserTypeCommand
        {
            get
            {
                _changeUserTypeCommand = _changeUserTypeCommand ?? new ChangeUserTypeCommandHandler(repository);

                return _changeUserTypeCommand;
            }
        }

        public ICommandHandlerAsync<DeleteUserCommand> DeleteUserCommand
        {
            get
            {
                _deleteUserCommand = _deleteUserCommand ?? new DeleteUserCommandHandler(repository);

                return _deleteUserCommand;
            }
        }

        public IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>> GetUsersByStatusQuery
        {
            get
            {
                _getUsersByStatusQuery = _getUsersByStatusQuery ?? new GetUsersByStatusQueryHandler(repository);

                return _getUsersByStatusQuery;
            }
        }


        public IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>> GetUsersByTypeQuery
        {
            get
            {
                _getUsersByTypeQuery = _getUsersByTypeQuery ?? new GetUsersByTypeQueryHandler(repository);

                return _getUsersByTypeQuery;
            }
        }


        public IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>> GetUsersQuery
        {
            get
            {
                _getUsersQuery = _getUsersQuery ?? new GetUsersQueryHandler(repository);

                return _getUsersQuery;
            }
        }


        public IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO> GetUserByIdQuery
        {
            get
            {
                _getUserByIdQuery = _getUserByIdQuery ?? new GetUserByIdQueryHandler(repository);

                return _getUserByIdQuery;
            }
        }

        public IQueryHandlerAsync<GetUserTypesQuery, IList<string>> GetUserTypesQuery
        {
            get
            {
                _getUserTypesQuery = _getUserTypesQuery ?? new GetUserTypesQueryHandler(repository);

                return _getUserTypesQuery;
            }
        }

        public IQueryHandlerAsync<GetUserStatusesQuery, IList<string>> GetUserStatusesQuery
        {
            get
            {
                _getUserStatusesQuery = _getUserStatusesQuery ?? new GetUserStatusesQueryHandler(repository);

                return _getUserStatusesQuery;
            }
        }

        public IQueryHandlerAsync<GetUserPersonalDataQuery, UserPersonalData> GetUserPersonalDataQuery
        {
            get
            {
                _getUserPersonalDataQuery = _getUserPersonalDataQuery ?? new GetUserPersonalDataQueryHandler(repository);

                return _getUserPersonalDataQuery;
            }
        }
    }
}
