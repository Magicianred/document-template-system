using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class SetTemplateOwnerCommand : ICommand
    {
        public int TemplateId { get; }
        public int OwnerId { get; }


        public SetTemplateOwnerCommand(int templateId, int ownerId)
        {
            TemplateId = templateId;
            OwnerId = ownerId;
        }
    }

    public sealed class SetTemplateOwnerCommandHandler : ICommandHandlerAsync<SetTemplateOwnerCommand>
    {
        private readonly IRepositoryWrapper repository;

        public SetTemplateOwnerCommandHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public async Task HandleAsync(SetTemplateOwnerCommand command)
        {
            var template = await repository.Templates
                .FindTemplateByIdAsync(command.TemplateId);

            template.OwnerId = command.OwnerId;
            await repository.Templates.UpdateTemplateAsync(template);
        }
    }
}
