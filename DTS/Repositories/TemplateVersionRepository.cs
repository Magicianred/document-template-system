using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateVersionRepository : RepositoryAsync<TemplateVersion>, ITemplateVersionRepository
    {
        public TemplateVersionRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<TemplateVersion> FindVersionByIDAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.Id == id);
            return template.DefaultIfEmpty(new TemplateVersion()).FirstOrDefault();
        }

        public async Task<IEnumerable<TemplateVersion>> FindByTemplateIdAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.TemplateId == id);
            return template.DefaultIfEmpty(new TemplateVersion());
        }

        public async Task<IEnumerable<TemplateVersion>> FindByUserIdAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.CreatorId == id);
            return template.DefaultIfEmpty(new TemplateVersion());
        }

        public async Task CreateAsync(TemplateVersion template)
        {
            Create(template);
            await SaveAsync();
        }

        public async Task UpdateAsync(TemplateVersion template)
        {
            Update(template);
            await SaveAsync();
        }

        public async Task<IEnumerable<TemplateVersion>> FindVersionByConditionAsync(Expression<Func<TemplateVersion, bool>> expression)
        {
            return await FindByConditionAsync(expression);
        }

        public async Task<IEnumerable<TemplateVersion>> FindAllVersions()
        {
            return await FindAllAsync();
        }

        public async Task UpdateAsync(IEnumerable<TemplateVersion> templates)
        {
            foreach (var template in templates)
            {
                Update(template);
            }
            await SaveAsync();
        }
    }
}
