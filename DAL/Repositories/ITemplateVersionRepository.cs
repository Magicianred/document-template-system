using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ITemplateVersionRepository
    {
        Task<IEnumerable<TemplateVersion>> FindAllTemplatesVersions();
        Task<TemplateVersion> FindTemplateVersionByIdAsync(int id);
        Task<IEnumerable<TemplateVersion>> FindTemplatesVersionsByConditionAsync(Expression<Func<TemplateVersion, bool>> expression);
        Task<IEnumerable<TemplateVersion>> FindAllTemplateVersionsByTemplateIdAsync(int id);
        Task<IEnumerable<TemplateVersion>> FindTemplatesVersionsByUserIdAsync(int id);
        Task CreateTemplateVersionAsync(TemplateVersion template);
        Task UpdateTemplateVersionAsync(TemplateVersion template);
        Task UpdateTemplatesVersionsAsync(IEnumerable<TemplateVersion> templates);
    }
}
