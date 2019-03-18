using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetUserByIdQuery : IQuery
    {
        public int Id { get; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }

    public sealed class GetUserByIdQueryHandler
        : IQueryHandlerAsync<GetUserByIdQuery, ExtendedUserDTO>
    {
        private IRepositoryWrapper repository;

        public GetUserByIdQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<ExtendedUserDTO> HandleAsync(GetUserByIdQuery query)
        {
            var user = await repository.Users.FindUserByIDAsync(query.Id);
            return ExtendedUserDTO.ParseUser(user);
        }
    }
}
