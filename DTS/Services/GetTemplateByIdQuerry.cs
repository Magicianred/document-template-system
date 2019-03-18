using DAL.Repositories;
using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplateByIdQuerry : IQuery
    {
        public int Id { get; }

        public GetTemplateByIdQuerry(int id)
        {
            Id = id;
        }
    }



    public sealed class GetTemplateByIdQueryHandler
        : IQueryHandlerAsync<GetTemplateByIdQuerry, TemplateDTO>
    {
        private readonly IRepositoryWrapper repository;

        public GetTemplateByIdQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<TemplateDTO> HandleAsync(GetTemplateByIdQuerry query)
        {
            var template = await repository.Templates.FindTemplateByIdAsync(query.Id);
            return TemplateDTO.ParseTemplate(template);
        }


        
    }
}
