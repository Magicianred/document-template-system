using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateRepository : RepositoryAsync<Template>, ITemplateRepository
    {
        public TemplateRepository(DTSContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<Template> FindByIDAsync(int id)
        {
            var template = await FindByConditionAsync(temp => temp.ID == id);
            return template.DefaultIfEmpty(new Template()).FirstOrDefault();
        }

        public async Task CreateAsync(Template template)
        {
            Create(template);
            await SaveAsync();
        }

        public async Task UpdateAsync(Template oldTemplate, Template template)
        {
            oldTemplate.Name = template.Name;
            oldTemplate.TemplateState = template.TemplateState;
            oldTemplate.TemplateVersions = template.TemplateVersions;

            Update(oldTemplate);
            await SaveAsync();
        }
    }
}
