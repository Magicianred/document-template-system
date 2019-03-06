using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Template>> FindAllTemplatesAsync();
        Task<Template> FindTemplateByIDAsync(int id);
        Task<bool> Exists(int id);
        Task CreateAsync(Template template);
        Task UpdateAsync(Template template);
    }
}
