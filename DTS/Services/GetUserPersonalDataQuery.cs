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
        : IQueryHandlerAsync<GetUserPersonalDataQuery, UserPersonalData>
    {
        private readonly IRepositoryWrapper repository;

        public GetUserPersonalDataQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<UserPersonalData> HandleAsync(GetUserPersonalDataQuery query)
        {
            var user = await repository.Users.FindUserByIDAsync(query.Id);
            var userDTO = UserPersonalData.ParseUserPersonalData(user);
            return userDTO;
        }
    }
}
