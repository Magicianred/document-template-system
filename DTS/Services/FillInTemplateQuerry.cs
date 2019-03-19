using DAL.Repositories;
using DTS.APi.Models;
using DTS.API.Helpers;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class FillInTemplateQuery : IQuery
    {
        public int TemplateId { get; }
        public object Data { get; }
        

        public FillInTemplateQuery(int id, object data)
        {
            TemplateId = id;
            Data= data;
        }
    }



    public sealed class FillInTemplateQueryHandler
        : IQueryHandlerAsync<FillInTemplateQuery, TemplateContentDTO>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _activeTemplateState = "Active";

        public FillInTemplateQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<TemplateContentDTO> HandleAsync(FillInTemplateQuery query)
        {
            var activeState = await repository.TemplateState.FindStateByName(_activeTemplateState);

            var templateVersion = await repository.TemplatesVersions
                    .FindTemplatesVersionsByConditionAsync(tempVer => tempVer.TemplateId == query.TemplateId && tempVer.State.Equals(activeState));

            var template = templateVersion.FirstOrDefault();

                template.Content = new JsonInputParser().FillTemplateFromJson(query.Data, template);

                return new TemplateContentDTO
                {
                    Content = template.Content
                };

        }



    }
}