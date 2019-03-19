using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
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
                    .ThenInclude(temp =>
                        temp.State)
                .Include(temp => temp.State)
                .Include(temp => temp.Owner)
                .ToListAsync();

            return templates.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task<Template> FindTemplateByIdAsync(int id)
        {
            var templates = await FindAllTemplatesAsync();
            var template = templates
                .Where(temp => temp.Id == id)
                .FirstOrDefault();

            return template ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<Template>> FindTemplatesByOwnerIdAsync(int id)
        {
            var templates = await FindAllTemplatesAsync();
            var template = templates
                .Where(temp => temp.OwnerId == id);

            return template.DefaultIfEmpty() ?? throw new KeyNotFoundException();
        }

        public async Task CreateTemplateAsync(Template template)
        {
            Create(template);
            await SaveAsync();
        }

        public async Task UpdateTemplateAsync(Template template)
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
