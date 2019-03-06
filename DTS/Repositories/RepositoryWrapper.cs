using DTS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DTSContext _context;
        private IUserRepository _users;
        private ITemplateRepository _templates;
        private ITemplateVersionControlRepository _templatesVersions;
        
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

        public ITemplateVersionControlRepository TemplatesVersions
        {
            get
            {
                if (_templatesVersions == null)
                {
                    _templatesVersions = new TemplateVersionControlRepository(_context);
                }
                return _templatesVersions;
            }
        }

        public RepositoryWrapper(DTSContext DtsContext)
        {
            this._context = DtsContext;
        }
    }
}
