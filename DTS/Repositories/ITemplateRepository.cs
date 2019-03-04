using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    interface ITemplateRepository : IRepositoryAsync<Template>
    {
        Task<Template> FindByIDAsync(int id);
        Task CreateAsync(Template template);
        Task UpdateAsync(Template oldTemplate, Template template);
    }
}
