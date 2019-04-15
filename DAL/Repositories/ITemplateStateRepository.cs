using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ITemplateStateRepository
    {
        Task<IEnumerable<TemplateState>> FindAllTemplateStatesAsync();
        Task<TemplateState> FindTemplateStateByIdAsync(int id);
        Task<TemplateState> FindTemplateStateByName(string name); 
    }
}
