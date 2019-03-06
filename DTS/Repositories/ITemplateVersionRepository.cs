using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface ITemplateVersionRepository
    {
        Task<IEnumerable<TemplateVersion>> FindAllVersions();
        Task<TemplateVersion> FindVersionByIDAsync(int id);
        Task<IEnumerable<TemplateVersion>> FindVersionByConditionAsync(Expression<Func<TemplateVersion, bool>> expression);
        Task<IEnumerable<TemplateVersion>> FindByTemplateIdAsync(int id);
        Task<IEnumerable<TemplateVersion>> FindByUserIdAsync(int id);
        Task CreateAsync(TemplateVersion template);
        Task UpdateAsync(TemplateVersion template);
        Task UpdateAsync(IEnumerable<TemplateVersion> templates);
    }
}
