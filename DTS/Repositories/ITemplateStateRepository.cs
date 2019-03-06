using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface ITemplateStateRepository
    {
        Task<TemplateState> FindStateByIdAsync(int id); 
    }
}
