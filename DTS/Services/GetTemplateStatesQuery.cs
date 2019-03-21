using DAL.Models;
using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetTemplateStatesQuery : IQuery
    {
    }

    public sealed class GetTemplateStatesQueryHandler
    : IQueryHandlerAsync<GetTemplateStatesQuery, IList<TemplateStateDTO>>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplateStatesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<IList<TemplateStateDTO>> HandleAsync(GetTemplateStatesQuery query)
        {
            var states = await repository.TemplateState.FindAllTemplateStatesAsync();

            var statesNames = new List<TemplateStateDTO>();

            foreach (var state in states)
            {
                statesNames.Add(TemplateStateDTO.ParseTemplateStateDTO(state));
            }
            return statesNames;
        }
    }
}
