using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetUsersQuery : IQuery
    {
    }

    public sealed class GetUsersQueryHandler
        : IQueryHandlerAsync<GetUsersQuery, List<ExtendedUserDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetUsersQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<List<ExtendedUserDTO>> HandleAsync(GetUsersQuery query)
        {
            var users = await repository.Users.FindAllUsersAsync();
            return CollectUsersDTOs(users);
        }

        private List<ExtendedUserDTO> CollectUsersDTOs(IEnumerable<User> users)
        {
            var usersDtos = new List<ExtendedUserDTO>();
            foreach (var user in users)
            {
                usersDtos.Add(ExtendedUserDTO.parseUser(user));
            }
            return usersDtos;
        }
    }
}
