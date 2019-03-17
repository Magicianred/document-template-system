using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class ActivateTemplateCommand : ICommand
    {
        public int Id { get; }

        public ActivateTemplateCommand(int id)
        {
            Id = id;
        }
    }

    public sealed class ActivateTemplateCommandHandler : ICommandHandlerAsync<ActivateTemplateCommand>
    {

        private readonly IRepositoryWrapper repository;
        private readonly string _activeTemplateState = "Active";

        public ActivateTemplateCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ActivateTemplateCommand command)
        {
            var template = await repository.Templates.FindTemplateByIDAsync(command.Id);
            var activeTemplate = await repository.TemplateState.FindStateByName(_activeTemplateState);

            template.State = activeTemplate;
            await repository.Templates.UpdateAsync(template);
        }
    }
}
