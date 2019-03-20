using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ITemplateStateRepository
    {
        Task<IEnumerable<TemplateState>> FindAllTemplatesAsync();
        Task<TemplateState> FindStateByIdAsync(int id);
        Task<TemplateState> FindStateByName(string name); 
    }
}
