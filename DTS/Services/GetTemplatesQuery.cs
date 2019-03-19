using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplatesQuery : IQuery
    {
    }


    public sealed class GetTemplatesQueryHandler : IQueryHandlerAsync<GetTemplatesQuery, List<TemplateDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplatesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<List<TemplateDTO>> HandleAsync(GetTemplatesQuery query)
        {
            var templates = await repository.Templates.FindAllTemplatesAsync();
            
            return TemplateDTO.ParseTemplatesDTO(templates);
        }
    }
}
