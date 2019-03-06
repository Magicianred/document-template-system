using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        IUserStatusRepository UserStatus { get; }
        IUserTypeRepository UserType { get; }
        ITemplateRepository Templates { get; }
        ITemplateVersionRepository TemplatesVersions { get; }
        ITemplateStateRepository TemplateState { get; }
    }
}
