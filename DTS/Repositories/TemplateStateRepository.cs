using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateStateRepository : RepositoryAsync<TemplateState>, ITemplateStateRepository
    {
        public TemplateStateRepository(DTSLocalDBContext DtsContext)
            :base(DtsContext)
        {
        }

        public async Task<TemplateState> FindStateByIdAsync(int id)
        {
            var states = await FindByConditionAsync(s => s.Id == id);
            return states.FirstOrDefault() ?? throw new KeyNotFoundException();
        }
    }
}
