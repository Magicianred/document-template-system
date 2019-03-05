using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface ITemplateVersionControlRepository
    {
        Task<IEnumerable<TemplateVersionControl>> FindAllVersions();
        Task<TemplateVersionControl> FindVersionByIDAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindVersionByConditionAsync(Expression<Func<TemplateVersionControl, bool>> expression);
        Task<IEnumerable<TemplateVersionControl>> FindByTemplateIdAsync(int id);
        Task<IEnumerable<TemplateVersionControl>> FindByUserIdAsync(int id);
        Task CreateAsync(TemplateVersionControl template);
        Task UpdateAsync(TemplateVersionControl template);
        Task UpdateAsync(IEnumerable<TemplateVersionControl> templates);
    }
}
