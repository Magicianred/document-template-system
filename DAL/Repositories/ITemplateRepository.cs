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
        Task<Template> FindTemplateByIDAsync(int id);
        Task<bool> Exists(int id);
        Task CreateAsync(Template template);
        Task UpdateAsync(Template template);
        Task<IEnumerable<Template>> FindByUserIdAsync(int id);
    }
}
