using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    interface ITemplateVersionControlRepository : IRepositoryAsync<TemplateVersionControl>
    {
        Task<TemplateVersionControl> FindByIDAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindByTemplateIdAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindByUserId(int id);
        Task CreateAsync(TemplateVersionControl template);
        Task UpdateAsync(TemplateVersionControl template);
    }
}
