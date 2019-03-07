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
        public TemplateRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<IEnumerable<Template>> FindAllTemplatesAsync()
        {
            var templates = await DTSContext.Template
                .Include(temp => temp.TemplateVersion)
                .Include(temp => temp.State)
                .Include(temp => temp.Owner)
                .ToListAsync();

            if (templates.FirstOrDefault() == null)
            {
                throw new InvalidOperationException("Templates not Found");
            }
            return templates;
        }

        public async Task<Template> FindTemplateByIDAsync(int id)
        {
            var templates = await FindAllTemplatesAsync();
            var template = templates
                .Where(temp => temp.Id == id)
                .DefaultIfEmpty(new Template())
                .FirstOrDefault();

            return template ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<Template>> FindByUserIdAsync(int id)
        {
            var templates = await FindAllTemplatesAsync();
            var template = templates
                .Where(temp => temp.OwnerId == id);

            return template ?? throw new KeyNotFoundException();
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
            return await DTSContext.Template.AnyAsync(e => e.Id == id);
        }
    }
}
