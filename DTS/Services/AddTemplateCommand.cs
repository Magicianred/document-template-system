using DAL.Models;
using DAL.Repositories;
using DTS.APi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class AddTemplateCommand : ICommand
    {
        public TemplateVersionInput NewTemplateVersionInput { get; }

        public AddTemplateCommand(TemplateVersionInput templateVersionInput)
        {
            NewTemplateVersionInput = templateVersionInput;
        }
    }

    public sealed class AddTemplateCommandHandler : ICommandHandlerAsync<AddTemplateCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _inactiveTemplateState = "Inactive";

        public AddTemplateCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(AddTemplateCommand command)
        {
            var inactiveTemplateState = await repository.TemplateState.FindTemplateStateByName(_inactiveTemplateState);

            var newTemplate = new Template
            {
                Name = command.NewTemplateVersionInput.TemplateName,
                OwnerId = command.NewTemplateVersionInput.AuthorId,
                State = inactiveTemplateState
            };
            await repository.Templates
                .CreateTemplateAsync(newTemplate);

            var newTemplateVersion = new TemplateVersion
            {
                Content = command.NewTemplateVersionInput.Template,
                TemplateId = newTemplate.Id,
                CreatorId = command.NewTemplateVersionInput.AuthorId,
                State = inactiveTemplateState
            };

            await repository.TemplatesVersions
                .CreateTemplateVersionAsync(newTemplateVersion);
        }
    }
}
