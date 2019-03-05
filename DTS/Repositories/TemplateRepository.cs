using DTS.Data;
using DTS.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Template>> FindAllTemplatesAsync()
        {
            return await DTSContext.Templates
                .Include(temp => temp.TemplateState)
                .Include(temp => temp.TemplateVersions)
                .ToListAsync();
        }

        public async Task<Template> FindTemplateByIDAsync(int id)
        {
            var template = await DTSContext.Templates
                .Include(temp => temp.TemplateVersions)
                .Include(temp => temp.TemplateState)
                .Where(temp => temp.ID == id)
                .ToListAsync();
            return template.DefaultIfEmpty(new Template()).FirstOrDefault();
        }

        public async Task CreateAsync(Template template)
        {
            Create(template);
            await SaveAsync();
        }

        public async Task UpdateAsync(Template template)
        {
            Update(template);
            await SaveAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await DTSContext.Templates.AnyAsync(e => e.ID == id);
        }
    }
}
