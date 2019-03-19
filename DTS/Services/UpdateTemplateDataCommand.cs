using DAL.Repositories;
using DTS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class UpdateTemplateDataCommand : ICommand
    {
        public int TemplateId { get; }
        public TemplateUpdateInput TemplateUpdate { get; }

        public UpdateTemplateDataCommand(int id, TemplateUpdateInput templateUpdateInput)
        {
            TemplateId = id;
            TemplateUpdate = templateUpdateInput;
        }
    }


    public sealed class UpdateTemplateDataCommandHandler 
        : ICommandHandlerAsync<UpdateTemplateDataCommand>
    {
        private readonly IRepositoryWrapper repository;

        public UpdateTemplateDataCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task HandleAsync(UpdateTemplateDataCommand command)
        {
            var template = await repository.Templates.FindTemplateByIdAsync(command.TemplateId);

            if(!string.IsNullOrWhiteSpace(command.TemplateUpdate.Name))
            {
                template.Name = command.TemplateUpdate.Name;
            }
           
            template.State = await repository.TemplateState.FindStateByIdAsync(command.TemplateUpdate.StateId);
            template.Owner = await repository.Users.FindUserByIDAsync(command.TemplateUpdate.OwnerID);

            await repository.Templates.UpdateTemplateAsync(template);
        }

        
    }
}
