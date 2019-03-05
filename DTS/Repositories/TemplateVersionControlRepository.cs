using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateVersionControlRepository : RepositoryAsync<TemplateVersionControl>, ITemplateVersionControlRepository
    {
        public TemplateVersionControlRepository(DTSContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<TemplateVersionControl> FindByIDAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.ID == id);
            return template.DefaultIfEmpty(new TemplateVersionControl()).FirstOrDefault();
        }

        public async Task<IEnumerable<TemplateVersionControl>> FindByTemplateIdAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.TemplateID == id);
            return template.DefaultIfEmpty(new TemplateVersionControl());
        }

        public async Task<IEnumerable<TemplateVersionControl>> FindByUserIdAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.UserID == id);
            return template.DefaultIfEmpty(new TemplateVersionControl());
        }

        public async Task CreateAsync(TemplateVersionControl template)
        {
            Create(template);
            await SaveAsync();
        }

        public async Task UpdateAsync(TemplateVersionControl template)
        {
            Update(template);
            await SaveAsync();
        }

        public async Task<IEnumerable<TemplateVersionControl>> FindByConditionAsync(Func<TemplateVersionControl, bool> expression)
        {
            return await FindByConditionAsync(expression);
        }
    }
}
