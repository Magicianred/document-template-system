using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Template>> FindAllTemplatesAsync();
        Task<Template> FindTemplateByIdAsync(int id);
        Task<bool> Exists(int id);
        Task CreateTemplateAsync(Template template);
        Task UpdateTemplateAsync(Template template);
        Task<IEnumerable<Template>> FindTemplatesByOwnerIdAsync(int id);
    }
}
