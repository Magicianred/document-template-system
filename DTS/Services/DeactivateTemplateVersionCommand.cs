using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class DeactivateTemplateVersionCommand : ICommand
    {
        public int TemplateVersionId { get; }
        

        public DeactivateTemplateVersionCommand(int templateVersionId)
        {
            TemplateVersionId = templateVersionId;
        }

    }

    public sealed class DeactivateTemplateVersionCommandHandler : ICommandHandlerAsync<DeactivateTemplateVersionCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _inactiveTemplateVersionState = "Inactive";


        public DeactivateTemplateVersionCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(DeactivateTemplateVersionCommand command)
        {
            var templateVersion = await repository.TemplatesVersions
                .FindTemplateVersionByIdAsync(command.TemplateVersionId);
            
            var inactiveState = await repository.TemplateState.FindStateByName(_inactiveTemplateVersionState);

            templateVersion.State = inactiveState;
            await repository.TemplatesVersions.UpdateTemplateVersionAsync(templateVersion);
         
        }
    }
}
