using DAL.Models;
using DAL.Repositories;
using DTS.APi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class AddTemplateVersionCommand : ICommand
    {
        public int TemplateId { get; }
        public TemplateVersionInput NewTemplateVersionInput { get; }

        public AddTemplateVersionCommand(int id, TemplateVersionInput templateVersionInput)
        {
            TemplateId = id;
            NewTemplateVersionInput = templateVersionInput;
        }
    }


    public sealed class AddTemplateVersionCommandHandler 
        : ICommandHandlerAsync<AddTemplateVersionCommand>
    {
        private readonly IRepositoryWrapper repository;
        private readonly string _inactiveTemplateState = "Inactive";

        public AddTemplateVersionCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task HandleAsync(AddTemplateVersionCommand command)
        {
            var newTemplateVersion = new TemplateVersion
            {
                Content = command.NewTemplateVersionInput.Template,
                TemplateId = command.TemplateId,
                CreatorId = command.NewTemplateVersionInput.AuthorId,
                State = await repository.TemplateState
                .FindStateByName(_inactiveTemplateState)
            };

            await repository.TemplatesVersions
                .CreateTemplateVersionAsync(newTemplateVersion);
        }
    }

}
