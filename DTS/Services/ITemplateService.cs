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
        ICommandHandlerAsync<AddTemplateCommand> AddTemplteCommand { get; }
        ICommandHandlerAsync<AddTemplateVersionCommand> AddTemplateVersionCommand { get; }
        ICommandHandlerAsync<DeactivateTemplateCommand> DeactivateTemplateCommand { get; }
        ICommandHandlerAsync<SetTemplateOwnerCommand> SetTemplateOwnerCommand { get; }
        IQueryHandlerAsync<GetTemplateByIdQuery, TemplateDTO> GetTemplateByIdQuery { get; }
        IQueryHandlerAsync<GetTemplatesByUserQuery, List<TemplateDTO>> GetTemplatesByUserQuery { get; }
        IQueryHandlerAsync<GetTemplatesQuery, List<TemplateDTO>> GetTemplatesQuery { get; }

    }
}
