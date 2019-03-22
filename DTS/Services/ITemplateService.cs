using DTS.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public interface ITemplateService
    {
        ICommandHandlerAsync<ActivateTemplateCommand> ActivateTemplateCommand { get; }
        ICommandHandlerAsync<ActivateTemplateVersionCommand> ActivateTemplateVersionCommand { get; }
        ICommandHandlerAsync<AddTemplateCommand> AddTemplateCommand { get; }
        ICommandHandlerAsync<AddTemplateVersionCommand> AddTemplateVersionCommand { get; }
        ICommandHandlerAsync<DeactivateTemplateCommand> DeactivateTemplateCommand { get; }
        ICommandHandlerAsync<DeactivateTemplateVersionCommand> DeactivateTemplateVersionCommand { get; }
        ICommandHandlerAsync<SetTemplateOwnerCommand> SetTemplateOwnerCommand { get; }
        ICommandHandlerAsync<UpdateTemplateDataCommand> UpdateTemplateDataCommand { get; }
        IQueryHandlerAsync<GetTemplateByIdQuery, TemplateDTO> GetTemplateByIdQuery { get; }
        IQueryHandlerAsync<GetTemplatesByUserQuery, IList<TemplateDTO>> GetTemplatesByUserQuery { get; }
        IQueryHandlerAsync<GetTemplatesQuery, IList<TemplateDTO>> GetTemplatesQuery { get; }
        IQueryHandlerAsync<FillInTemplateQuery, TemplateContentDTO> FillInTemplateQuery { get; }
        IQueryHandlerAsync<GetTemplateFormQuery, IDictionary<string, string>> GetTemplateFormQuery { get; }
        IQueryHandlerAsync<GetTemplateStatesQuery, IList<TemplateStateDTO>> GetTemplateStatesQuery { get; }
        IQueryHandlerAsync<GetActiveTemplatesQuery, IList<TemplateDTO>> GetActiveTemplatesQuery { get; }
    }
}
