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
            return await DTSContext.Template
                .Include(temp => temp.State)
                .ToListAsync();
        }

        public async Task<Template> FindTemplateByIDAsync(int id)
        {
            var template = await DTSContext.Template
                .Include(temp => temp.State)
                .Where(temp => temp.Id == id)
                .ToListAsync();
            return template.DefaultIfEmpty(new Template()).FirstOrDefault();
        }

        public async Task<IEnumerable<Template>> FindByUserIdAsync(int id)
        {
             return await DTSContext.Template
                .Where(temp => temp.OwnerId == id)
                .ToListAsync();
            
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
