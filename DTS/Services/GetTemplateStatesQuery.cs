using DAL.Repositories;
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
    : IQueryHandlerAsync<GetTemplateStatesQuery, IEnumerable<string>>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplateStatesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<IEnumerable<string>> HandleAsync(GetTemplateStatesQuery query)
        {
            var states = await repository.TemplateState.FindAllTemplatesAsync();
            var statesNames = new List<string>();
            foreach (var state in states)
            {
                statesNames.Add(state.State);
            }
            return statesNames;
        }
    }
}
