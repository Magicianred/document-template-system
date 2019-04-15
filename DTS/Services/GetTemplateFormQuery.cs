using DAL.Repositories;
using DTS.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplateFormQuery : IQuery
    {
        public int TemplateId { get; }

        public GetTemplateFormQuery(int id)
        {
            TemplateId = id;
        }
    }

    public sealed class GetTemplateFormQueryHandler : IQueryHandlerAsync<GetTemplateFormQuery, IDictionary<string, string>>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _activeTemplateState = "Active";

        public GetTemplateFormQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task<IDictionary<string, string>> HandleAsync(GetTemplateFormQuery query)
        {
            var activeState = await repository.TemplateState.FindTemplateStateByName(_activeTemplateState);
            var templates = await repository.TemplatesVersions
                    .FindTemplatesVersionsByConditionAsync(tempVer => tempVer.TemplateId == query.TemplateId && tempVer.State.Equals(activeState));

            var template = templates.FirstOrDefault(); 
            var templateFormContent = template.Content;

            return new TemplateParser().ParseFields(templateFormContent);
        }
    }
}
