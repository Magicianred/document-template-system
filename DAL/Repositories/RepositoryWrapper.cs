using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DTSLocalDBContext _context;
        private IUserRepository _users;
        private ITemplateRepository _templates;
        private ITemplateVersionRepository _templatesVersions;
        private IUserStatusRepository _userStatus;
        private IUserTypeRepository _userType;
        private ITemplateStateRepository _templateState;
        
        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public ITemplateRepository Templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = new TemplateRepository(_context);
                }
                return _templates;
            }
        }

        public ITemplateVersionRepository TemplatesVersions
        {
            get
            {
                if (_templatesVersions == null)
                {
                    _templatesVersions = new TemplateVersionRepository(_context);
                }
                return _templatesVersions;
            }
        }

        public IUserStatusRepository UserStatus
        {
            get
            {
                if (_userStatus == null)
                {
                    _userStatus = new UserStatusRepository(_context);
                }
                return _userStatus;
            }
        }

        public IUserTypeRepository UserType
        {
            get
            {
                if (_userType == null)
                {
                    _userType = new UserTypeRepository(_context);
                }
                return _userType;
            }
        }

        public ITemplateStateRepository TemplateState
        {
            get
            {
                if (_templateState == null)
                {
                    _templateState = new TemplateStateRepository(_context);
                }
                return _templateState;
            }
        }

        public RepositoryWrapper(DTSLocalDBContext DtsContext)
        {
            this._context = DtsContext;
        }
    }
}
