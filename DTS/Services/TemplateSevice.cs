using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Repositories;
using DTS.API.Models.DTOs;

namespace DTS.API.Services
{
    public class TemplateService : ITemplateService
    {
        private ICommandHandlerAsync<ActivateTemplateCommand> _activateTemplateCommand;
        private ICommandHandlerAsync<ActivateTemplateVersionCommand> _activateTemplateVersionCommand;
        private ICommandHandlerAsync<AddTemplateCommand> _addTemplateCommand;
        private ICommandHandlerAsync<AddTemplateVersionCommand> _addTemplateVersionCommand;
        private ICommandHandlerAsync<DeactivateTemplateCommand> _deactivateTemplateCommand;
        private ICommandHandlerAsync<SetTemplateOwnerCommand> _setTemplateOwnerCommand;
        private IQueryHandlerAsync<GetTemplateByIdQuery, TemplateDTO> _getTemplateByIdQuery;
        private IQueryHandlerAsync<GetTemplatesByUserQuery, List<TemplateDTO>> _getTemplatesByUserQuery;
        private IQueryHandlerAsync<GetTemplatesQuery, List<TemplateDTO>> _getTemplatesQuery;
        private IQueryHandlerAsync<FillInTemplateQuery, TemplateContentDTO> _fillInTemplateQuery;
        private IQueryHandlerAsync<GetTemplateFormQuery, Dictionary<string, string>> _getTemplateFormQuery;
        private readonly IRepositoryWrapper repository;


        public TemplateService(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }


        public ICommandHandlerAsync<ActivateTemplateCommand> ActivateTemplateCommand
        {
            get
            {
                if (_activateTemplateCommand == null)
                {
                    _activateTemplateCommand = new ActivateTemplateCommandHandler(repository);
                }
                return _activateTemplateCommand;
            }
        }


        public ICommandHandlerAsync<ActivateTemplateVersionCommand> ActivateTemplateVersionCommand
        {
            get
            {
                if (_activateTemplateVersionCommand == null)
                {
                    _activateTemplateVersionCommand = new ActivateTemplateVersionCommandHandler(repository);
                }
                return _activateTemplateVersionCommand;
            }
        }


        public ICommandHandlerAsync<AddTemplateCommand> AddTemplateCommand
        {
            get
            {
                if (_addTemplateCommand == null)
                {
                    _addTemplateCommand = new AddTemplateCommandHandler(repository);
                }
                return _addTemplateCommand;
            }
        }


        public ICommandHandlerAsync<AddTemplateVersionCommand> AddTemplateVersionCommand
        {
            get
            {
                if (_addTemplateVersionCommand == null)
                {
                    _addTemplateVersionCommand = new AddTemplateVersionCommandHandler(repository);
                }
                return _addTemplateVersionCommand;
            }
        }


        public ICommandHandlerAsync<DeactivateTemplateCommand> DeactivateTemplateCommand
        {
            get
            {
                if (_deactivateTemplateCommand == null)
                {
                    _deactivateTemplateCommand = new DeactivateTemplateCommandHandler(repository);
                }
                return _deactivateTemplateCommand;
            }
        }


        public ICommandHandlerAsync<SetTemplateOwnerCommand> SetTemplateOwnerCommand
        {
            get
            {
                if (_setTemplateOwnerCommand == null)
                {
                    _setTemplateOwnerCommand = new SetTemplateOwnerCommandHandler(repository);
                }
                return _setTemplateOwnerCommand;
            }
        }


        public IQueryHandlerAsync<GetTemplateByIdQuery, TemplateDTO> GetTemplateByIdQuery
        {
            get
            {
                if (_getTemplateByIdQuery == null)
                {
                    _getTemplateByIdQuery = new GetTemplateByIdQueryHandler(repository);
                }
                return _getTemplateByIdQuery;
            }
        }


        public IQueryHandlerAsync<GetTemplatesByUserQuery, List<TemplateDTO>> GetTemplatesByUserQuery
        {
            get
            {
                if (_getTemplatesByUserQuery == null)
                {
                    _getTemplatesByUserQuery = new GetTemplatesByUserQueryHandler(repository);
                }
                return _getTemplatesByUserQuery;
            }
        }


        public IQueryHandlerAsync<GetTemplatesQuery, List<TemplateDTO>> GetTemplatesQuery
        {
            get
            {
                if (_getTemplatesQuery == null)
                {
                    _getTemplatesQuery = new GetTemplatesQueryHandler(repository);
                }
                return _getTemplatesQuery;
            }
        }


        public IQueryHandlerAsync<FillInTemplateQuery, TemplateContentDTO> FillInTemplateQuery
        {
            get
            {
                if (_fillInTemplateQuery == null)
                {
                    _fillInTemplateQuery = new FillInTemplateQueryHandler(repository);
                }
                return _fillInTemplateQuery;
            }
        }


        public IQueryHandlerAsync<GetTemplateFormQuery, Dictionary<string, string>> GetTemplateFormQuery
        {
            get
            {
                if (_getTemplateFormQuery == null)
                {
                    _getTemplateFormQuery = new GetTemplateFormQueryHandler(repository);
                }
                return _getTemplateFormQuery;
            }
        }
    }
}
