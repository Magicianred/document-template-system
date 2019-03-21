using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetActiveTemplatesQuery : IQuery
    {
    }

    public sealed class GetActiveTemplatesQueryHandler
        : IQueryHandlerAsync<GetActiveTemplatesQuery, IList<TemplateDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetActiveTemplatesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<IList<TemplateDTO>> HandleAsync(GetActiveTemplatesQuery query)
        {
            string activeState = "Active";
            var templates = await repository.Templates.FindAllTemplatesAsync();
            var activeTemplates = templates.Where(t => t.State.State.Equals(activeState)).ToList();
            return TemplateDTO.ParseTemplatesDTO(activeTemplates);
        }
    }
}
