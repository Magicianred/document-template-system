using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface ITemplateVersionControlRepository
    {
        Task<TemplateVersionControl> FindByIDAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindByConditionAsync(Func<TemplateVersionControl, bool> expression);
        Task<IEnumerable<TemplateVersionControl>> FindByTemplateIdAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindByUserIdAsync(int id);
        Task CreateAsync(TemplateVersionControl template);
        Task UpdateAsync(TemplateVersionControl template);
    }
}
