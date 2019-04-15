using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetUsersByTypeQuery : IQuery
    {
        public string Type { get; }

        public GetUsersByTypeQuery(string type)
        {
            Type = type;
        }
    }


    public sealed class GetUsersByTypeQueryHandler
        : IQueryHandlerAsync<GetUsersByTypeQuery, List<ExtendedUserDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetUsersByTypeQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<List<ExtendedUserDTO>> HandleAsync(GetUsersByTypeQuery query)
        {
            var users = await repository.Users
                .FindUserByCondition(u => u.Type.Name.ToUpper().Equals(query.Type.ToUpper()));

            return ExtendedUserDTO.ParseUsersDTO(users);
        }
    }
}
