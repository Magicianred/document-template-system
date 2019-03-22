using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class ActivateTemplateVersionCommand : ICommand
    {
        public int TemplateVersionId { get; }
        public int TemplateId { get; }

        public ActivateTemplateVersionCommand(int templateVersionId, int templateId)
        {
            TemplateVersionId = templateVersionId;
            TemplateId = templateId;
        }

    }

    public sealed class ActivateTemplateVersionCommandHandler : ICommandHandlerAsync<ActivateTemplateVersionCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _activeTemplateVersionState = "Active";
        private readonly string _inactiveTemplateVersionState = "Inactive";


        public ActivateTemplateVersionCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ActivateTemplateVersionCommand command)
        {
            var templateVersion = await repository.TemplatesVersions
                .FindTemplateVersionByIdAsync(command.TemplateVersionId);

            var allTemplateVersions = await repository.TemplatesVersions
                .FindAllTemplateVersionsByTemplateIdAsync(command.TemplateId);

            var activeState = await repository.TemplateState.FindTemplateStateByName(_activeTemplateVersionState);
            var inactiveState = await repository.TemplateState.FindTemplateStateByName(_inactiveTemplateVersionState);

            foreach (var tempVersion in allTemplateVersions)
            {
                if (tempVersion.State.Equals(activeState))
                {
                    tempVersion.State = inactiveState;
                    await repository.TemplatesVersions.UpdateTemplateVersionAsync(tempVersion);
                } 
            }

            templateVersion.State = activeState;
            await repository.TemplatesVersions.UpdateTemplateVersionAsync(templateVersion);

        }
    }
}
