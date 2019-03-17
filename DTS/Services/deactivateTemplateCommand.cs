using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class DeactivateTemplateCommand : ICommand
    {
        public int Id { get; }

        public DeactivateTemplateCommand(int id)
        {
            Id = id;
        }
    }

    public sealed class DeactivateTemplateCommandHandler : ICommandHandlerAsync<DeactivateTemplateCommand>
    {

        private readonly IRepositoryWrapper repository;
        private readonly string _inactiveTemplateState = "Inactive";

        public DeactivateTemplateCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(DeactivateTemplateCommand command)
        {
            var template = await repository.Templates.FindTemplateByIDAsync(command.Id);
            var inactiveTemplate = await repository.TemplateState.FindStateByName(_inactiveTemplateState);

            template.State = inactiveTemplate;
            await repository.Templates.UpdateAsync(template);
        }
    }
}
