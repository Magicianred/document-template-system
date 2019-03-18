using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplateByIdQuery : IQuery
    {
        public int Id { get; }

        public GetTemplateByIdQuery(int id)
        {
            Id = id;
        }
    }

    
    public sealed class GetTemplateByIdQueryHandler
        : IQueryHandlerAsync<GetTemplateByIdQuery, TemplateDTO>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplateByIdQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<TemplateDTO> HandleAsync(GetTemplateByIdQuery query)
        {
            var template = await repository.Templates.FindTemplateByIdAsync(query.Id);
            return TemplateDTO.ParseTemplateDTO(template);
        }

    }
}
