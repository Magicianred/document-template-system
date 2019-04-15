using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetUserPersonalDataQuery : IQuery
    {
        public int Id { get; }

        public GetUserPersonalDataQuery(int id)
        {
            Id = id;
        }
    }

    public sealed class GetUserPersonalDataQueryHandler
        : IQueryHandlerAsync<GetUserPersonalDataQuery, UserPersonalDataDTO>
    {
        private readonly IRepositoryWrapper repository;

        public GetUserPersonalDataQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<UserPersonalDataDTO> HandleAsync(GetUserPersonalDataQuery query)
        {
            var user = await repository.Users.FindUserByIDAsync(query.Id);
            var userDTO = UserPersonalDataDTO.ParseUserPersonalDataDTO(user);
            return userDTO;
        }
    }
}
