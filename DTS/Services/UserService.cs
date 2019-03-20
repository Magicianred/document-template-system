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
        private IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>> _getUsersByStatusQuery;
        private IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>> _getUsersByTypeQuery;
        private IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>> _getUsersQuery;
        private IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO> _getUserByIdQuery;
        private IQueryHandlerAsync<GetUserTypesQuery, IEnumerable<string>> _getUserTypesQuery;
        private IQueryHandlerAsync<GetUserStatusesQuery, IEnumerable<string>> _getUserStatusesQuery;
        private IRepositoryWrapper repository;


        public UserService(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public ICommandHandlerAsync<ActivateUserCommand> ActivateUserCommand
        {
            get
            {
                if (_activateUserCommand == null)
                {
                    _activateUserCommand = new ActivateUserCommandHandler(repository);
                }
                return _activateUserCommand;
            }
        }


        public ICommandHandlerAsync<BlockUserCommand> BlockUserCommand
        {
            get
            {
                if (_blockUserCommand == null)
                {
                    _blockUserCommand = new BlockUserCommandHandler(repository);
                }
                return _blockUserCommand;
            }
        }


        public ICommandHandlerAsync<ChangeUserPersonalDataCommand> ChangeUserPersonalDataCommand
        {
            get
            {
                if (_changeUserPersonalDataCommand == null)
                {
                    _changeUserPersonalDataCommand = new ChangeUserPersonalDataCommandHandler(repository);
                }
                return _changeUserPersonalDataCommand;
            }
        }


        public ICommandHandlerAsync<ChangeUserTypeCommand> ChangeUserTypeCommand
        {
            get
            {
                if (_changeUserTypeCommand == null)
                {
                    _changeUserTypeCommand = new ChangeUserTypeCommandHandler(repository);
                }
                return _changeUserTypeCommand;
            }
        }


        public IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>> GetUsersByStatusQuery
        {
            get
            {
                if (_getUsersByStatusQuery == null)
                {
                    _getUsersByStatusQuery = new GetUsersByStatusQueryHandler(repository);
                }
                return _getUsersByStatusQuery;
            }
        }


        public IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>> GetUsersByTypeQuery
        {
            get
            {
                if (_getUsersByTypeQuery == null)
                {
                    _getUsersByTypeQuery = new GetUsersByTypeQueryHandler(repository);
                }
                return _getUsersByTypeQuery;
            }
        }


        public IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>> GetUsersQuery
        {
            get
            {
                if (_getUsersQuery == null)
                {
                    _getUsersQuery = new GetUsersQueryHandler(repository);
                }
                return _getUsersQuery;
            }
        }


        public IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO> GetUserByIdQuery
        {
            get
            {
                if (_getUserByIdQuery == null)
                {
                    _getUserByIdQuery = new GetUserByIdQueryHandler(repository);
                }
                return _getUserByIdQuery;
            }
        }

        public IQueryHandlerAsync<GetUserTypesQuery, IEnumerable<string>> GetUserTypesQuery
        {
            get
            {
                if (_getUserTypesQuery == null)
                {
                    _getUserTypesQuery = new GetUserTypesQueryHandler(repository);
                }
                return _getUserTypesQuery;
            }
        }

        public IQueryHandlerAsync<GetUserStatusesQuery, IEnumerable<string>> GetUserStatusesQuery
        {
            get
            {
                if (_getUserStatusesQuery == null)
                {
                    _getUserStatusesQuery = new GetUserStatusesQueryHandler(repository);
                }
                return _getUserStatusesQuery;
            }
        }
    }
}
