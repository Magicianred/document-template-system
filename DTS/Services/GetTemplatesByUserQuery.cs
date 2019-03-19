using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplatesByUserQuery : IQuery
    {
        public int UserId { get; }

        public GetTemplatesByUserQuery(int userId)
        {
            UserId = userId;
        }
    }

    public sealed class GetTemplatesByUserQueryHandler
        : IQueryHandlerAsync<GetTemplatesByUserQuery, List<TemplateDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplatesByUserQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<List<TemplateDTO>> HandleAsync(GetTemplatesByUserQuery query)
        {
            var userTemplates = await repository
                .Templates.FindTemplatesByOwnerIdAsync(query.UserId);
  
            return TemplateDTO.ParseTemplatesDTO(userTemplates);
        }
    }
}
