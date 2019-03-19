using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetUsersByStatusQuery : IQuery
    {
        public string Status { get; }

        public GetUsersByStatusQuery(string status)
        {
            Status = status;
        }
    }

    public sealed class GetUsersByStatusQueryHandler
        : IQueryHandlerAsync<GetUsersByStatusQuery, List<ExtendedUserDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetUsersByStatusQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<List<ExtendedUserDTO>> HandleAsync(GetUsersByStatusQuery query)
        {
            var users = await repository.Users
                .FindUserByCondition(u => u.Status.Name.ToUpper().Equals(query.Status.ToUpper()));

            return ExtendedUserDTO.ParseUsersDTO(users);
        }   
    }
}
