using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TemplateVersionRepository : RepositoryAsync<TemplateVersion>, ITemplateVersionRepository
    {
        public TemplateVersionRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<IEnumerable<TemplateVersion>> FindAllTemplatesVersions()
        {
            var templatesVersions = await DTSContext.TemplateVersion
                .Include(temp => temp.State)
                .Include(temp => temp.Template)
                .Include(temp => temp.Creator)
                .ToListAsync();

            return templatesVersions.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task<TemplateVersion> FindTemplateVersionByIdAsync(int id)
        {
            var templatesVersions = await FindAllTemplatesVersions();
            var templateVersion = templatesVersions
                .Where(temp => temp.Id == id);
            return templateVersion.FirstOrDefault() ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<TemplateVersion>> FindAllTemplateVersionsByTemplateIdAsync(int id)
        {
            var templatesVersions = await FindAllTemplatesVersions();
            var templateVersions = templatesVersions
                .Where(temp => temp.TemplateId == id);
            return templateVersions.DefaultIfEmpty() ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<TemplateVersion>> FindTemplatesVersionsByUserIdAsync(int id)
        {
            var templatesVersions = await FindAllTemplatesVersions();
            var userTemplatesVersions = templatesVersions
                .Where(temp => temp.CreatorId == id);
            return userTemplatesVersions.DefaultIfEmpty() ?? throw new KeyNotFoundException();
        }

        public async Task CreateTemplateVersionAsync(TemplateVersion templateVersion)
        {
            Create(templateVersion);
            await SaveAsync();
        }

        public async Task UpdateTemplateVersionAsync(TemplateVersion templateVersion)
        {
            Update(templateVersion);
            await SaveAsync();
        }

        public async Task<IEnumerable<TemplateVersion>> FindTemplatesVersionsByConditionAsync(Expression<Func<TemplateVersion, bool>> expression)
        {
            var templatesVersions = await FindByConditionAsync(expression);
            return templatesVersions.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task UpdateTemplatesVersionsAsync(IEnumerable<TemplateVersion> templatesVersions)
        {
            foreach (var templateVersion in templatesVersions)
            {
                Update(templateVersion);
            }
            await SaveAsync();
        }
    }
}
